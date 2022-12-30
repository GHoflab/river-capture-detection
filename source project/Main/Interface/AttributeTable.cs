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
    public partial class AttributeTable : Form
    {
        public AttributeTable(string[] tableHead, string[][] tableList)
        {
            InitializeComponent();
            for (int i = 0; i < tableHead.Length; i++)
                dataGridView1.Columns.Add(i.ToString(), tableHead[i]);
            for (int i = 0; i < tableList.Length; i++)
            {
                dataGridView1.Rows.Add(tableList[i]);
                //for (int j = 0; j < tableList[0].Length; j++)
                //{

                //}
            }
        }
    }
}
