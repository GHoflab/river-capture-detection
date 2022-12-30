using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geometry;

namespace Main.Operation
{
    class Reach
    {
        public int ID;
        public double flowDirection;
        public double length;
        public int InDeg = 0;
        public Point fPoint;        //feature point
        public List<Point> points = new List<Point>();
        public int downStream = -1; //downstream index
        public int upstream1 = -1;  //upstream index
        public int upstream2 = -1;
        public List<Point> profilePoints;//densified points
        public List<double> distance;//distance between next point
        public List<double> acc;
        public List<double> elevation;
        public List<double> chi;
        public Reach(IFeature pFeature, int num)
        {
            ID = num;
            //Get x,y from shape
            IPointCollection pPC = pFeature.Shape as IPointCollection;
            IPoint pPoint;
            int count = pPC.PointCount;
            for (int i = 0; i < count; i++)
            {
                pPoint = pPC.get_Point(i);
                Point point = new Point(pPoint.X, pPoint.Y);
                points.Add(point);
            }
            //flowDirection = FlowDirection(points, ref fPoint, ref length, ratio);
            profilePoints = new List<Point>();
            distance = new List<double>();
            acc = new List<double>();
            elevation = new List<double>();
            chi = new List<double>();
        }
        //calculate the initial flow direction
        public void FlowDirection(double ratio)
        {
            double totalLength = 0.0;
            double tempLength = 0.0;
            for (int i = 0; i < points.Count - 1; i++)
            {
                totalLength += points[i].Length(points[i + 1]);
            }
            length = totalLength;

            for (int i = 0; i < points.Count - 1; i++)
            {
                tempLength += points[i].Length(points[i + 1]);
                if (tempLength >= (totalLength * ratio))
                {
                    double x1 = points[i].GetX();
                    double y1 = points[i].GetY();
                    double x2 = points[i + 1].GetX();
                    double y2 = points[i + 1].GetY();
                    double length12 = points[i].Length(points[i + 1]);//length of reach bewteen points[p] and points[p+1]
                    double l = tempLength - (totalLength * ratio);//distance between feature point and points[i+1]
                    //feature point coordinates
                    double x = x2 - ((x2 - x1) * l / length12);
                    double y = y2 - ((y2 - y1) * l / length12);
                    fPoint = new Point(x, y);
                    IPoint point = new PointClass();
                    point.PutCoords(x, y);

                    ////display feature points when needed
                    //IMarkerSymbol markerSymbol = new SimpleMarkerSymbolClass();
                    //IColor pColor = new RgbColor();
                    //pColor.RGB = 0;
                    //markerSymbol.Color = pColor;
                    //markerSymbol.Size = 4;
                    //IMarkerElement pMarkerElement = new MarkerElementClass();
                    //pMarkerElement.Symbol = markerSymbol;
                    //IElement pElement = pMarkerElement as IElement;
                    //pElement.Geometry = point as IGeometry;
                    //axMapControl1.ActiveView.GraphicsContainer.AddElement(pElement, 0);
                    //axMapControl1.ActiveView.Refresh();



                    //display line from start node to end node when needed
                    //IElementCollection pElements = new ElementCollectionClass();
                    //ISegmentCollection pPath = new PathClass();
                    ////set start node and end node of line
                    //IPoint startPoint = new PointClass();
                    //startPoint.PutCoords(stream.points[0].x, stream.points[0].y);
                    //IPoint endPoint = new PointClass();
                    //endPoint.PutCoords(x, y);
                    //ILine pLine = new LineClass();
                    //pLine.FromPoint = startPoint;
                    //pLine.ToPoint = endPoint;
                    //pPath.AddSegment(pLine as ISegment);
                    //IGeometryCollection pPolyline = new PolylineClass();
                    //pPolyline.AddGeometry(pPath as IGeometry);
                    ////set line symbol
                    //ISimpleLineSymbol lineSymbol = new SimpleLineSymbolClass();
                    //IColor pColor = new RgbColor();
                    //pColor.RGB = 255;
                    //lineSymbol.Color = pColor;
                    //lineSymbol.Style = esriSimpleLineStyle.esriSLSInsideFrame;
                    //lineSymbol.Width = 1;
                    //ILineElement pLineElement = new LineElementClass();
                    //pLineElement.Symbol = lineSymbol;
                    //IElement pElement = pLineElement as IElement;
                    //pElement.Geometry = pPolyline as IGeometry;
                    //axMapControl1.ActiveView.GraphicsContainer.AddElement(pElement, 0);
                    //axMapControl1.ActiveView.Refresh();

                    flowDirection = points[0].ReachAngle(new Point(x, y));
                    return;
                }
            }
            flowDirection = - 1;
        }
        //get the densified point
        public void GetProfilePoints(double step)
        {
            profilePoints.Clear();
            distance.Clear();
            for (int i = 0; i < points.Count - 1; i++)
            {
                profilePoints.Add(points[i]);
                double disAB = points[i].Length(points[i + 1]);
                double dX = step / disAB * (points[i + 1].GetX() - points[i].GetX());
                double dY = step / disAB * (points[i + 1].GetY() - points[i].GetY());
                int times = (int)Math.Ceiling(disAB / step - 1);
                double nowX = points[i].GetX();
                double nowY = points[i].GetY();
                for (int j = 0; j < times; j++)
                {
                    nowX += dX;
                    nowY += dY;
                    Point nowP = new Point(nowX, nowY);
                    profilePoints.Add(nowP);
                    distance.Add(step);
                }
                if (nowX != points[i + 1].GetX() && nowY != points[i + 1].GetY())
                {
                    profilePoints.Add(points[i + 1]);
                    Point nowP = new Point(nowX, nowY);
                    double disCB = nowP.Length(points[i + 1]);
                    distance.Add(disCB);
                }
                distance.Add(0);
            }
        }
    }
}
