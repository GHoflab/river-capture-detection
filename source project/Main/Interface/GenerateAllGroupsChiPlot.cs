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
    public partial class GenerateAllGroupsChiPlot : Form
    {
        public GenerateAllGroupsChiPlot(IMap map, double defaultExponent, string defaultGroupPath)
        {
            InitializeComponent();
            pMap = map;
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                comboBox1.Items.Add(pMap.get_Layer(i).Name);
                comboBox2.Items.Add(pMap.get_Layer(i).Name);
                comboBox3.Items.Add(pMap.get_Layer(i).Name);
                comboBox4.Items.Add(pMap.get_Layer(i).Name);
            }
            exponent = defaultExponent;
            groupPath = defaultGroupPath;
        }
        private string groupPath = "";
        private IMap pMap;
        public double minHeight;
        public ILayer riverLayer;
        public ILayer heightLayer;
        public ILayer accLayer;
        public ILayer groupLayer;
        public bool OKClicked = false;
        public double exponent;
        public double step;
        public bool beheadedChoice;
        public string savePath;
        private double accXSize = 0;
        private double accYSize = 0;
        private double heightXSize = 0;
        private double heightYSize = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = dialog.SelectedPath;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = minHeight.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = step.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = exponent.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ILayer pLayer = pMap.get_Layer(comboBox2.SelectedIndex);
            IRasterLayer pAccLayer = pLayer as IRasterLayer;
            IRaster raster = pAccLayer.Raster;
            IRasterProps RasterProps = (IRasterProps)raster;
            accXSize = RasterProps.MeanCellSize().X;
            accYSize = RasterProps.MeanCellSize().Y;
            if (heightXSize == 0)
            {
                step = (accXSize + accYSize) / 4;
            }
            else
            {
                step = (Math.Min(heightXSize, accXSize) + Math.Min(heightYSize, accYSize)) / 4;
            }
            textBox2.Text = step.ToString();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ILayer pLayer = pMap.get_Layer(comboBox3.SelectedIndex);
            IRasterLayer pHeightLayer = pLayer as IRasterLayer;
            IRaster raster = pHeightLayer.Raster;
            IRasterBandCollection rasterBands = raster as IRasterBandCollection;
            IRasterBand rasterBand = rasterBands.Item(0);
            rasterBand.ComputeStatsAndHist();
            IRasterStatistics rasterStatistics = rasterBand.Statistics;
            minHeight = rasterStatistics.Minimum;
            textBox1.Text = minHeight.ToString();

            IRasterProps RasterProps = (IRasterProps)raster;
            heightXSize = RasterProps.MeanCellSize().X;
            heightYSize = RasterProps.MeanCellSize().Y;
            if (accXSize == 0)
            {
                step = (heightXSize + heightYSize) / 4;
            }
            else
            {
                step = (Math.Min(heightXSize, accXSize) + Math.Min(heightYSize, accYSize)) / 4;
            }
            textBox2.Text = step.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            minHeight = Convert.ToDouble(textBox1.Text);
            riverLayer = pMap.get_Layer(comboBox1.SelectedIndex);
            accLayer = pMap.get_Layer(comboBox2.SelectedIndex);
            heightLayer = pMap.get_Layer(comboBox3.SelectedIndex);
            groupLayer = pMap.get_Layer(comboBox4.SelectedIndex);
            exponent = Convert.ToDouble(textBox3.Text);
            savePath = textBox4.Text;
            step = Convert.ToDouble(textBox2.Text);
            beheadedChoice = radioButton1.Checked;
            OKClicked = true;
            this.Close();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            radioButton2.Checked = false;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            radioButton1.Checked = false;
        }
    }
}
