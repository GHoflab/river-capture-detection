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
    public partial class DisplayOneChiPlot : Form
    {
        public DisplayOneChiPlot()
        {
            InitializeComponent();
            chart1.Series.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOFD = new OpenFileDialog();
            pOFD.Title = "χ-plot file of a river";
            pOFD.Filter = "CSV(*.csv)|*.csv";
            if (pOFD.ShowDialog() == DialogResult.OK)
            {
                string filePath = pOFD.FileName;
                textBox1.Text = filePath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = textBox1.Text;
            ChiFile chiFile = new ChiFile(filePath);
            IList<double> chi = chiFile.chi;
            IList<double> height = chiFile.height;
            Series s1 = new Series();
            s1.ChartType = SeriesChartType.Point;
            s1.Points.DataBindXY(chi, height);
            s1.Name = "h-χ plot";
            chart1.Series.Clear();
            chart1.Series.Add(s1);
            chart1.ChartAreas[0].AxisY.Minimum = height.Min();
            chart1.ChartAreas[0].AxisX.Minimum = 0;
        }
    }
}
