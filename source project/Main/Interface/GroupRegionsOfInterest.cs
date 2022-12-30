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
    public partial class GroupRegionsOfInterest : Form
    {
        private IMap pMap;
        public string savePath;
        public ILayer riverLayer;
        public bool OkClicked;
        public GroupRegionsOfInterest(IMap map)
        {
            InitializeComponent();
            pMap = map;
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                comboBox1.Items.Add(pMap.get_Layer(i).Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog pSFD = new SaveFileDialog();
            pSFD.Title = "Export candidate regions";
            pSFD.Filter = "Shapefile(*.shp)|*.shp";
            if (pSFD.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = pSFD.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OkClicked = true;
            riverLayer = pMap.get_Layer(comboBox1.SelectedIndex);
            savePath = textBox1.Text;
            this.Close();
        }
    }
}
