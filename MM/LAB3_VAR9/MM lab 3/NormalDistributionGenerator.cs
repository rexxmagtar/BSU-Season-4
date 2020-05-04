using System;
using System.Collections.Generic;
using System.Linq;

namespace MM_lab_3
{
    public class NormalDistributionGenerator
    {
        private int termCount;
        public double[] RandomValues { get; set; }
        public int RandomValuesCount { get; set; }
        public double Expectation { get; set; }
        public double Dispersion { get; set; }
        public int TermCount {
            get
            {
                return termCount;
            }
            set
            {
                termCount = value;
                CreateRandArr(termCount);
            }
        }

        public double[] GetRandomArray(int index)
        {
            int from = TermCount * index,
                to = TermCount * (1 + index);
            double[] subRandValues = new double[TermCount];

            for (int i = from, j = 0; i < to; i++, j++)
                subRandValues[j] = GetRandomValues()[i];

            return subRandValues;
        }
        public double[] GetRandomValues()
        {
            return RandomValues;
        }

        private void CreateRandArr(int count)
        {
            int realSize = count * RandomValuesCount;
           
            RandomValues = new double[realSize];

            for (int i = 0; i < realSize; i++)
                RandomValues[i] = BSV.Random();
        }
        public double GetRandom(int index)
        {
            double[] subRandValues = GetRandomArray(index);
            double sumOfTerms = GetSumOfTerms(subRandValues);

            return (sumOfTerms - GetSumOfExpect(TermCount))
                / Math.Sqrt(TermCount / 12.0)
                * Math.Sqrt(Dispersion)
                + Expectation;
        }

        public double GetSumOfExpect(int count) => 0.5 * count;

        public double GetSumOfTerms(double[] randValues)
        {
            return randValues.Sum();
        }

        public double SumS2(List<double> bsvS, double m)
        {
            double sum = 0;
            for (int i = 0; i < bsvS.Count; i++)
                sum += Math.Pow(bsvS[i] - m, 2);
            return sum / (bsvS.Count - 1);
        }

        public NormalDistributionGenerator(int termCount, double m, double sSqr, int randValCount)
        {
            RandomValuesCount = randValCount;
            TermCount = termCount;
            Expectation = m;
            Dispersion = sSqr;
        }
    }
}
