namespace MM_lab_3
{
    public class BSV
    {
        private const long alpha = 146051657;
        private const long beta = 1928884637;
        private const long m = 2147483648;

        public static double Random()
        {
            return (double)(beta * alpha % m) / m;
        }
    }
}
