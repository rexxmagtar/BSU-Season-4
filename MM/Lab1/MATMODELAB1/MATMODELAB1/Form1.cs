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
        long c1 = 474379977;
        long M = (long)Math.Pow(2, 31);
        private long b1;
        long a1 = 261463909;
            
        long c2 = 3097871;
        private long b2;
        long a2 = 234289925;

        int K = 192;
        public Form1()
        {
            b1 = Math.Max(c1, M - c1);
            b2 = Math.Max(c2, M - c2);
          
            
            InitializeComponent();
            
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ResultBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void DoMKM()
        {
            List<float> result = Computer.MKM(261463909,b1,M,1000);
            UpdateResult(result);
        }

        private void DoMMM()
        {
            List<float> B = Computer.MKM(a1, b1, M, 1500);
            List<float> C = Computer.MKM(a2, b2, M, 1500);
            
            UpdateResult(Computer.MMM(K,1000,C,B));
            
        }

        private void UpdateResult(List<float> result)
        {
            ResultBox.Items.Clear();
            for (int i = 0; i < result.Count; i++)
            {
                ResultBox.Items.Add(i +1+ ": " + result[i]);
            }

            AnswerTextBox.Lines=new []{"100: " + result[99] ,  "900: " + result[899] ,  "1000: " + result[999]};
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (NameLabke.Text == "MKM")
            {
                NameLabke.Text = "MMM";
                DoMMM();
            }
            else
            {
                NameLabke.Text = "MKM";
                DoMKM(); 
            }
        }
    }
}