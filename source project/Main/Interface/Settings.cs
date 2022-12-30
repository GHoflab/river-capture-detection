using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Main.Interface
{
    public partial class Settings : Form
    {
        public bool OKClicked = false;
        public string defaultROIFolder;
        public string defaultChiPlotFolder;
        public double defaultRatio;
        public double defaultCaptureElbowThre;
        public double defaultTributaryAngleThre;
        public double defaultOppositeReachThre;
        public double defaultExponent;
        public Settings(string ROIFolder, string chiPlotFolder, double ratio, double captrueElbowThre, double tributaryAngleThre, double oppositeReachThre, double exponent)
        {
            InitializeComponent();
            textBox1.Text = ROIFolder;
            textBox2.Text = chiPlotFolder;
            textBox6.Text = ratio.ToString();
            textBox5.Text = captrueElbowThre.ToString();
            textBox3.Text = tributaryAngleThre.ToString();
            textBox4.Text = oppositeReachThre.ToString();
            textBox7.Text = exponent.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = dialog.SelectedPath;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OKClicked = true;
            defaultROIFolder = textBox1.Text;
            defaultChiPlotFolder = textBox2.Text;
            defaultRatio = Convert.ToDouble(textBox6.Text);
            defaultCaptureElbowThre = Convert.ToDouble(textBox5.Text);
            defaultTributaryAngleThre = Convert.ToDouble(textBox3.Text);
            defaultOppositeReachThre = Convert.ToDouble(textBox4.Text);
            defaultExponent = Convert.ToDouble(textBox7.Text);
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox6.Text = "0.5";
            textBox5.Text = "120";
            textBox3.Text = "120";
            textBox4.Text = "145";
            textBox7.Text = "0.45";
        }
    }
}
