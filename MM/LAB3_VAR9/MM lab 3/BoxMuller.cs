using System;
using System.Collections.Generic;

namespace MM_lab_3
{
    public class BoxMuller
    {
        public int Count { get; set; }

        public List<double> GetList()
        {
            List<double> result = new List<double>();

            for (int i = 0; i < Count / 2; i++)
            {
                double x = Get(),
                    y = Get(),
                    s = GetS(x, y);
                while (s > 1 || s == 0)
                {
                    x = Get();
                    y = Get();
                    s = GetS(x,y);
                }
                result.Add(x * Math.Sqrt(-2 * Math.Log(s) / s));
                result.Add(y * Math.Sqrt(-2 * Math.Log(s) / s));
            }
            return result;
        }

        private double GetS(double x, double y)
        {
            return Math.Pow(x, 2) + Math.Pow(y, 2);
        }

        private double Get()
        {
            return BSV.Random() * 2 - 1;
        }
        public BoxMuller(int count)
        {
            Count = count;
        }
    }
}
