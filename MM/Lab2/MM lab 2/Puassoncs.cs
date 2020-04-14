using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM_lab_2
{
    class Puassons 
    {
        private BSV random = new BSV(146051657, 1928884637, 2147483648);
        private double lambda;

        public Puassons(double lambda)
        {
            this.lambda = lambda;
        }

        public double CountAverage()
        {
            return lambda;
        }

        public double CountDispersion()
        {
            return lambda;
        }

        public List<double> GenerataRandomList(int arraySize)
        {
            List<double> result = new List<double>();

            for (int i = 0; i < arraySize; i++)
            {
                int iteration = 0;
                double x = random.Random();
                while (x >= Math.Pow(Math.E, -lambda))
                {
                    x *= random.Random();
                    ++iteration;
                }

                result.Add(iteration);
            }

            return result;
        }
    }
}
