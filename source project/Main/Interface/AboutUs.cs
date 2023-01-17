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
    public partial class AboutUs : Form
    {
        public AboutUs()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("Automatic detection of river capture based on planform pattern and χ-plot of the stream network");
            MessageBox.Show("Title copied");
        }
    }
}
