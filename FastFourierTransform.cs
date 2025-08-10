
using System;
using Complex = System.Numerics.Complex;

namespace Vocal_Fry
{
    public static class FastFourierTransform
    {
        /// <summary>
        /// Performs Fast Fourier Transform on complex data
        /// </summary>
        /// <param name="inPlace">Whether to perform the transform in-place (always true in this implementation)</param>
        /// <param name="data">Complex data array to transform</param>
        /// <param name="forward">True for forward FFT, false for inverse FFT</param>
        public static void FFT(bool inPlace, Complex[] data, bool forward)
        {
            int n = data.Length;
            if ((n & (n - 1)) != 0)
                throw new ArgumentException("Data length must be a power of 2.", nameof(data));

            int numBits = (int)Math.Log(n, 2);

            // Bit reversal permutation
            for (int i = 0; i < n; i++)
            {
                int j = BitReverse(i, numBits);
                if (j > i)
                {
                    Swap(ref data[i], ref data[j]);
                }
            }

            // Cooley-Tukey FFT algorithm
            for (int len = 2; len <= n; len <<= 1)
            {
                double angle = (forward ? -2.0 : 2.0) * Math.PI / len;
                Complex wlen = new Complex(Math.Cos(angle), Math.Sin(angle));

                for (int i = 0; i < n; i += len)
                {
                    Complex w = 1.0;
                    for (int j = 0; j < len / 2; j++)
                    {
                        Complex u = data[i + j];
                        Complex v = data[i + j + len / 2] * w;
                        data[i + j] = u + v;
                        data[i + j + len / 2] = u - v;
                        w *= wlen;
                    }
                }
            }

            // Scale if inverse FFT
            if (!forward)
            {
                for (int i = 0; i < n; i++)
                {
                    data[i] /= n;
                }
            }
        }

        /// <summary>
        /// Reverses the bits of a number
        /// </summary>
        private static int BitReverse(int x, int numBits)
        {
            int reversed = 0;
            for (int i = 0; i < numBits; i++)
            {
                reversed = (reversed << 1) | (x & 1);
                x >>= 1;
            }
            return reversed;
        }

        /// <summary>
        /// Swaps two complex numbers
        /// </summary>
        private static void Swap(ref Complex a, ref Complex b)
        {
            (b, a) = (a, b);
        }
    }
}
