using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace MATMODELAB1
{
    public class Computer
    {
        private long a;
        private long b;
        private long M;

        public Computer(int a, int b, int m)
        {
            this.a = a;
            this.b = b;
            M = m;
        }

        public static List<float> MKM(long a0, long b, long M, int size)
        {
            List<float> result = new List<float>();
            long a = a0;
            bool stop = false;

            while (!stop)
            {
                a = (a * b) - M * ((a * b) / M);
                result.Add((1.0f * a) / M);
                if (result.Count == size)
                {
                    Debug.WriteLine("Finished");
                    stop = true;
                }
            }

            return result;
        }

        public static List<int> LKMBinary(long X0, long a, long c, long M, int size)
        {
            List<int> result = new List<int>();
            long x = X0;
            bool stop = false;

            while (!stop)
            {
                x = (a * x + c) % M;
                result.Add(1.0* x/M > 0.5 ? 1 : 0);
                if (result.Count == size)
                {
                    Debug.WriteLine("Finished");
                    stop = true;
                }
            }

            return result;
        }


        public static List<int> MKMBinary(long a0, long b, long M, int size)
        {
            List<int> result = new List<int>();
            long a = a0;
            bool stop = false;

            while (!stop)
            {
                a = (a * b) - M * ((a * b) / M);

                result.Add(((1.0f * a) / M) < 0.5 ? 0 : 1);

                if (result.Count == size)
                {
                    //Debug.WriteLine("Finished");
                    stop = true;
                }
            }

            return result;
        }

        public static List<float> MMM(int K, int size, List<float> C, List<float> B)
        {

            List<float> V = new List<float>();
            List<float> result = new List<float>();
            bool stop = false;
            for (int i = 0; i < K; i++)
            {
                V.Add(B[i]);
            }

            while (!stop)
            {
                int s = (int)(C[result.Count] * K);
                float a = V[s];
                V[s] = B[result.Count + K];
                result.Add(a);
                if (result.Count == size)
                {
                    break;
                }
            }

            return result;

        }
    }


}