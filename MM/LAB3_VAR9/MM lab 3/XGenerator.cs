using System.Collections.Generic;
using System.Linq;

namespace MM_lab_3
{
    public class XGenerator
    {
        private int fredomDegree;
        private double[] randomValues;

        public void GetRandomArray(int count)
        {
            int size = fredomDegree * count;
            randomValues = new double[size];
            NormalDistributionGenerator normalDistributionGenerator = new NormalDistributionGenerator(48, 0.0d, 1.0d, size);
            for (int i = 0; i < size; i++)
                randomValues[i] = normalDistributionGenerator.GetRandom(i);
        }

        public double GetRandom(int index)
        {
            return Count(GetSubList(index));
        }

        private List<double> GetSubList(int index)
        {
            int start = 4 * index,
                finish = 4 * (1 + index);
            List<double> subRandValues = new List<double>();
            for (int i = start, j = 0; i < finish; i++, j++)
                subRandValues.Add(randomValues[i]);
            return subRandValues;
        }

        private double Count(List<double> randVal) => randVal.Sum(x => x * x);

        public XGenerator(int fredomDegree)
        {
            this.fredomDegree = fredomDegree;
        }
    }
}
