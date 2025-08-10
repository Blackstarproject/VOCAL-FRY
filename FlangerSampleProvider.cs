using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocal_Fry
{
    /// <summary>
    /// Applies a flanger effect to an audio stream.
    /// The effect is created by mixing a signal with a slightly delayed copy of itself,
    /// where the delay time is modulated by a low-frequency oscillator (LFO).
    /// </summary>
    public class FlangerSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider sourceProvider;
        private readonly float[] delayBuffer;
        private int delayPosition;
        private float lfoPhase;

        public WaveFormat WaveFormat => sourceProvider.WaveFormat;

        // --- Flanger Parameters ---

        /// <summary>
        /// The rate (in Hz) of the LFO that modulates the delay time.
        /// Controls the "speed" of the swooshing effect.
        /// </summary>
        public float Rate { get; set; } = 0.5f;

        /// <summary>
        /// The depth (in milliseconds) of the delay modulation.
        /// Controls the "width" of the swooshing effect.
        /// </summary>
        public float Depth { get; set; } = 5.0f;

        /// <summary>
        /// The amount of processed signal fed back into the delay line.
        /// Creates a more resonant, intense effect.
        /// </summary>
        public float Feedback { get; set; } = 0.6f;

        /// <summary>
        /// The mix between the dry (original) and wet (flanged) signal.
        /// 0.0 is fully dry, 1.0 is fully wet.
        /// </summary>
        public float Mix { get; set; } = 0.5f;

        /// <summary>
        /// Initializes a new instance of the FlangerSampleProvider class.
        /// </summary>
        /// <param name="sourceProvider">The input audio stream.</param>
        /// <param name="maxDelayMilliseconds">The maximum possible delay. Should be greater than the depth.</param>
        public FlangerSampleProvider(ISampleProvider sourceProvider, int maxDelayMilliseconds = 20)
        {
            this.sourceProvider = sourceProvider;

            // Create a buffer large enough for the maximum delay
            int delayBufferSize = (int)(WaveFormat.SampleRate * (maxDelayMilliseconds / 1000.0));
            delayBuffer = new float[delayBufferSize];
        }

        /// <summary>
        /// Reads audio samples from the provider, applying the flanger effect.
        /// </summary>
        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = sourceProvider.Read(buffer, offset, count);

            float lfoIncrement = (2.0f * (float)Math.PI * Rate) / WaveFormat.SampleRate;
            float baseDelay = (Depth / 1000.0f) * WaveFormat.SampleRate;

            for (int i = 0; i < samplesRead; i++)
            {
                float drySample = buffer[offset + i];

                // Calculate the LFO value (sine wave)
                float lfoValue = (float)Math.Sin(lfoPhase);
                lfoPhase += lfoIncrement;
                if (lfoPhase >= 2.0f * Math.PI) lfoPhase -= 2.0f * (float)Math.PI;

                // Modulate the delay time
                float currentDelay = baseDelay * (1.0f + lfoValue) / 2.0f;

                // Get the delayed sample using linear interpolation for fractional delays
                float delayedSample = GetDelayedSample(currentDelay);

                // Store the new sample in the delay buffer with feedback
                delayBuffer[delayPosition] = drySample + (delayedSample * Feedback);

                // Mix the dry and wet signals
                buffer[offset + i] = (drySample * (1.0f - Mix)) + (delayedSample * Mix);

                // Move to the next position in the circular delay buffer
                delayPosition++;
                if (delayPosition >= delayBuffer.Length)
                {
                    delayPosition = 0;
                }
            }
            return samplesRead;
        }

        /// <summary>
        /// Gets a sample from the delay buffer at a given delay time, using linear interpolation.
        /// </summary>
        private float GetDelayedSample(float delay)
        {
            // Find the two surrounding integer sample positions
            int index1 = (int)Math.Floor(delay);
            int index2 = index1 + 1;
            float fraction = delay - index1;

            // Get the corresponding positions in the circular buffer
            int bufferIndex1 = (delayPosition - index1 + delayBuffer.Length) % delayBuffer.Length;
            int bufferIndex2 = (delayPosition - index2 + delayBuffer.Length) % delayBuffer.Length;

            // Get the sample values at those positions
            float sample1 = delayBuffer[bufferIndex1];
            float sample2 = delayBuffer[bufferIndex2];

            // Linearly interpolate between the two samples
            return sample1 + (sample2 - sample1) * fraction;
        }
    }
}
