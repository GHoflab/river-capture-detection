using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Main.Operation;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.DataManagementTools;
using ESRI.ArcGIS.Geoprocessing;

namespace Main.Operation
{
    class Matrix
    {
        private ISpatialReference pSR;
        private double xMin;
        private double yMin;
        private double yMax;
        private int row;
        private int column;
        private double xSize;
        private double ySize;
        public List<List<double>> data = new List<List<double>>();
        //read raster as matrix
        public Matrix(ILayer pLayer)
        {
            IRasterLayer pRL = pLayer as IRasterLayer;
            IRaster raster = pRL.Raster;
            IRaster2 raster2 = raster as IRaster2;
            IRasterProps RasterProps = (IRasterProps)raster;
            pSR = RasterProps.SpatialReference;
            IEnvelope extent = RasterProps.Extent; //extent of raster
            xMin = extent.XMin;
            yMin = extent.YMin;
            yMax = extent.YMax;
            row = RasterProps.Height;//row number of raster
            column = RasterProps.Width; //column number of raster
            xSize = RasterProps.MeanCellSize().X;//raster width
            ySize = RasterProps.MeanCellSize().Y;//raster height

            //Geoprocessor pGP = new Geoprocessor();
            //GetCellValue pGCV = new GetCellValue();
            //pGCV.in_raster = pRL;
            //for (int i = 0; i < column; i++)
            //{
            //    List<double> rowData = new List<double>();

            //    for (int j = 0; j < row; j++)
            //    {
            //        pGCV.location_point = (xMin + i * xSize + xSize / 2).ToString() + " " + (yMax - j * ySize - ySize / 2).ToString();
            //        IGeoProcessorResult result = (IGeoProcessorResult)pGP.Execute(pGCV, null);
            //        string result_str = result.ReturnValue.ToString();
            //        rowData.Add(Convert.ToDouble(result.ReturnValue.ToString()));
            //    }
            //    data.Add(rowData);
            //}

            IPnt pntSize = new PntClass();
            pntSize.SetCoords(column, row);
            IRasterCursor pRasterCursor = raster2.CreateCursorEx(pntSize);
            IPixelBlock3 pPixelBlock3 = pRasterCursor.PixelBlock as IPixelBlock3;
            int blockheight = pPixelBlock3.Height;
            int blockwidth = pPixelBlock3.Width;
            System.Array pixels = (System.Array)pPixelBlock3.get_PixelData(0);
            for (int i = 0; i < blockheight; i++)
            {
                List<double> rowData = new List<double>();
                for (int j = 0; j < blockwidth; j++)
                {
                    //[Width,Height]
                    rowData.Add(Convert.ToDouble(pixels.GetValue(j, i)));
                }
                data.Add(rowData);
            }
        }
        //point xy in matrix
        public Point MatrixXY(Point p)
        {
            double x = (p.GetX() - xMin - 0.5 * xSize) / xSize;
            double y = (yMax - p.GetY() - 0.5 * ySize) / ySize;
            Point pointAcc = new Point(x, y);
            return pointAcc;
        }
        //get the nearest value of the point
        public void Nearest(Reach reach, ref List<double> datalist)
        {
            datalist.Clear();
            for (int i = 0; i < reach.profilePoints.Count; i++)
            {
                Point matrixP = MatrixXY(reach.profilePoints[i]);
                if (matrixP.GetX() < 0)
                    matrixP.SetX(0);
                else if (matrixP.GetX() > data[0].Count - 1)
                    matrixP.SetX(data[0].Count - 1);
                if (matrixP.GetY() < 0)
                    matrixP.SetY(0);
                else if (matrixP.GetY() > data.Count - 1)
                    matrixP.SetY(data.Count - 1);
                datalist.Add(data[(int)Math.Round(matrixP.GetY())][(int)Math.Round(matrixP.GetX())]);
            }
        }
        //calculate bilinear interpolation value at the point
        public void Bilinear(Reach reach, ref List<double> datalist)
        {
            datalist.Clear();
            for (int i = 0; i < reach.profilePoints.Count; i++)
            {
                double x = MatrixXY(reach.profilePoints[i]).GetX();
                double y = MatrixXY(reach.profilePoints[i]).GetY();
                double result = 0;
                if (x >= 0 && x <= column - 1 && y >= 0 && y <= row - 1)
                {
                    int x1 = (int)(Math.Floor(x));
                    int x2 = (int)(Math.Ceiling(x));
                    int y1 = (int)(Math.Floor(y));
                    int y2 = (int)(Math.Ceiling(y));
                    double r1 = (x2 - x) * data[y1][x1] + (x - x1) * data[y1][x2];
                    double r2 = (x2 - x) * data[y2][x1] + (x - x1) * data[y2][x2];
                    double c1 = (y - y1) * data[y2][x1] + (y2 - y) * data[y1][x1];
                    double c2 = (y - y1) * data[y2][x2] + (y2 - y) * data[y1][x2];
                    if (y1 == y2)
                    {
                        if (x1 == x2)
                            result = data[y1][x1];
                        else
                            result = r1;
                    }
                    else
                    {
                        if (x1 == x2)
                            result = c1;
                        else
                            result = (y - y1) * r2 + (y2 - y) * r1;
                    }
                }
                else
                {
                    int x_bound = -1;
                    int y_bound = -1;
                    if (x < 0)
                    {
                        x_bound = 0;
                    }
                    else if (x > column - 1)
                    {
                        x_bound = column - 1;
                    }
                    else
                    {
                        x_bound = (int)Math.Round(x);
                    }
                    if (y < 0)
                    {
                        y_bound = 0;
                    }
                    else if (y > row - 1)
                    {
                        y_bound = row - 1;
                    }
                    else
                    {
                        y_bound = (int)Math.Round(y);
                    }
                    result = data[y_bound][x_bound];
                }
                datalist.Add(result);
            }
        }
    }
}
