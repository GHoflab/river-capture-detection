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
    public partial class AllGroupsIdentificationByChiPlot : Form
    {
        private IMap pMap;
        public bool OKClicked = false;
        public string groupPath = "";
        public ILayer groupLayer;
        public AllGroupsIdentificationByChiPlot(IMap map)
        {
            InitializeComponent();
            pMap = map;
            for (int i = 0; i < pMap.LayerCount; i++)
            {
                comboBox1.Items.Add(pMap.get_Layer(i).Name);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OKClicked = true;
            groupPath = textBox1.Text;
            groupLayer = pMap.get_Layer(comboBox1.SelectedIndex);
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.SelectedPath;
            }
        }
    }
}
