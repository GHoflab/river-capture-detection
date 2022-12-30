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
    public partial class OneGroupIdentificationByChiPlot : Form
    {
        public OneGroupIdentificationByChiPlot()
        {
            InitializeComponent();
        }
        public string captureFile;
        public string reservedFile;
        public string beheadedFile;
        public bool OKClicked = false;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOFD = new OpenFileDialog();
            pOFD.Title = "χ-plot file of possible captured river";
            pOFD.Filter = "csv(*.csv)|*.csv";
            if (pOFD.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = pOFD.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOFD = new OpenFileDialog();
            pOFD.Title = "χ-plot file of possible reversed river";
            pOFD.Filter = "csv(*.csv)|*.csv";
            if (pOFD.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = pOFD.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOFD = new OpenFileDialog();
            pOFD.Title = "χ-plot file of possible beheaded river";
            pOFD.Filter = "csv(*.csv)|*.csv";
            if (pOFD.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = pOFD.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OKClicked = true;
            captureFile = textBox1.Text;
            reservedFile = textBox2.Text;
            beheadedFile = textBox3.Text;
            this.Close();
        }
    }
}
