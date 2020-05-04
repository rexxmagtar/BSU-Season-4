namespace MM_lab_3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Start = new System.Windows.Forms.Button();
            this.Expectation = new System.Windows.Forms.Label();
            this.FabricExpectation = new System.Windows.Forms.Label();
            this.Dispersion = new System.Windows.Forms.Label();
            this.FabricDispersion = new System.Windows.Forms.Label();
            this.ExpectText = new System.Windows.Forms.TextBox();
            this.FabricExpectText = new System.Windows.Forms.TextBox();
            this.DispText = new System.Windows.Forms.TextBox();
            this.FabricDispText = new System.Windows.Forms.TextBox();
            this.HiExpection = new System.Windows.Forms.Label();
            this.FacbricHiExpect = new System.Windows.Forms.Label();
            this.HiDisp = new System.Windows.Forms.Label();
            this.FabricHiDisp = new System.Windows.Forms.Label();
            this.HiExText = new System.Windows.Forms.TextBox();
            this.FHiExText = new System.Windows.Forms.TextBox();
            this.HiDispText = new System.Windows.Forms.TextBox();
            this.FHiDispText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LaplasExTB = new System.Windows.Forms.TextBox();
            this.FabLaplasExTB = new System.Windows.Forms.TextBox();
            this.LaplasDispTB = new System.Windows.Forms.TextBox();
            this.FabLaplasDispTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.LNormExTB = new System.Windows.Forms.TextBox();
            this.FabLNormExTB = new System.Windows.Forms.TextBox();
            this.LNormDispTB = new System.Windows.Forms.TextBox();
            this.FabLNormDispTB = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.CoshiExTB = new System.Windows.Forms.TextBox();
            this.FabCoshiExTB = new System.Windows.Forms.TextBox();
            this.CorrTB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.MixAverage = new System.Windows.Forms.TextBox();
            this.MixDispTrue = new System.Windows.Forms.TextBox();
            this.MixDisp = new System.Windows.Forms.TextBox();
            this.MixTrueEx = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(384, 40);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 0;
            this.Start.Text = "ПУСК";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Expectation
            // 
            this.Expectation.AutoSize = true;
            this.Expectation.Location = new System.Drawing.Point(17, 43);
            this.Expectation.Name = "Expectation";
            this.Expectation.Size = new System.Drawing.Size(102, 17);
            this.Expectation.TabIndex = 1;
            this.Expectation.Text = "МатОжидание";
            // 
            // FabricExpectation
            // 
            this.FabricExpectation.AutoSize = true;
            this.FabricExpectation.Location = new System.Drawing.Point(17, 76);
            this.FabricExpectation.Name = "FabricExpectation";
            this.FabricExpectation.Size = new System.Drawing.Size(157, 17);
            this.FabricExpectation.TabIndex = 2;
            this.FabricExpectation.Text = "Приближенное МатОж";
            // 
            // Dispersion
            // 
            this.Dispersion.AutoSize = true;
            this.Dispersion.Location = new System.Drawing.Point(17, 108);
            this.Dispersion.Name = "Dispersion";
            this.Dispersion.Size = new System.Drawing.Size(81, 17);
            this.Dispersion.TabIndex = 3;
            this.Dispersion.Text = "Дисперсия";
            // 
            // FabricDispersion
            // 
            this.FabricDispersion.AutoSize = true;
            this.FabricDispersion.Location = new System.Drawing.Point(17, 141);
            this.FabricDispersion.Name = "FabricDispersion";
            this.FabricDispersion.Size = new System.Drawing.Size(145, 17);
            this.FabricDispersion.TabIndex = 4;
            this.FabricDispersion.Text = "Приближенная Дисп";
            // 
            // ExpectText
            // 
            this.ExpectText.Location = new System.Drawing.Point(211, 40);
            this.ExpectText.Name = "ExpectText";
            this.ExpectText.Size = new System.Drawing.Size(100, 22);
            this.ExpectText.TabIndex = 5;
            // 
            // FabricExpectText
            // 
            this.FabricExpectText.Location = new System.Drawing.Point(211, 76);
            this.FabricExpectText.Name = "FabricExpectText";
            this.FabricExpectText.Size = new System.Drawing.Size(100, 22);
            this.FabricExpectText.TabIndex = 6;
            // 
            // DispText
            // 
            this.DispText.Location = new System.Drawing.Point(211, 108);
            this.DispText.Name = "DispText";
            this.DispText.Size = new System.Drawing.Size(100, 22);
            this.DispText.TabIndex = 7;
            // 
            // FabricDispText
            // 
            this.FabricDispText.Location = new System.Drawing.Point(211, 141);
            this.FabricDispText.Name = "FabricDispText";
            this.FabricDispText.Size = new System.Drawing.Size(100, 22);
            this.FabricDispText.TabIndex = 8;
            // 
            // HiExpection
            // 
            this.HiExpection.AutoSize = true;
            this.HiExpection.Location = new System.Drawing.Point(505, 44);
            this.HiExpection.Name = "HiExpection";
            this.HiExpection.Size = new System.Drawing.Size(102, 17);
            this.HiExpection.TabIndex = 9;
            this.HiExpection.Text = "МатОжидание";
            // 
            // FacbricHiExpect
            // 
            this.FacbricHiExpect.AutoSize = true;
            this.FacbricHiExpect.Location = new System.Drawing.Point(505, 76);
            this.FacbricHiExpect.Name = "FacbricHiExpect";
            this.FacbricHiExpect.Size = new System.Drawing.Size(157, 17);
            this.FacbricHiExpect.TabIndex = 10;
            this.FacbricHiExpect.Text = "Приближенное МатОж";
            // 
            // HiDisp
            // 
            this.HiDisp.AutoSize = true;
            this.HiDisp.Location = new System.Drawing.Point(508, 108);
            this.HiDisp.Name = "HiDisp";
            this.HiDisp.Size = new System.Drawing.Size(81, 17);
            this.HiDisp.TabIndex = 11;
            this.HiDisp.Text = "Дисперсия";
            // 
            // FabricHiDisp
            // 
            this.FabricHiDisp.AutoSize = true;
            this.FabricHiDisp.Location = new System.Drawing.Point(508, 145);
            this.FabricHiDisp.Name = "FabricHiDisp";
            this.FabricHiDisp.Size = new System.Drawing.Size(145, 17);
            this.FabricHiDisp.TabIndex = 12;
            this.FabricHiDisp.Text = "Приближенная Дисп";
            // 
            // HiExText
            // 
            this.HiExText.Location = new System.Drawing.Point(695, 40);
            this.HiExText.Name = "HiExText";
            this.HiExText.Size = new System.Drawing.Size(100, 22);
            this.HiExText.TabIndex = 13;
            // 
            // FHiExText
            // 
            this.FHiExText.Location = new System.Drawing.Point(695, 76);
            this.FHiExText.Name = "FHiExText";
            this.FHiExText.Size = new System.Drawing.Size(100, 22);
            this.FHiExText.TabIndex = 14;
            // 
            // HiDispText
            // 
            this.HiDispText.Location = new System.Drawing.Point(695, 108);
            this.HiDispText.Name = "HiDispText";
            this.HiDispText.Size = new System.Drawing.Size(100, 22);
            this.HiDispText.TabIndex = 15;
            // 
            // FHiDispText
            // 
            this.FHiDispText.Location = new System.Drawing.Point(695, 138);
            this.FHiDispText.Name = "FHiDispText";
            this.FHiDispText.Size = new System.Drawing.Size(100, 22);
            this.FHiDispText.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(508, 333);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Приближенная Дисп";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(508, 296);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "Дисперсия";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(505, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "Приближенное МатОж";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(505, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "МатОжидание";
            // 
            // LaplasExTB
            // 
            this.LaplasExTB.Location = new System.Drawing.Point(695, 226);
            this.LaplasExTB.Name = "LaplasExTB";
            this.LaplasExTB.Size = new System.Drawing.Size(100, 22);
            this.LaplasExTB.TabIndex = 21;
            // 
            // FabLaplasExTB
            // 
            this.FabLaplasExTB.Location = new System.Drawing.Point(695, 264);
            this.FabLaplasExTB.Name = "FabLaplasExTB";
            this.FabLaplasExTB.Size = new System.Drawing.Size(100, 22);
            this.FabLaplasExTB.TabIndex = 22;
            // 
            // LaplasDispTB
            // 
            this.LaplasDispTB.Location = new System.Drawing.Point(695, 296);
            this.LaplasDispTB.Name = "LaplasDispTB";
            this.LaplasDispTB.Size = new System.Drawing.Size(100, 22);
            this.LaplasDispTB.TabIndex = 23;
            // 
            // FabLaplasDispTB
            // 
            this.FabLaplasDispTB.Location = new System.Drawing.Point(695, 333);
            this.FabLaplasDispTB.Name = "FabLaplasDispTB";
            this.FabLaplasDispTB.Size = new System.Drawing.Size(100, 22);
            this.FabLaplasDispTB.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(146, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 17);
            this.label5.TabIndex = 25;
            this.label5.Text = "Нормальное";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(602, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 17);
            this.label6.TabIndex = 26;
            this.label6.Text = "Хи-квадрат";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(598, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 17);
            this.label7.TabIndex = 27;
            this.label7.Text = "Лаплас";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 332);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(145, 17);
            this.label9.TabIndex = 31;
            this.label9.Text = "Приближенная Дисп";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 295);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 17);
            this.label10.TabIndex = 30;
            this.label10.Text = "Дисперсия";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 263);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(157, 17);
            this.label11.TabIndex = 29;
            this.label11.Text = "Приближенное МатОж";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 231);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 17);
            this.label12.TabIndex = 28;
            this.label12.Text = "МатОжидание";
            // 
            // LNormExTB
            // 
            this.LNormExTB.Location = new System.Drawing.Point(211, 231);
            this.LNormExTB.Name = "LNormExTB";
            this.LNormExTB.Size = new System.Drawing.Size(100, 22);
            this.LNormExTB.TabIndex = 32;
            // 
            // FabLNormExTB
            // 
            this.FabLNormExTB.Location = new System.Drawing.Point(211, 264);
            this.FabLNormExTB.Name = "FabLNormExTB";
            this.FabLNormExTB.Size = new System.Drawing.Size(100, 22);
            this.FabLNormExTB.TabIndex = 33;
            // 
            // LNormDispTB
            // 
            this.LNormDispTB.Location = new System.Drawing.Point(211, 295);
            this.LNormDispTB.Name = "LNormDispTB";
            this.LNormDispTB.Size = new System.Drawing.Size(100, 22);
            this.LNormDispTB.TabIndex = 34;
            // 
            // FabLNormDispTB
            // 
            this.FabLNormDispTB.Location = new System.Drawing.Point(211, 332);
            this.FabLNormDispTB.Name = "FabLNormDispTB";
            this.FabLNormDispTB.Size = new System.Drawing.Size(100, 22);
            this.FabLNormDispTB.TabIndex = 35;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(17, 445);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(168, 17);
            this.label14.TabIndex = 37;
            this.label14.Text = "Приближенная медиана";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(17, 413);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 17);
            this.label15.TabIndex = 36;
            this.label15.Text = "Медиана";
            // 
            // CoshiExTB
            // 
            this.CoshiExTB.Location = new System.Drawing.Point(211, 413);
            this.CoshiExTB.Name = "CoshiExTB";
            this.CoshiExTB.Size = new System.Drawing.Size(100, 22);
            this.CoshiExTB.TabIndex = 40;
            // 
            // FabCoshiExTB
            // 
            this.FabCoshiExTB.Location = new System.Drawing.Point(211, 445);
            this.FabCoshiExTB.Name = "FabCoshiExTB";
            this.FabCoshiExTB.Size = new System.Drawing.Size(100, 22);
            this.FabCoshiExTB.TabIndex = 41;
            // 
            // CorrTB
            // 
            this.CorrTB.Location = new System.Drawing.Point(505, 423);
            this.CorrTB.Name = "CorrTB";
            this.CorrTB.Size = new System.Drawing.Size(100, 22);
            this.CorrTB.TabIndex = 42;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(146, 498);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 17);
            this.label8.TabIndex = 43;
            this.label8.Text = "Лаплас + Х";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 533);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(102, 17);
            this.label13.TabIndex = 44;
            this.label13.Text = "МатОжидание";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(20, 570);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(133, 17);
            this.label16.TabIndex = 45;
            this.label16.Text = "Настоящее МатОж";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(20, 609);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(81, 17);
            this.label17.TabIndex = 46;
            this.label17.Text = "Дисперсия";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(17, 651);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(121, 17);
            this.label18.TabIndex = 47;
            this.label18.Text = "Настоящая Дисп";
            // 
            // MixAverage
            // 
            this.MixAverage.Location = new System.Drawing.Point(192, 533);
            this.MixAverage.Name = "MixAverage";
            this.MixAverage.Size = new System.Drawing.Size(100, 22);
            this.MixAverage.TabIndex = 48;
            // 
            // MixDispTrue
            // 
            this.MixDispTrue.Location = new System.Drawing.Point(192, 651);
            this.MixDispTrue.Name = "MixDispTrue";
            this.MixDispTrue.Size = new System.Drawing.Size(100, 22);
            this.MixDispTrue.TabIndex = 49;
            // 
            // MixDisp
            // 
            this.MixDisp.Location = new System.Drawing.Point(192, 609);
            this.MixDisp.Name = "MixDisp";
            this.MixDisp.Size = new System.Drawing.Size(100, 22);
            this.MixDisp.TabIndex = 50;
            // 
            // MixTrueEx
            // 
            this.MixTrueEx.Location = new System.Drawing.Point(192, 570);
            this.MixTrueEx.Name = "MixTrueEx";
            this.MixTrueEx.Size = new System.Drawing.Size(100, 22);
            this.MixTrueEx.TabIndex = 51;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(101, 192);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(135, 17);
            this.label19.TabIndex = 52;
            this.label19.Text = "Логнормированное";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 723);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.MixTrueEx);
            this.Controls.Add(this.MixDisp);
            this.Controls.Add(this.MixDispTrue);
            this.Controls.Add(this.MixAverage);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.CorrTB);
            this.Controls.Add(this.FabCoshiExTB);
            this.Controls.Add(this.CoshiExTB);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.FabLNormDispTB);
            this.Controls.Add(this.LNormDispTB);
            this.Controls.Add(this.FabLNormExTB);
            this.Controls.Add(this.LNormExTB);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.FabLaplasDispTB);
            this.Controls.Add(this.LaplasDispTB);
            this.Controls.Add(this.FabLaplasExTB);
            this.Controls.Add(this.LaplasExTB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FHiDispText);
            this.Controls.Add(this.HiDispText);
            this.Controls.Add(this.FHiExText);
            this.Controls.Add(this.HiExText);
            this.Controls.Add(this.FabricHiDisp);
            this.Controls.Add(this.HiDisp);
            this.Controls.Add(this.FacbricHiExpect);
            this.Controls.Add(this.HiExpection);
            this.Controls.Add(this.FabricDispText);
            this.Controls.Add(this.DispText);
            this.Controls.Add(this.FabricExpectText);
            this.Controls.Add(this.ExpectText);
            this.Controls.Add(this.FabricDispersion);
            this.Controls.Add(this.Dispersion);
            this.Controls.Add(this.FabricExpectation);
            this.Controls.Add(this.Expectation);
            this.Controls.Add(this.Start);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Label Expectation;
        private System.Windows.Forms.Label FabricExpectation;
        private System.Windows.Forms.Label Dispersion;
        private System.Windows.Forms.Label FabricDispersion;
        private System.Windows.Forms.TextBox ExpectText;
        private System.Windows.Forms.TextBox FabricExpectText;
        private System.Windows.Forms.TextBox DispText;
        private System.Windows.Forms.TextBox FabricDispText;
        private System.Windows.Forms.Label HiExpection;
        private System.Windows.Forms.Label FacbricHiExpect;
        private System.Windows.Forms.Label HiDisp;
        private System.Windows.Forms.Label FabricHiDisp;
        private System.Windows.Forms.TextBox HiExText;
        private System.Windows.Forms.TextBox FHiExText;
        private System.Windows.Forms.TextBox HiDispText;
        private System.Windows.Forms.TextBox FHiDispText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox LaplasExTB;
        private System.Windows.Forms.TextBox FabLaplasExTB;
        private System.Windows.Forms.TextBox LaplasDispTB;
        private System.Windows.Forms.TextBox FabLaplasDispTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox LNormExTB;
        private System.Windows.Forms.TextBox FabLNormExTB;
        private System.Windows.Forms.TextBox LNormDispTB;
        private System.Windows.Forms.TextBox FabLNormDispTB;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox CoshiExTB;
        private System.Windows.Forms.TextBox FabCoshiExTB;
        private System.Windows.Forms.TextBox CorrTB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox MixAverage;
        private System.Windows.Forms.TextBox MixDispTrue;
        private System.Windows.Forms.TextBox MixDisp;
        private System.Windows.Forms.TextBox MixTrueEx;
        private System.Windows.Forms.Label label19;
    }
}

