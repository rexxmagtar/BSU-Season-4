using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MM_lab_3
{
    public partial class Form1 : Form
    {
        public List<double> randsOfNormDistrib = new List<double>();
        public List<double> randsOfHiSqr = new List<double>();
        public List<double> randsOfLaplas = new List<double>();
        public List<double> randsOfLogNorm = new List<double>();
        public List<double> randsOfCoshi = new List<double>();
        public List<double> randsOfMix = new List<double>();

        public static List<double> standartRandsOfNormDistrib = new List<double>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Start_Click(object sender, EventArgs e)
        {
            NormalDistributionGenerator randGener = new NormalDistributionGenerator(48, 0.0d, 64.0d, 10000);
            for (int i = 0; i < 10000; i++)
                randsOfNormDistrib.Add(randGener.GetRandom(i));
            ExpectText.Text = randGener.Expectation.ToString();
            FabricExpectText.Text = randsOfNormDistrib.Average().ToString();
            DispText.Text = randGener.Dispersion.ToString();
            FabricDispText.Text = SumS2(randsOfNormDistrib,
                randsOfNormDistrib.Average()).ToString();

            //////////////////////////////////////////////////////////////

            XGenerator hiSqrGener = new XGenerator(4);

            hiSqrGener.GetRandomArray(10000);

            for (int i = 0; i < 10000; i++)
                randsOfHiSqr.Add(hiSqrGener.GetRandom(i));

            FHiExText.Text = randsOfHiSqr.Average().ToString();
            HiExText.Text = "4";
            FHiDispText.Text = SumS2(randsOfHiSqr,
                randsOfHiSqr.Average()).ToString();
            HiDispText.Text = "8";

            ////////////////////////////////////////////////////////////////

            LaplasGenerator laplasGener = new LaplasGenerator(2);

            for (int i = 0; i < 10000; i++)
                randsOfLaplas.Add(laplasGener.GetRandom());

            FabLaplasExTB.Text = randsOfLaplas.Average().ToString();
            LaplasExTB.Text = "0";
            FabLaplasDispTB.Text = SumS2(randsOfLaplas,
                randsOfLaplas.Average()).ToString();
            LaplasDispTB.Text = (2.0 / laplasGener.Lambda / 
                laplasGener.Lambda).ToString();

            //////////////////////////////////////////////////////////////

            Mix mix = new Mix(4, 2);
            for(int i=0; i<10000; i++)
            {
                randsOfMix.Add(mix.GetRandom(i));
            }
            MixAverage.Text = randsOfMix.Average().ToString();
            MixDisp.Text = SumS2(randsOfMix, randsOfMix.Average()).ToString();
            MixTrueEx.Text = (0.5 * (randsOfHiSqr.Average() + randsOfLaplas.Average())).ToString();
            MixDispTrue.Text = (0.5 * (SumS2(randsOfLaplas, randsOfLaplas.Average()) + Math.Pow(randsOfLaplas.Average(), 2) - Math.Pow(randsOfMix.Average(), 2)) +
                 0.5 * (SumS2(randsOfHiSqr, randsOfHiSqr.Average()) + Math.Pow(randsOfHiSqr.Average(), 2) - Math.Pow(randsOfMix.Average(), 2))).ToString(); 
            //////////////////////////////////////////////////////////////

            LogNormGenerator logNormGener =
                new LogNormGenerator(48, 0.0d, 4.0d, 10000);

            for (int i = 0; i < 10000; i++)
                randsOfLogNorm.Add(logNormGener.GetRandom(i));

            FabLNormExTB.Text = randsOfLogNorm.Average().ToString();
            LNormExTB.Text = Math.Exp(2.0).ToString();
            LNormDispTB.Text = ((Math.Exp(4.0) - 1) *
                Math.Exp(4.0)).ToString();
            FabLNormDispTB.Text = SumS2(randsOfLogNorm,
                randsOfLogNorm.Average()).ToString();

            ///////////////////////////////////////////////////////////////

            CoshiGenerator coshiGener = new CoshiGenerator(1, 2);

            for (int i = 0; i < 10000; i++)
                randsOfCoshi.Add(coshiGener.GetRandom());
            randsOfCoshi.Sort();

            FabCoshiExTB.Text = randsOfCoshi[5000].ToString();
            CoshiExTB.Text = coshiGener.A.ToString();

            ///////////////////////////////////////////////////////////////

            BoxMuller box_MullerGener = new BoxMuller(10000);

            List<double> boxMullList = box_MullerGener.GetList();

            CorrTB.Text = GetCorr(boxMullList).ToString();
        }

        public static double SumS2(List<double> bsvS, double m)
        {
            double sum = 0;
            for (int i = 0; i < bsvS.Count; i++)
                sum += Math.Pow(bsvS[i] - m, 2);
            return sum / (bsvS.Count - 1);
        }

        public static List<double> GetSubList(int index)
        {
            int from = 4 * index,
                to = 4 * (1 + index);
            List<double> subRandValues = new List<double>();

            for (int i = from, j = 0; i < to; i++, j++)
                subRandValues.Add(standartRandsOfNormDistrib[i]);

            return subRandValues;
        }

        public static double CountHiSqr(List<double> randVal) => randVal.Sum(x => x * x);

        public static double GetCorr(List<double> boxMullList)
        {
            double _x = 0,
                _y = 0,
                numerator = 0,
                denominatorX = 0,
                denominatorY = 0;

            for (int i = 0; i < boxMullList.Count; i += 2)
            {
                _x += boxMullList[i];
                _y += boxMullList[i + 1];
            }
                
            _x /= boxMullList.Count;
            _y /= boxMullList.Count;

            for (int i = 0; i < boxMullList.Count; i += 2)
            {
                numerator += (boxMullList[i] - _x) * (boxMullList[i + 1] - _y);
                denominatorX += (boxMullList[i] - _x) * (boxMullList[i] - _x);
                denominatorY += (boxMullList[i + 1] - _y) * (boxMullList[i + 1] - _y);
            }

            return numerator / Math.Sqrt(denominatorX * denominatorY);
        }
    }
}
