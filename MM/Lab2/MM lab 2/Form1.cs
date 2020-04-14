using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MM_lab_2
{
    public partial class Form1 : Form
    {
        int index = 0;
        Bi ftbi;
        Puassons ftp;
        Geo ftg;
        NoBi ftnb;
        List<double> ftbiList,
            ftpList,
            ftgList,
            ftnbList;

        public Form1()
        {
            InitializeComponent();
            BTASK2.Visible = true;
            BTASK2.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
        }

        private void DoBinomO()
        {
            CurrentFunctionLable.Text = "обратный биноминальный (5,0.6)";
            ftnb = new NoBi(5, 0.6);
            double aproxAverNB, aproxDispNB;
            ftnbList = ftnb.GenerataRandomList(1000);
            RealE.Text = ftnb.CountAverage().ToString();
            RealD.Text = ftnb.CountDispersion().ToString();

            aproxAverNB = ftnbList.Average();
            aproxDispNB = SumS2(ftnbList, aproxAverNB);

            ResultE.Text = aproxAverNB.ToString();
            ResultD.Text = aproxDispNB.ToString();
            RealAssymetry.Text = (GetCentralNMoment(ftnbList, 3) /
                          Math.Pow(Math.Sqrt(SumS2(ftnbList, ftnb.CountAverage())), 3))
                .ToString();
            ResultAssymetry.Text = ((2 - ftnb.GetP()) /
                           Math.Sqrt(ftnb.GetR() * (1 - ftnb.GetP()))).ToString();
            RealExces.Text = ((GetCentralNMoment(ftnbList, 4) /
                Math.Pow(SumS2(ftnbList, ftnb.CountAverage()), 2) - 3)).ToString();
            ResultExces.Text = (6 / ftnb.GetR() +
                          Math.Pow(ftnb.GetP(), 2) / (ftnb.GetP() * ftnb.GetR())).ToString();


            for (int i = 1; i <= 15; i++)
            {
                BTASK2.Series[0].Points.AddY((from temp
                        in ftnbList
                    where temp == i
                    select temp).Count());
                BTASK2.Series[1].Points.AddY(NBProbability(i) * ftnbList.Count);
            }
        }

        private void DoGeometry()
        {
            CurrentFunctionLable.Text = "Геометрический (0.25)";
            ftg = new Geo(0.25);
            double aproxAverG, aproxDispG;
            ftgList = ftg.GenerataRandomList(1000);
            RealE.Text = ftg.CountAverage().ToString();
            RealD.Text = ftg.CountDispersion().ToString();

            aproxAverG = ftgList.Average();
            aproxDispG = SumS2(ftgList, aproxAverG);

            ResultE.Text = aproxAverG.ToString();
            ResultD.Text = aproxDispG.ToString();
            RealAssymetry.Text = (GetCentralNMoment(ftgList, 3) /
                         Math.Pow(Math.Sqrt(SumS2(ftgList, ftg.CountAverage())), 3))
                .ToString();
            ResultAssymetry.Text = ((2 - 1 / ftg.CountAverage()) /
                          (Math.Sqrt(1 - 1 / ftg.CountAverage()))).ToString();
            RealExces.Text = ((GetCentralNMoment(ftgList, 4) /
                Math.Pow(SumS2(ftgList, ftg.CountAverage()), 2) - 3)).ToString();
            ResultExces.Text = (6 +
                (Math.Pow(1 / ftg.CountAverage(), 2))
                / 1 - (1 / ftg.CountAverage())).ToString();

            for (int i = 1; i <= 15; i++)
            {
                BTASK2.Series[0].Points.AddXY(i,(from temp
                        in ftgList
                    where temp == i
                    select temp).Count());
                BTASK2.Series[1].Points.AddXY(i,GeoProbability(i) * ftgList.Count);

            }
        }

        private void DoPuasson()
        {
            CurrentFunctionLable.Text = "Пуасона (3)";
            ftp = new Puassons(3);
            double aproxAverP, aproxDispP;
            ftpList = ftp.GenerataRandomList(1000);
             RealE.Text =  ftp.CountAverage().ToString();
             RealD.Text =  ftp.CountDispersion().ToString();

            aproxAverP = ftpList.Average();
            aproxDispP = SumS2(ftpList, aproxAverP);

             ResultE.Text = aproxAverP.ToString();
            ResultD.Text = aproxDispP.ToString();
           RealAssymetry.Text = (GetCentralNMoment(ftpList, 3) /
                         Math.Pow(Math.Sqrt(SumS2(ftpList, ftp.CountAverage())), 3))
                .ToString();
            ResultAssymetry.Text = (Math.Pow(ftp.CountAverage(), -1 / 2.0)).ToString();
            RealExces.Text = ((GetCentralNMoment(ftpList, 4) /
                Math.Pow(SumS2(ftpList, ftp.CountAverage()), 2) - 3)).ToString();
            ResultExces.Text = (1 / ftp.CountAverage()).ToString();


            for (int i = 0; i < 15; i++)
            {
                BTASK2.Series[0].Points.AddXY(i,(from temp
                        in ftpList
                    where temp == i
                    select temp).Count());
                BTASK2.Series[1].Points.AddXY(i,PusassonProbability(i) * ftpList.Count);

            }

        }

        private void DoBinom()
        {
            CurrentFunctionLable.Text = "Биноминальный (0.75)";
            ftbi = new Bi(0.75);
            double aproxAverBi, aproxDispBi;
            ftbiList = ftbi.GenerataRandomList(1000);

            aproxAverBi = ftbiList.Average();
            aproxDispBi = SumS2(ftbiList, aproxAverBi);

            ResultE.Text = aproxAverBi.ToString(); 
            ResultD.Text = aproxDispBi.ToString(); 

            RealE.Text = ftbi.CountAverage().ToString();
            RealD.Text = ftbi.CountDispersion().ToString();

            ResultAssymetry.Text = (GetCentralNMoment(ftbiList, 3) /
                         Math.Pow(Math.Sqrt(SumS2(ftbiList, ftbi.CountAverage())), 3))
                .ToString();
            RealAssymetry.Text = ((1 - 2 * ftbi.CountAverage()) /
                          Math.Sqrt((1 - ftbi.CountAverage()) * ftbi.CountAverage())).ToString();
            ResultExces.Text = (GetCentralNMoment(ftbiList, 4) /
                Math.Pow(SumS2(ftbiList, ftbi.CountAverage()), 2) - 3).ToString();
            RealExces.Text = ((1 - 6 * (1 - ftbi.CountAverage()) * ftbi.CountAverage())
                         / (ftbi.CountAverage() * (1 - ftbi.CountAverage()))).ToString();


            int zeroes = (from temp
                    in ftbiList
                where temp == 0
                select temp).Count();

            BTASK2.Series[0].Points.AddXY(0,zeroes);
            BTASK2.Series[1].Points.AddXY(0,ftbiList.Count * 0.25);
            BTASK2.Series[0].Points.AddXY(1,ftbiList.Count - zeroes);
            BTASK2.Series[1].Points.AddXY(1,ftbiList.Count * 0.75);
        }

        public static double SumS2(List<double> bsvS, double m)
        {
            double sum = 0;
            for (int i = 0; i < bsvS.Count; i++)
                sum += Math.Pow(bsvS[i] - m, 2);
            return sum / (bsvS.Count - 1);
        }

        public double GetCentralNMoment(List<double> randomList, int sizeOfMoment)
        {
            double result = 0,
                n = randomList.Count,
                average = randomList.Average();

            for (int i = 0; i < n; i++)
                result += Math.Pow(randomList[i] - average, sizeOfMoment);

            result /= n;

            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BTASK2.Series[0].Points.Clear();
            BTASK2.Series[1].Points.Clear();

            switch (index)
            {
                case 0:
                    DoBinom();
                    break;
                case 1:
                 DoPuasson();
                    break;
                case 2:
                   DoGeometry();
                    break;
                case 3:
                    DoBinomO();
                    break;

            }

            index = (index + 1) % 4;
        }

        private double PusassonProbability(int k)
        {
            return Math.Exp(-ftp.CountAverage()) * Math.Pow(ftp.CountAverage(), k) / GetFactorial(k);
        }

        private void BTASK2_Click(object sender, EventArgs e)
        {

        }

        private double GeoProbability(int n)
        {
            return Math.Pow(1 - 1 / ftg.CountAverage(), n - 1) / ftg.CountAverage();
        }

        private double NBProbability(int k)
        {
            return MathNet.Numerics.SpecialFunctions.Gamma(ftnb.GetR() + k) *
                Math.Pow(ftnb.GetP(), ftnb.GetR()) *
                Math.Pow((1 - ftnb.GetP()), k) /
                GetFactorial(k) /
                MathNet.Numerics.SpecialFunctions.Gamma(ftnb.GetR());
        }

        private long GetFactorial(int num)
        {
            long start = 1;

            for (int i = 2; i <= num; i++)
            {
                start *= i;
            }

            return start;
        }
    }
}
