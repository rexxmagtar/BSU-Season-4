using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MATMODELAB1
{
    public partial class Form1 : Form
    {
        long b1 = 474379977;
        long M1 = 101;
        private long M2 = (long)Math.Pow(2, 20) + 12 * 10103;
        long a1 = 261463909;
            
        long b2 = 3097871;
        long a2 = 234289925;

        int K = 192;
        
        public Form1()
        {
         List<float> generator1 =   Computer.MKM(a1, b1, M1, 1000);
         List<float> generator2 = Computer.MKM(a2, b2, M2, 1000);

         
            
            InitializeComponent();

            MainDiagramm.Series["Generator1Points"].Color=Color.Red;

            MainDiagramm.Series["Generator2 points"].Color=Color.Blue;

            MainDiagramm.Series["Generator3 points"].Color = Color.Purple;

            MainDiagramm.Series["Generator4 points"].Color = Color.Green;



        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ResultBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }





        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void DrawGen1()
        {

        }

        private void DrawGen2()
        {

        }

        private void DrawGistGram(List<float> points, string seriaName, bool clearAll = false)
        {
            if (clearAll)
            {
                foreach (var seria in MainDiagramm.Series)
                {
                    seria.Points.Clear();
                }
            }

            for (int i = 1; i < points.Count; i++)
            {
                MainDiagramm.Series[seriaName].Points.AddXY(points[i - 1], points[i]);
            }

            
        }
    

        private void Gen1CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Gen1CheckBox.Checked)
            {
                List<float> points = Computer.MKM(a1, b1, M1, 4000);
                DrawGistGram(points, "Generator1Points");
            }
            else
            {
                MainDiagramm.Series["Generator1Points"].Points.Clear();
            }
        }

        private void Gen2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Gen2CheckBox.Checked)
            {
                List<float> points = Computer.MKM(a2, b2, M2, 4000);
                DrawGistGram(points, "Generator2 points");
            }
            else
            {
                MainDiagramm.Series["Generator2 points"].Points.Clear();
            }
        }

        private void Gen3CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Gen3CheckBox.Checked)
            {
                List<float> gen1 = Computer.MKM(a1, b1, M1, 1050000);
                List<float> gen2 = Computer.MKM(a2, b2, M2, 1050000);

                List<float> points = Computer.MMM(228, 1048576, gen2, gen1);
                DrawGistGram(points, "Generator3 points", false);
            }
            else
            {
                MainDiagramm.Series["Generator3 points"].Points.Clear();
            }
        }

        private void Gen4CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Gen4CheckBox.Checked)
            {
                List<float> gen1 = Computer.MKM(a1, b1, M1, 1050000);
                List<float> gen2 = Computer.MKM(a2, b2, M2, 1050000);

                List<float> points = Computer.MMM(500, 1048576, gen1, gen2);
                DrawGistGram(points, "Generator4 points");
            }
            else
            {
                MainDiagramm.Series["Generator4 points"].Points.Clear();
            }
        }
    }
}