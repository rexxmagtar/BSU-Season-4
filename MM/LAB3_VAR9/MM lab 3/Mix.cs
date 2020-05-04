namespace MM_lab_3
{
    public class Mix
    {
        XGenerator XGenerator;
        LaplasGenerator LaplasGener;

        public Mix(int m, int a)
        {
            XGenerator = new XGenerator(m);
            XGenerator.GetRandomArray(10000);
            LaplasGener = new LaplasGenerator(a);
        }

        public double GetRandom(int i)
        {
            double bsv = BSV.Random();

            return bsv >= 0.5 ?
                LaplasGener.GetRandom() :
                XGenerator.GetRandom(i);
        }
    }
}
