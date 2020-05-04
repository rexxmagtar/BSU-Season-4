using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using MATMODELAB1;

namespace Lab3Krypt
{
    class Program
    {

        static void Main(string[] args)
        {

            long M = 217728;
            long c = 45989;
            long a = 84589;

            List<int> binarySequence = Computer.LKMBinary(12, a, c, M, 1000000);


            long M1 = 217728;
            long c1 = 45989;
            long a1 = 84589;

            long M2 = 21773;
            long c2 = 4531233;
            long a2 = 23;

            List<float> gen1 = Computer.MKM(a1, c1, M1, 1050000);
            List<float> gen2 = Computer.MKM(a2, c2, M2, 1050000);

            List<float> points = Computer.MMM(500, 1000000, gen1, gen2);

            List<int> MMMBinarysequence = new List<int>();

            for (int i = 0; i < 1000000; i++)
            {
                MMMBinarysequence.Add(points[i] > 0.5 ? 1 : 0);
            }

            for (int i = 0; i < 1000000; i++)
            {
                binarySequence[i] = 1;
            }

            double Pvalue = TestLongestRunOfOnesInaBlock(binarySequence);


            StreamReader reader = new StreamReader("e.txt");
            char symbol = '1';

            Console.WriteLine($"P Linear  = {Pvalue}");

            binarySequence = new List<int>();

            string buffer = reader.ReadLine();

            for (int i = 0; i < buffer.Length; i++)
            {
                binarySequence.Add(int.Parse(buffer[i].ToString()));
            }
            Pvalue = TestLongestRunOfOnesInaBlock(binarySequence);

            Console.WriteLine($"P from text = {Pvalue}");

            Pvalue = TestLongestRunOfOnesInaBlock(MMMBinarysequence);

            Console.WriteLine($"P MM = {Pvalue}");

             binarySequence = new List<int>();

            Random rng = new Random(2019);

            for (int i = 0; i < 1000000; i++)
            {
                binarySequence.Add(rng.NextDouble() > 0.5 ? 1 : 0);
            }

            Pvalue = TestLongestRunOfOnesInaBlock(binarySequence);
            Console.WriteLine($"P (c# Random) = {Pvalue}");
        }

        static double TestLongestRunOfOnesInaBlock(List<int> sequence)
        {
            int N = 75;
            int K = 6;
            int M = 10000;

            int blockCount = sequence.Count / M;

            int[] vFrequency = new int[7];
            double[] vPi = new double[7];

            List<int> maxBlocksOneSequenceCounts = new List<int>();

            int currentIndex = 0;

            for (int i = 0; i < blockCount; i++)
            {
                int onesCount = 0;
                int onesMaxCount = int.MinValue;

                for (int j = 0; j < M; j++)
                {
                    if (sequence[currentIndex] == 1)
                    {
                        onesCount++;
                    }
                    else
                    {
                        onesMaxCount = Math.Max(onesCount, onesMaxCount);
                        onesCount = 0;
                    }
                    currentIndex++;
                }

                maxBlocksOneSequenceCounts.Add(Math.Max( onesMaxCount,onesCount));
            }


            vFrequency[0] = maxBlocksOneSequenceCounts.FindAll(number => number <= 10).Count;
            vFrequency[1] = maxBlocksOneSequenceCounts.FindAll(number => number == 11).Count;
            vFrequency[2] = maxBlocksOneSequenceCounts.FindAll(number => number == 12).Count;
            vFrequency[3] = maxBlocksOneSequenceCounts.FindAll(number => number == 13).Count;
            vFrequency[4] = maxBlocksOneSequenceCounts.FindAll(number => number == 14).Count;
            vFrequency[5] = maxBlocksOneSequenceCounts.FindAll(number => number == 15).Count;
            vFrequency[6] = maxBlocksOneSequenceCounts.FindAll(number => number >= 16).Count;

            vPi[0] = 0.0882f;
            vPi[1] = 0.2092f;
            vPi[2] = 0.2483f;
            vPi[3] = 0.1933f;
            vPi[4] = 0.1208f;
            vPi[5] = 0.0675f;
            vPi[6] = 0.0727f;

            double obs = 0;
            for (int i = 0; i <= K; i++)
            {
                obs += (double)Math.Pow((vFrequency[i] - N * vPi[i]), 2) / N * vPi[i];
            }

            double pvalue = (double)alglib.incompletegammac(K / 2.0f, obs / 2);

            return pvalue;

        }
    }
}
