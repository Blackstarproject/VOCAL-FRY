using NAudio.Wave;
using System;

namespace Vocal_Fry
{
    /// <summary>
    /// Applies a simple distortion effect to an audio stream.
    /// </summary>
    public class DistortionSampleProvider : ISampleProvider
    {
        private readonly ISampleProvider sourceProvider;

        public WaveFormat WaveFormat => sourceProvider.WaveFormat;

        /// <summary>
        /// The amount of gain to apply before clipping. Values greater than 1.0 will cause distortion.
        /// </summary>
        public float Gain { get; set; } = 1.0f;

        /// <summary>
        /// The mix between the dry (original) and wet (distorted) signal.
        /// 0.0 is fully dry, 1.0 is fully wet. For a demonic effect, a high value is recommended.
        /// </summary>
        public float Mix { get; set; } = 1.0f;

        /// <summary>
        /// Initializes a new instance of the DistortionSampleProvider class.
        /// </summary>
        /// <param name="sourceProvider">The input audio stream to distort.</param>
        public DistortionSampleProvider(ISampleProvider sourceProvider)
        {
            this.sourceProvider = sourceProvider ?? throw new ArgumentNullException(nameof(sourceProvider));
        }

        /// <summary>
        /// Reads audio samples from the provider, applying the distortion effect.
        /// </summary>
        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = sourceProvider.Read(buffer, offset, count);

            for (int i = 0; i < samplesRead; i++)
            {
                // Get the original sample
                float drySample = buffer[offset + i];

                // Apply gain to create distortion
                float wetSample = drySample * Gain;

                // Use the tanh function for a slightly smoother, "warmer" clipping
                wetSample = (float)Math.Tanh(wetSample);

                // Mix the distorted signal with the original
                buffer[offset + i] = (wetSample * Mix) + (drySample * (1.0f - Mix));
            }

            return samplesRead;
        }
    }
}
