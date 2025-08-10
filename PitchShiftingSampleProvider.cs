using NAudio.Wave;
using System;
using System.Numerics;
using System.IO;
using System.Diagnostics;

namespace Vocal_Fry
{
    /// <summary>
    /// Provides real-time pitch shifting of audio samples using the phase vocoder technique.
    /// </summary>
    public class PitchShiftingSampleProvider : ISampleProvider, IDisposable
    {
        #region Fields and Properties

        /// <summary>
        /// Gets the WaveFormat of this sample provider.
        /// </summary>
        public WaveFormat WaveFormat { get; }

        private readonly ISampleProvider sourceProvider;
        private readonly object lockObject = new object();
        private volatile float pitchRatio;
        private readonly int fftSize;
        private readonly int hopSize;
        private readonly float[] window;
        private readonly float[] inputBuffer;
        private int inputBufferCount;
        private readonly Complex[] fftBuffer;
        private readonly float[] outputBuffer;
        private int outputBufferCount;
        private int outputBufferPosition;
        private readonly float[] lastInputPhase;
        private readonly float[] lastOutputPhase;
        private bool isDisposed = false;
        private readonly ErrorLogger logger;

        /// <summary>
        /// Gets or sets the pitch ratio. A value of 1.0 means no change,
        /// 0.5 means one octave down, and 2.0 means one octave up.
        /// </summary>
        public float PitchRatio
        {
            get
            {
                lock (lockObject)
                {
                    return pitchRatio;
                }
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Pitch ratio must be greater than zero.");

                lock (lockObject)
                {
                    this.pitchRatio = value;
                }
            }
        }

        #endregion

        #region Constructor and Initialization

