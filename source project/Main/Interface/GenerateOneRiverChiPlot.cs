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
    public partial class GenerateOneRiverChiPlot : Form
    {
        private IMap pMap;
        public double minHeight;
        public ILayer riverLayer;
        public ILayer heightLayer;
        public ILayer accLayer;
        public bool OKClicked = false;
        public double exponent;
        public double step;
        public string savePath;
        private double accXSize = 0;
        private double accYSize = 0;
        private double heightXSize = 0;
        private double heightYSize = 0;
        private double defaultExponent;
        private string defaultSavePath;
        public GenerateOneRiverChiPlot(IMap map, double exponent,string savePath)
        {
            InitializeComponent();
            pMap = map;
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                comboBox1.Items.Add(pMap.get_Layer(i).Name);
                comboBox2.Items.Add(pMap.get_Layer(i).Name);
                comboBox3.Items.Add(pMap.get_Layer(i).Name);
            }
            defaultExponent = exponent;
            textBox3.Text = defaultExponent.ToString();
            defaultSavePath = savePath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = minHeight.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = step.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = defaultExponent.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog pSFD = new SaveFileDialog();
            pSFD.Filter = "csv(*.csv)|*.csv";
            pSFD.Title = "Output file";
            pSFD.InitialDirectory = defaultSavePath;
            if (pSFD.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = pSFD.FileName;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            minHeight = Convert.ToDouble(textBox1.Text);
            accLayer = pMap.get_Layer(comboBox2.SelectedIndex);
            heightLayer = pMap.get_Layer(comboBox3.SelectedIndex);
            riverLayer = pMap.get_Layer(comboBox1.SelectedIndex);
            exponent = Convert.ToDouble(textBox3.Text);
            savePath = textBox4.Text;
            step = Convert.ToDouble(textBox2.Text);
            OKClicked = true;
            this.Close();
        }
    }
}
