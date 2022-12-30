using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;

namespace Main.Interface
{
    public partial class PlatformFeatureCaptureIdentification : Form
    {
        private IMap pMap;
        public ILayer riverLayer;
        public ILayer roiLayer;
        public bool OKClicked = false;
        public double ratio;
        public double captureElbowThre;
        public double tributaryAngleThre;
        public double oppositeReachThre;
        public bool windGapJudged = true;
        public PlatformFeatureCaptureIdentification(IMap map,double defaultRatio,double defaultCaptureElbowThre,double defaultTributaryAngleThre,double defaultOppositeReachThre)
        {
            InitializeComponent();
            ratio = defaultRatio;
            captureElbowThre = defaultCaptureElbowThre;
            tributaryAngleThre = defaultTributaryAngleThre;
            oppositeReachThre = defaultOppositeReachThre;
            textBox1.Text = ratio.ToString();
            textBox2.Text = captureElbowThre.ToString();
            textBox3.Text = tributaryAngleThre.ToString();
            textBox4.Text = oppositeReachThre.ToString();
            pMap = map;
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                comboBox1.Items.Add(pMap.get_Layer(i).Name);
                comboBox2.Items.Add(pMap.get_Layer(i).Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = ratio.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = captureElbowThre.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = tributaryAngleThre.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Text = oppositeReachThre.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OKClicked = true;
            riverLayer = pMap.get_Layer(comboBox1.SelectedIndex);
            roiLayer = pMap.get_Layer(comboBox2.SelectedIndex);
            ratio = Convert.ToDouble(textBox1.Text);
            captureElbowThre = Convert.ToDouble(textBox2.Text);
            tributaryAngleThre = Convert.ToDouble(textBox3.Text);
            oppositeReachThre = Convert.ToDouble(textBox4.Text);
            windGapJudged = checkBox1.Checked;
            this.Close();
        }
    }
}
