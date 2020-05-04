using System;

namespace MM_lab_3
{
    public class LaplasGenerator
    {
        public int Lambda { get; set; }

        public LaplasGenerator(int lambda)
        {
            Lambda = lambda;
        }

        public double GetRandom()
        {
            double temp = BSV.Random();

            if (temp >= 0.5)
                return -1.0 / Lambda * Math.Log(2 * (1 - temp));
            return 1.0 / Lambda * Math.Log(2 * temp);
        }
    }
}
