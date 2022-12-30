using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Main.Operation;
using System.Windows.Forms.DataVisualization.Charting;

namespace Main.Interface
{
    public partial class DisplayGroupChiPlot : Form
    {
        public DisplayGroupChiPlot()
        {
            InitializeComponent();
            chart1.Series.Clear();
        }

        public DisplayGroupChiPlot(string capturedPath, string reversedPath, string beheadedPath)
        {
            InitializeComponent();
            chart1.Series.Clear();
            textBox1.Text = capturedPath;
            textBox2.Text = reversedPath;
            textBox3.Text = beheadedPath;

            ChiIdentification pCI = new ChiIdentification(capturedPath, reversedPath, beheadedPath);
            //ChiIdentification pCI = new ChiIdentification(capturedPath, reversedPath);
            pCI.BaseLevelUnify();
            ChiFile chiFile1 = pCI.captureChi;
            ChiFile chiFile2 = pCI.reversedChi;
            ChiFile chiFile3 = pCI.beheadedChi;
            IList<double> chi1 = chiFile1.chi;
            IList<double> height1 = chiFile1.height;
            Series s1 = new Series();
            s1.ChartType = SeriesChartType.Point;
            s1.Points.DataBindXY(chi1, height1);
            s1.Name = "Possible captured river";
            chart1.Series.Clear();
            chart1.Series.Add(s1);

            IList<double> chi2 = chiFile2.chi;
            IList<double> height2 = chiFile2.height;
            Series s2 = new Series();
            s2.ChartType = SeriesChartType.Point;
            s2.Points.DataBindXY(chi2, height2);
            s2.Name = "Possible reversed river";
            chart1.Series.Add(s2);

            IList<double> chi3 = chiFile3.chi;
            IList<double> height3 = chiFile3.height;
            Series s3 = new Series();
            s3.ChartType = SeriesChartType.Point;
            s3.Points.DataBindXY(chi3, height3);
            s3.Name = "Possible beheaded river";
            chart1.Series.Add(s3);

            chart1.ChartAreas[0].AxisY.Minimum = Math.Min(height3.Min(), Math.Min(height1.Min(), height2.Min()));
            //chart1.ChartAreas[0].AxisX.Minimum = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOFD = new OpenFileDialog();
            pOFD.Title = "χ-plot file of possible captured river";
            pOFD.Filter = "CSV(*.csv)|*.csv";
            if (pOFD.ShowDialog() == DialogResult.OK)
            {
                string filePath = pOFD.FileName;
                textBox1.Text = filePath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOFD = new OpenFileDialog();
            pOFD.Title = "χ-plot file of possible reversed river";
            pOFD.Filter = "CSV(*.csv)|*.csv";
            if (pOFD.ShowDialog() == DialogResult.OK)
            {
                string filePath = pOFD.FileName;
                textBox2.Text = filePath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOFD = new OpenFileDialog();
            pOFD.Title = "χ-plot file of possible beheaded river";
            pOFD.Filter = "CSV(*.csv)|*.csv";
            if (pOFD.ShowDialog() == DialogResult.OK)
            {
                string filePath = pOFD.FileName;
                textBox3.Text = filePath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChiIdentification pCI = new ChiIdentification(textBox1.Text, textBox2.Text, textBox3.Text);
            //ChiIdentification pCI = new ChiIdentification(textBox1.Text, textBox2.Text);
            pCI.BaseLevelUnify();
            ChiFile chiFile1 = pCI.captureChi;
            ChiFile chiFile2 = pCI.reversedChi;
            ChiFile chiFile3 = pCI.beheadedChi;

            IList<double> chi1 = chiFile1.chi;
            IList<double> height1 = chiFile1.height;
            Series s1 = new Series();
            s1.ChartType = SeriesChartType.Point;
            s1.Points.DataBindXY(chi1, height1);
            s1.Name = "possible captured river";
            chart1.Series.Clear();
            chart1.Series.Add(s1);

            IList<double> chi2 = chiFile2.chi;
            IList<double> height2 = chiFile2.height;
            Series s2 = new Series();
            s2.ChartType = SeriesChartType.Point;
            s2.Points.DataBindXY(chi2, height2);
            s2.Name = "possible reversed river";
            chart1.Series.Add(s2);

            IList<double> chi3 = chiFile3.chi;
            IList<double> height3 = chiFile3.height;
            Series s3 = new Series();
            s3.ChartType = SeriesChartType.Point;
            s3.Points.DataBindXY(chi3, height3);
            s3.Name = "possible beheaded river";
            chart1.Series.Add(s3);

            chart1.ChartAreas[0].AxisY.Minimum = Math.Min(height3.Min(), Math.Min(height1.Min(), height2.Min()));
            //chart1.ChartAreas[0].AxisX.Minimum = 0;
        }
    }
}
