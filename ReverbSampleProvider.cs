using NAudio.Wave;

namespace Vocal_Fry
{
    /// <summary>
    /// Applies a simple reverb/echo effect using a feedback delay.
    /// </summary>
    public class ReverbSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider sourceProvider;
        private readonly float[] delayBuffer;
        private int delayPosition;

        public WaveFormat WaveFormat => sourceProvider.WaveFormat;

        /// <summary>
        /// The amount of signal to feed back into the delay line. Determines the length of the "tail".
        /// Recommended values are between 0.0 and 0.9 to avoid runaway feedback.
        /// </summary>
        public float Feedback { get; set; } = 0.5f;

        /// <summary>
        /// The mix between the dry (original) and wet (reverb) signal.
        /// 0.0 is fully dry, 1.0 is fully wet.
        /// </summary>
        public float Mix { get; set; } = 0.5f;

        /// <summary>
        /// Initializes a new instance of the ReverbSampleProvider class.
        /// </summary>
        /// <param name="sourceProvider">The input audio stream.</param>
        /// <param name="delayMilliseconds">The delay time in milliseconds, affecting the perceived "room size".</param>
        public ReverbSampleProvider(ISampleProvider sourceProvider, int delayMilliseconds = 200)
        {
            this.sourceProvider = sourceProvider;
            // Calculate the buffer size based on sample rate and delay time
            int delaySamples = (int)(WaveFormat.SampleRate * (delayMilliseconds / 1000.0));
            delayBuffer = new float[delaySamples];
        }

        /// <summary>
        /// Reads audio samples from the provider, applying the reverb effect.
        /// </summary>
        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = sourceProvider.Read(buffer, offset, count);

            for (int i = 0; i < samplesRead; i++)
            {
                float drySample = buffer[offset + i];
                float delayedSample = delayBuffer[delayPosition];

                // Mix the original signal with the delayed signal
                float mixedSample = (drySample * (1.0f - Mix)) + (delayedSample * Mix);

                // Store the new sample in the delay buffer with feedback
                delayBuffer[delayPosition] = drySample + (delayedSample * Feedback);

                // Output the mixed sample
                buffer[offset + i] = mixedSample;

                // Move to the next position in the circular delay buffer
                delayPosition++;
                if (delayPosition >= delayBuffer.Length)
                {
                    delayPosition = 0;
                }
            }
            return samplesRead;
        }
    }
}
