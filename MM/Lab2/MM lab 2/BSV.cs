using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM_lab_2
{
    class BSV
    {
        private long alpha;
        private long beta;
        private long M;

        public BSV(long a, long b, long M)
        {
            alpha = a;
            beta = b;
            this.M = M;
        }

        public double Random()
        {
            alpha = (beta * alpha) % M;
            return (double)alpha / M;
        }
    }
}
