using System;

namespace MM_lab_3
{
    public class CoshiGenerator
    {
        public int A { get; set; }
        public int B { get; set; }

        public double GetRandom()
        {
            return A - B * Math.Tan(Math.PI * (BSV.Random() - 0.5));
        }

        public CoshiGenerator(int a, int b)
        {
            A = a;
            B = b;
        }
    }
}
