using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM_lab_2
{
    class Bi
    {
        private BSV random = new BSV(209, 8327, 2483648);
        private double p;

        public Bi(double p)
        {
            this.p = p;
        }

        public List<double> GenerataRandomList(int arraySize)
        {
            List<double> result = new List<double>();

            for (int i = 0; i < arraySize; i++)
            {
                int x = p - random.Random() > 0 ? 1 : 0;
                result.Add(x);
            }

            return result;
        }

        public double CountAverage()
        {
            return p;
        }

        public double CountDispersion()
        {
            return p * (1 - p);
        }
    }
}
