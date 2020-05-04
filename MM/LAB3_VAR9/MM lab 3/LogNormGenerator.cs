using System;

namespace MM_lab_3
{
    public class LogNormGenerator
    {
        private NormalDistributionGenerator normalDistribution;

        public LogNormGenerator(int termCount, double m, double sSqr, int count)
        {
            normalDistribution = new NormalDistributionGenerator(termCount, m, sSqr, count);
        }

        public double GetRandom(int index)
        {
            return Math.Exp(normalDistribution.GetRandom(index));
        }
    }
}
