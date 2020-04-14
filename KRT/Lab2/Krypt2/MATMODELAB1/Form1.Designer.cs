namespace MATMODELAB1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series15 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series16 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.MainDiagramm = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Gen1CheckBox = new System.Windows.Forms.CheckBox();
            this.Gen2CheckBox = new System.Windows.Forms.CheckBox();
            this.Gen3CheckBox = new System.Windows.Forms.CheckBox();
            this.Gen4CheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.MainDiagramm)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // MainDiagramm
            // 
            this.MainDiagramm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            chartArea4.Name = "ChartArea1";
            this.MainDiagramm.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.MainDiagramm.Legends.Add(legend4);
            this.MainDiagramm.Location = new System.Drawing.Point(-46, 2);
            this.MainDiagramm.Name = "MainDiagramm";
            this.MainDiagramm.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series13.ChartArea = "ChartArea1";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series13.Legend = "Legend1";
            series13.MarkerSize = 2;
            series13.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series13.Name = "Generator1Points";
            series13.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series13.YValuesPerPoint = 10;
            series13.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series14.ChartArea = "ChartArea1";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series14.Legend = "Legend1";
            series14.MarkerSize = 2;
            series14.Name = "Generator2 points";
            series15.ChartArea = "ChartArea1";
            series15.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series15.Legend = "Legend1";
            series15.MarkerSize = 2;
            series15.Name = "Generator3 points";
            series16.ChartArea = "ChartArea1";
            series16.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series16.Legend = "Legend1";
            series16.MarkerSize = 2;
            series16.Name = "Generator4 points";
            this.MainDiagramm.Series.Add(series13);
            this.MainDiagramm.Series.Add(series14);
            this.MainDiagramm.Series.Add(series15);
            this.MainDiagramm.Series.Add(series16);
            this.MainDiagramm.Size = new System.Drawing.Size(1043, 581);
            this.MainDiagramm.TabIndex = 0;
            this.MainDiagramm.Text = "MainDiagramm";
            this.MainDiagramm.Click += new System.EventHandler(this.chart1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Gen4CheckBox);
            this.groupBox1.Controls.Add(this.Gen3CheckBox);
            this.groupBox1.Controls.Add(this.Gen2CheckBox);
            this.groupBox1.Controls.Add(this.Gen1CheckBox);
            this.groupBox1.Location = new System.Drawing.Point(825, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(155, 194);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // Gen1CheckBox
            // 
            this.Gen1CheckBox.AutoSize = true;
            this.Gen1CheckBox.Location = new System.Drawing.Point(10, 21);
            this.Gen1CheckBox.Name = "Gen1CheckBox";
            this.Gen1CheckBox.Size = new System.Drawing.Size(145, 21);
            this.Gen1CheckBox.TabIndex = 0;
            this.Gen1CheckBox.Text = "Generator1 points";
            this.Gen1CheckBox.UseVisualStyleBackColor = true;
            this.Gen1CheckBox.CheckedChanged += new System.EventHandler(this.Gen1CheckBox_CheckedChanged);
            // 
            // Gen2CheckBox
            // 
            this.Gen2CheckBox.AutoSize = true;
            this.Gen2CheckBox.Location = new System.Drawing.Point(10, 48);
            this.Gen2CheckBox.Name = "Gen2CheckBox";
            this.Gen2CheckBox.Size = new System.Drawing.Size(145, 21);
            this.Gen2CheckBox.TabIndex = 1;
            this.Gen2CheckBox.Text = "Generator2 points";
            this.Gen2CheckBox.UseVisualStyleBackColor = true;
            this.Gen2CheckBox.CheckedChanged += new System.EventHandler(this.Gen2CheckBox_CheckedChanged);
            // 
            // Gen3CheckBox
            // 
            this.Gen3CheckBox.AutoSize = true;
            this.Gen3CheckBox.Location = new System.Drawing.Point(10, 75);
            this.Gen3CheckBox.Name = "Gen3CheckBox";
            this.Gen3CheckBox.Size = new System.Drawing.Size(145, 21);
            this.Gen3CheckBox.TabIndex = 2;
            this.Gen3CheckBox.Text = "Generator3 points";
            this.Gen3CheckBox.UseVisualStyleBackColor = true;
            this.Gen3CheckBox.CheckedChanged += new System.EventHandler(this.Gen3CheckBox_CheckedChanged);
            // 
            // Gen4CheckBox
            // 
            this.Gen4CheckBox.AutoSize = true;
            this.Gen4CheckBox.Location = new System.Drawing.Point(10, 102);
            this.Gen4CheckBox.Name = "Gen4CheckBox";
            this.Gen4CheckBox.Size = new System.Drawing.Size(145, 21);
            this.Gen4CheckBox.TabIndex = 3;
            this.Gen4CheckBox.Text = "Generator4 points";
            this.Gen4CheckBox.UseVisualStyleBackColor = true;
            this.Gen4CheckBox.CheckedChanged += new System.EventHandler(this.Gen4CheckBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 587);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MainDiagramm);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MainDiagramm)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataVisualization.Charting.Chart MainDiagramm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox Gen1CheckBox;
        private System.Windows.Forms.CheckBox Gen2CheckBox;
        private System.Windows.Forms.CheckBox Gen3CheckBox;
        private System.Windows.Forms.CheckBox Gen4CheckBox;
    }
}