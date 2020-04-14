using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM_lab_2
{
    class NoBi 
    {
        private BSV random = new BSV(14, 312317, 47483648);
        private int r; 
        private double p;

        public NoBi(int r, double p)
        {
            this.r = r;
            this.p = p;
        }

        public int GetR()
        {
            return r;
        }

        public double GetP()
        {
            return p;
        }

        public double CountAverage()
        {
            return r * (1 - p) / p;
        }

        public double CountDispersion()
        {
            return r * (1 - p) / (p * p);
        }

        public List<double> GenerataRandomList(int arraySize)
        {
            List<double> result = new List<double>();

            int less, more;

            for (int i = 0; i < arraySize; i++)
            {
                less = more = 0;

                while(less < r)
                {
                    double temp = random.Random();

                    if (temp < p)
                        less++;
                    else
                        more++;
                }

                result.Add(more);
            }

            return result;
        }
    }
}
