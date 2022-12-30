using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main.Operation
{
    class ChiFile
    {
        public List<double> chi = new List<double>();
        public List<double> height = new List<double>();
        //read file output with chi and elevation
        public ChiFile(string filePath)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(filePath);
            file.ReadLine();
            while ((line = file.ReadLine()) != null)
            {
                chi.Add(Convert.ToDouble(line.Split(',')[0]));
                height.Add(Convert.ToDouble(line.Split(',')[1]));
            }
            file.Close();
        }
    }
}
