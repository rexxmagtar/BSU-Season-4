using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM_lab_2
{
    class Geo 
    {
        private BSV random = new BSV(143260, 4637, 2147483648);
        private double p;

        public Geo(double p)
        {
            this.p = p;
        }

        public double CountAverage()
        {
            return 1 / p;
        }

        public double CountDispersion()
        {
            return (1 - p) / (p * p);
        }

        public List<double> GenerataRandomList(int arraySize)
        {
            List<double> result = new List<double>();

            for (int i = 0; i < arraySize; i++)
            {
                result.Add(Math.Ceiling(Math.Log(random.Random()) /
                    Math.Log(1 - p)));
            }

            return result;
        }
    }   
}