        /// <summary>
        /// Creates a new PitchShiftingSampleProvider.
        /// </summary>
        /// <param name="source">The source sample provider.</param>
        /// <param name="fftSize">The FFT size (must be a power of 2).</param>
        /// <param name="hopSize">The hop size (typically fftSize/4 or fftSize/8).</param>
        /// <exception cref="ArgumentNullException">Thrown if source is null.</exception>
        /// <exception cref="ArgumentException">Thrown if fftSize is not a power of 2.</exception>
        public PitchShiftingSampleProvider(ISampleProvider source, int fftSize = 4096, int hopSize = 1024)
        {
            try
            {
                // Create logger
                logger = new ErrorLogger();
                logger.LogMessage("Initializing PitchShiftingSampleProvider");
                if ((fftSize & (fftSize - 1)) != 0)
                    throw new ArgumentException("FFT size must be a power of 2.");

                if (hopSize <= 0 || hopSize > fftSize)
                    throw new ArgumentException("Hop size must be positive and not greater than FFT size.");

                // Initialize fields
                this.sourceProvider = source ?? throw new ArgumentNullException(nameof(source));
                this.WaveFormat = source.WaveFormat;
                this.pitchRatio = 1.0f;
                this.fftSize = fftSize;
                this.hopSize = hopSize;

                logger.LogMessage($"Source format: {source.WaveFormat}, FFT size: {fftSize}, Hop size: {hopSize}");

                // Initialize window function (Hann window)
                this.window = new float[fftSize];
                for (int i = 0; i < fftSize; i++)
                {
                    window[i] = 0.5f * (1 - (float)Math.Cos(2 * Math.PI * i / fftSize));
                }

                // Initialize buffers
                this.inputBuffer = new float[fftSize];
                this.fftBuffer = new Complex[fftSize];
                this.outputBuffer = new float[fftSize];

                int freqDomainSize = fftSize / 2 + 1;
                this.lastInputPhase = new float[freqDomainSize];
                this.lastOutputPhase = new float[freqDomainSize];

                InitializeFftBuffer();

                // Clear output buffer initially
                Array.Clear(outputBuffer, 0, fftSize);

                logger.LogMessage("PitchShiftingSampleProvider initialized successfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                if (logger != null)
                    logger.LogException("PitchShiftingSampleProvider Constructor", ex);
                else
                    Debug.WriteLine($"Error initializing PitchShiftingSampleProvider: {ex}");

                // Rethrow to notify caller
                throw;
            }
        }

        /// <summary>
        /// Initializes the FFT buffer with zeros.
        /// </summary>
        private void InitializeFftBuffer()
        {
            try
            {
                for (int i = 0; i < fftSize; i++)
                {
                    fftBuffer[i] = new Complex(0, 0);
                }
            }
            catch (Exception ex)
            {
                logger.LogException("InitializeFftBuffer", ex);
                throw;
            }
        }

        #endregion

        #region Read Method

        /// <summary>
        /// Reads samples from the source provider, applies pitch shifting, and writes to the output buffer.
        /// </summary>
        /// <param name="buffer">The buffer to write to.</param>
        /// <param name="offset">The offset in the buffer to start writing.</param>
        /// <param name="count">The number of samples to read.</param>
        /// <returns>The number of samples written to the buffer.</returns>
        public int Read(float[] buffer, int offset, int count)
        {
            if (isDisposed)
                throw new ObjectDisposedException("PitchShiftingSampleProvider");

            try
            {
                // Validate parameters
                if (buffer == null)
                    throw new ArgumentNullException(nameof(buffer));
                if (offset < 0)
                    throw new ArgumentOutOfRangeException(nameof(offset), "Offset cannot be negative");
                if (count < 0)
                    throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be negative");
                if (buffer.Length - offset < count)
                    throw new ArgumentException("Insufficient space in destination buffer.");

                int samplesWritten = 0;
                while (samplesWritten < count)
                {
                    // If we have output samples available, copy them to the output buffer
                    if (outputBufferCount > 0)
                    {
                        int toCopy = Math.Min(count - samplesWritten, outputBufferCount);

                        try
                        {
                            // Manual copy instead of Array.Copy to avoid potential type mismatch issues
                            for (int i = 0; i < toCopy; i++)
                            {
                                buffer[offset + samplesWritten + i] = outputBuffer[outputBufferPosition + i];
                            }

                            samplesWritten += toCopy;
                            outputBufferPosition += toCopy;
                            outputBufferCount -= toCopy;
                        }
                        catch (Exception ex)
                        {
                            logger.LogException("Buffer Copy", ex);
                            throw new InvalidOperationException("Error copying audio data", ex);
                        }
                    }
                    else
                    {
                        try
                        {
                            // Need to process more input data
                            int samplesNeeded = fftSize - inputBufferCount;
                            int samplesRead = sourceProvider.Read(inputBuffer, inputBufferCount, samplesNeeded);

                            // If no more input data is available, break out of the loop
                            if (samplesRead == 0) break;

                            inputBufferCount += samplesRead;

                            // If we have enough input samples, process a frame
                            if (inputBufferCount == fftSize)
                            {
                                ProcessFrame();

                                // Shift input buffer by hop size to prepare for next frame
                                // Manual copy instead of Array.Copy
                                for (int i = 0; i < fftSize - hopSize; i++)
                                {
                                    inputBuffer[i] = inputBuffer[i + hopSize];
                                }
                                inputBufferCount = fftSize - hopSize;
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.LogException("Input Processing", ex);
                            throw new InvalidOperationException("Error processing input audio", ex);
                        }
                    }
                }
                return samplesWritten;
            }
            catch (Exception ex)
            {
                logger.LogException("Read Method", ex);
                throw;
            }
        }

        #endregion

        #region ProcessFrame Method

        /// <summary>
        /// Processes a frame of audio using the phase vocoder algorithm.
        /// </summary>
        private void ProcessFrame()
        {
            if (isDisposed)
                throw new ObjectDisposedException("PitchShiftingSampleProvider");

            try
            {
                float localPitchRatio;

                lock (lockObject)
                {
                    for (int i = 0; i < fftSize; i++)
                    {
                        fftBuffer[i] = new Complex(inputBuffer[i] * window[i], 0);
                    }
                    localPitchRatio = pitchRatio; // Use the updated pitch ratio
                }

                // Perform forward FFT
                FastFourierTransform.FFT(false, fftBuffer, true);

                for (int i = 0; i <= fftSize / 2; i++)
                {
                    float magnitude = (float)fftBuffer[i].Magnitude;
                    float phase = (float)Math.Atan2(fftBuffer[i].Imaginary, fftBuffer[i].Real);

                    float phaseDelta = phase - lastInputPhase[i];
                    lastInputPhase[i] = phase;

                    float expectedPhaseAdvance = (float)(2 * Math.PI * i * hopSize / fftSize);
                    float phaseError = phaseDelta - expectedPhaseAdvance;

                    while (phaseError > Math.PI) phaseError -= (float)(2 * Math.PI);
                    while (phaseError < -Math.PI) phaseError += (float)(2 * Math.PI);

                    float freq = (float)(2 * Math.PI * i / fftSize) + phaseError / hopSize;
                    float newPhase = lastOutputPhase[i] + freq * hopSize * localPitchRatio; // Apply pitch ratio here
                    lastOutputPhase[i] = newPhase;

                    fftBuffer[i] = Complex.FromPolarCoordinates(magnitude, newPhase);

                    if (i > 0 && i < fftSize / 2)
                    {
                        fftBuffer[fftSize - i] = Complex.Conjugate(fftBuffer[i]);
                    }
                }

                // Perform inverse FFT
                FastFourierTransform.FFT(false, fftBuffer, false);

                lock (lockObject)
                {
                    for (int i = 0; i < fftSize; i++)
                    {
                        outputBuffer[i] += (float)fftBuffer[i].Real * window[i] / (fftSize / hopSize);
                    }

                    outputBufferCount = hopSize;
                    outputBufferPosition = 0;

                    for (int i = 0; i < fftSize - hopSize; i++)
                    {
                        outputBuffer[i] = outputBuffer[i + hopSize];
                    }

                    Array.Clear(outputBuffer, fftSize - hopSize, hopSize);
                }
            }
            catch (Exception ex)
            {
                logger.LogException("ProcessFrame", ex);
                throw;
            }
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Disposes resources used by the PitchShiftingSampleProvider.
        /// </summary>
        public void Dispose()
        {
            if (!isDisposed)
            {
                try
                {
                    // Clear buffers
                    Array.Clear(inputBuffer, 0, inputBuffer.Length);
                    Array.Clear(outputBuffer, 0, outputBuffer.Length);
                    Array.Clear(lastInputPhase, 0, lastInputPhase.Length);
                    Array.Clear(lastOutputPhase, 0, lastOutputPhase.Length);

                    // Log disposal
                    logger?.LogMessage("PitchShiftingSampleProvider disposed");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error during disposal: {ex}");
                }
                finally
                {
                    isDisposed = true;
                }
            }
        }

        #endregion

        #region Error Logger

        /// <summary>
        /// Simple error logger for the PitchShiftingSampleProvider.
        /// </summary>
        private class ErrorLogger
        {
            private readonly string logFilePath;
            private static readonly object lockObject = new object();

            public ErrorLogger()
            {
                try
                {
                    // Create logs directory if it doesn't exist
                    string directory = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "VocalFry", "Logs");

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    // Create log file with timestamp
                    logFilePath = Path.Combine(directory, $"PitchShifter_{DateTime.Now:yyyyMMdd_HHmmss}.log");

                    // Initialize log file with header
                    File.WriteAllText(logFilePath, $"=== PitchShiftingSampleProvider Log Started at {DateTime.Now} ===\r\n");
                }
                catch
                {
                    // Fallback to desktop if we can't create the log directory
                    try
                    {
                        logFilePath = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                            $"PitchShifter_Log_{DateTime.Now:yyyyMMdd_HHmmss}.log");
                    }
                    catch
                    {
                        // Last resort - just use Debug output
                        logFilePath = null;
                    }
                }
            }

            public void LogMessage(string message)
            {
                try
                {
                    if (logFilePath != null)
                    {
                        lock (lockObject)
                        {
                            File.AppendAllText(logFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] INFO: {message}\r\n");
                        }
                    }
                    Debug.WriteLine($"[INFO] {message}");
                }
                catch { /* Ignore logging failures */ }
            }

            public void LogException(string context, Exception ex)
            {
                try
                {
                    string message = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] EXCEPTION in {context}:\r\n" +
                                    $"Message: {ex.Message}\r\n" +
                                    $"Type: {ex.GetType().FullName}\r\n" +
                                    $"Stack Trace: {ex.StackTrace}\r\n";

                    if (ex.InnerException != null)
                    {
                        message += $"Inner Exception: {ex.InnerException.Message}\r\n" +
                                  $"Inner Stack Trace: {ex.InnerException.StackTrace}\r\n";
                    }

                    message += "------------------------------------\r\n";

                    if (logFilePath != null)
                    {
                        lock (lockObject)
                        {
                            File.AppendAllText(logFilePath, message);
                        }
                    }
                    Debug.WriteLine(message);
                }
                catch { /* Ignore logging failures */ }
            }
        }

        #endregion
    }
}