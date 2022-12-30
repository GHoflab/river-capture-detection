using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DataSourcesRaster;
using System.IO;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.ConversionTools;
using ESRI.ArcGIS.Controls;
using Main.Interface;
using Main.Operation;
using System.Diagnostics;

namespace Main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private IList<Reach> reaches = new List<Reach>();
        private IList<Group> groups = new List<Group>();
        private string defaultROIFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string defaultChiPlotFolder = "";//Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private double defaultRatio = 0.5;
        private double defaultCaptureElbowThre = 120;
        private double defaultTributaryAngleThre = 120;
        private double defaultOppositeReachThre = 145;
        private double defaultExponent = 0.45;
        private string groupPath = "";
        private int main = -1;
        private int t1 = -1;
        private int t2 = -1;

        private ILayer riverLayer;
        private ILayer groupLayer;

        # region View
        private void statusBarToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (statusBarToolStripMenuItem.Checked)
                statusStrip1.Show();
            else
                statusStrip1.Hide();
        }

        private void toolBarToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (toolBarToolStripMenuItem.Checked)
                axToolbarControl1.Show();
            else
                axToolbarControl1.Hide();
        }

        private void eagleeyeMapToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (eagleeyeMapToolStripMenuItem.Checked)
                axMapControl2.Show();

            else
                axMapControl2.Hide();
        }
        #endregion

        #region File
        private void openShpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOFD = new OpenFileDialog();
            pOFD.Title = "OpenShp";
            pOFD.Filter = "*.shp|*.shp";
            if (pOFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = pOFD.FileName;
                string fileFolder = System.IO.Path.GetDirectoryName(filePath);
                string fileName = System.IO.Path.GetFileName(filePath);
                axMapControl1.AddShapeFile(fileFolder, fileName);
                IFeatureLayer pFL = axMapControl1.Map.get_Layer(0) as IFeatureLayer;
                if (pFL.FeatureClass.ShapeType == esriGeometryType.esriGeometryPolyline)
                {
                    IFeatureClass pFC = pFL.FeatureClass;
                    IGeoFeatureLayer pGFL = pFL as IGeoFeatureLayer;
                    ISimpleRenderer pSimpleRenderer = new SimpleRendererClass();
                    ////set symbol of each line and display the lines
                    ISimpleLineSymbol pNextSymbol = new SimpleLineSymbolClass();
                    pNextSymbol.Width = 3;
                    IColor pColor = new RgbColor();
                    //pColor.RGB = 128 * 256 * 256 + 128 * 256 + 128;
                    pColor.RGB = 252 * 256 * 256 + 147 * 256 + 10;
                    pNextSymbol.Color = pColor;
                    pSimpleRenderer.Symbol = pNextSymbol as ISymbol;

                    //IFeatureCursor pFeatureCursor = pFC.Search(null, false);
                    //int n = pFC.FeatureCount(null);
                    //for (int i = 0; i < n; i++)
                    //{
                    //    IFeature pFeature = pFeatureCursor.NextFeature();
                    //    ISimpleLineSymbol pNextSymbol = new SimpleLineSymbolClass();
                    //    pNextSymbol.Width = 3;
                    //    IColor pColor = new RgbColor();
                    //    //pColor.RGB = 128 * 256 * 256 + 128 * 256 + 128;
                    //    pColor.RGB = 252 * 256 * 256 + 147 * 256 + 10;
                    //    pNextSymbol.Color = pColor;
                    //    pSimpleRenderer.Symbol = pNextSymbol as ISymbol;
                    //}
                    IFeatureRenderer pFR = pSimpleRenderer as IFeatureRenderer;
                    pGFL.Renderer = pFR;
                }
                axMapControl1.ActiveView.Refresh();
                axMapControl2.AddLayer(axMapControl1.Map.get_Layer(0));
                axTOCControl1.Update();
            }
        }

        private void openRasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.CheckFileExists = true;
            pOpenFileDialog.Title = "Open Raster";
            pOpenFileDialog.Filter = "Raster File（*.*)|*.bmp;*.tif;*.jpg;*img;*.adf;*.png";
            if (pOpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string pRasterFileName = pOpenFileDialog.FileName;
                string pPath = System.IO.Path.GetDirectoryName(pRasterFileName);
                string pFileName = System.IO.Path.GetFileName(pRasterFileName);

                IWorkspaceFactory pWorkspaceFactory = new RasterWorkspaceFactory();
                IWorkspace pWorkspace = pWorkspaceFactory.OpenFromFile(pPath, 0);
                IRasterWorkspace pRasterWorkspace = pWorkspace as IRasterWorkspace;
                IRasterDataset pRasterDataset = pRasterWorkspace.OpenRasterDataset(pFileName);

                //Raster pyramid
                IRasterPyramid3 pRasPyrmid;
                pRasPyrmid = pRasterDataset as IRasterPyramid3;
                if (pRasPyrmid != null)
                {
                    if (!(pRasPyrmid.Present))
                    {
                        pRasPyrmid.Create();
                    }
                }
                //Display raster
                IRaster pRaster = pRasterDataset.CreateDefaultRaster();
                IRasterLayer pRasterLayer;
                pRasterLayer = new RasterLayerClass();
                pRasterLayer.CreateFromRaster(pRaster);
                ILayer pLayer = pRasterLayer as ILayer;
                axMapControl1.Map.AddLayer(pLayer);
                axMapControl2.Map.AddLayer(pLayer);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog pSaveFileDialog = new SaveFileDialog();
            //pSaveFileDialog.InitialDirectory = defaultSavePath;
            pSaveFileDialog.Title = "Save Map Document";
            pSaveFileDialog.OverwritePrompt = true;
            pSaveFileDialog.Filter = "Map Document（*.mxd）|*.mxd";
            pSaveFileDialog.RestoreDirectory = true;

            if (pSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string mxdSavePath = pSaveFileDialog.FileName;
                IMapDocument pMapDocument = new MapDocumentClass();
                pMapDocument.New(mxdSavePath);
                pMapDocument.ReplaceContents(axMapControl1.Map as IMxdContents);
                pMapDocument.Save(true, true);
                pMapDocument.Close();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dR = MessageBox.Show("The system will be exited", "Exit", MessageBoxButtons.OKCancel);
            if (dR == DialogResult.OK)
            {
                System.Environment.Exit(0);
            }
        }
        #endregion

        #region status bar
        private void axMapControl1_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            string sMapUnits = axMapControl1.Map.MapUnits.ToString();
            sMapUnits = sMapUnits.Substring(4);
            toolStripStatusLabel1.Text = string.Format("Current Coordinates：X={0:#.###} Y={1:#.###} {2}", e.mapX, e.mapY, sMapUnits);
        }
        #endregion

        #region eagle eye map
        private IEnvelope pEnv;
        private bool bCanDrag;
        private IPoint pMoveRectPoint;

        private void axMapControl1_OnMapReplaced(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
        {
            //clear map
            if (axMapControl1.LayerCount > 0)
            {
                axMapControl2.ClearLayers();
            }
            //copy spatial reference
            axMapControl2.SpatialReference = axMapControl1.SpatialReference;
            for (int i = axMapControl1.LayerCount - 1; i >= 0; i--)
            {
                //copy layer order
                ILayer pLayer = axMapControl1.get_Layer(i);
                if (pLayer is IGroupLayer || pLayer is ICompositeLayer)
                {
                    ICompositeLayer pCompositeLayer = (ICompositeLayer)pLayer;
                    for (int j = pCompositeLayer.Count - 1; j >= 0; j--)
                    {
                        ILayer pSubLayer = pCompositeLayer.get_Layer(j);
                        IFeatureLayer pFeatureLayer = pSubLayer as IFeatureLayer;
                        if (pFeatureLayer != null)
                        {
                            if (pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint && pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryMultipoint)
                            {
                                axMapControl2.AddLayer(pLayer);
                            }
                        }
                    }
                }
                else
                {
                    IFeatureLayer pFeatureLayer = pLayer as IFeatureLayer;
                    if (pFeatureLayer != null)
                    {
                        if (pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint && pFeatureLayer.FeatureClass.ShapeType != esriGeometryType.esriGeometryMultipoint)
                        {
                            axMapControl2.AddLayer(pLayer);
                        }
                    }
                }
                axMapControl2.Extent = axMapControl2.FullExtent;
                pEnv = axMapControl1.Extent as IEnvelope;
                //draw rectangle
                DrawRectangle(pEnv);

                axMapControl2.ActiveView.Refresh();
            }
        }

        private void DrawRectangle(IEnvelope pEnvelope)
        {
            IGraphicsContainer pGraphicsContainer = axMapControl2.Map as IGraphicsContainer;
            IActiveView pActiveView = pGraphicsContainer as IActiveView;
            pGraphicsContainer.DeleteAllElements();

            IRectangleElement pRectangleElement = new RectangleElementClass();
            IElement pElement = pRectangleElement as IElement;
            pElement.Geometry = pEnvelope;

            //red, width=1
            IRgbColor pColor = new RgbColorClass();
            pColor = GetRgbColor(255, 0, 0);
            pColor.Transparency = 255;
            ILineSymbol pOutLine = new SimpleLineSymbolClass();
            pOutLine.Width = 1;
            pOutLine.Color = pColor;

            //transparent fill
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pColor = new RgbColorClass();
            pColor.Transparency = 0;
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutLine;

            //add rectangle
            IFillShapeElement pFillShapeElement = pElement as IFillShapeElement;
            pFillShapeElement.Symbol = pFillSymbol;
            pGraphicsContainer.AddElement((IElement)pFillShapeElement, 0);

            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private IRgbColor GetRgbColor(int r, int g, int b)
        {
            IRgbColor pColor = new RgbColor();
            pColor.Red = r;
            pColor.Green = g;
            pColor.Blue = b;
            return pColor;
        }

        private void axMapControl1_OnExtentUpdated(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            IEnvelope pEnvelope = (IEnvelope)e.newEnvelope;
            pEnv = (IEnvelope)e.newEnvelope;
            DrawRectangle(pEnvelope);
        }

        private void axMapControl2_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            //left click drag: translate rectangle
            if (e.button == 1)
            {
                if (e.mapX > pEnv.XMin && e.mapY > pEnv.YMin && e.mapX < pEnv.XMax && e.mapY < pEnv.YMax)
                {
                    bCanDrag = true;
                }
                pMoveRectPoint = new PointClass();
                pMoveRectPoint.PutCoords(e.mapX, e.mapY);
            }
            //right click drag: redraw rectangle and range
            if (e.button == 2)
            {
                IEnvelope pEnvelope = axMapControl2.TrackRectangle();
                IPoint pTempPoint = new PointClass();
                pTempPoint.PutCoords(pEnvelope.XMin + pEnvelope.Width / 2, pEnvelope.YMin + pEnvelope.Height / 2);
                axMapControl1.Extent = pEnvelope;

                axMapControl1.CenterAt(pTempPoint);
            }
        }

        private void axMapControl2_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (e.mapX > pEnv.XMin && e.mapY > pEnv.YMin && e.mapX < pEnv.XMax && e.mapY < pEnv.YMax)
            {
                //change cursor to hand
                axMapControl2.MousePointer = esriControlsMousePointer.esriPointerHand;

                if (e.button == 2)              //right click to recover cursor
                {
                    axMapControl2.MousePointer = esriControlsMousePointer.esriPointerDefault;
                }
            }
            else //recover cursor
            {
                axMapControl2.MousePointer = esriControlsMousePointer.esriPointerDefault;
            }

            if (bCanDrag)
            {
                double dx, dy;
                dx = e.mapX - pMoveRectPoint.X;
                dy = e.mapY - pMoveRectPoint.Y;
                pEnv.Offset(dx, dy);      //get offset and change envelope
                pMoveRectPoint.PutCoords(e.mapX, e.mapY);
                DrawRectangle(pEnv);
                axMapControl1.Extent = pEnv;
            }
        }

        private void axMapControl2_OnMouseUp(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseUpEvent e)
        {
            if (e.button == 1 && pMoveRectPoint != null)
            {
                if (e.mapX == pMoveRectPoint.X && e.mapY == pMoveRectPoint.Y)
                {
                    axMapControl1.CenterAt(pMoveRectPoint);
                }
                //unable to drag
                bCanDrag = false;
            }
        }
        #endregion

        #region group
        private void groupRegionsOfInterestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupRegionsOfInterest pFROI = new GroupRegionsOfInterest(axMapControl1.Map);
            pFROI.ShowDialog();
            if (pFROI.OkClicked)
            {
                riverLayer = pFROI.riverLayer;
                IFeatureLayer pFL = pFROI.riverLayer as IFeatureLayer;
                IFeatureCursor pFC = pFL.Search(null, false);
                IFeature pFeature = pFC.NextFeature();
                //int num = 0;
                while (pFeature != null)
                {
                    int num = Convert.ToInt32(pFeature.get_Value(0));
                    Reach reach = new Reach(pFeature, num);
                    //num++;
                    reaches.Add(reach);
                    pFeature = pFC.NextFeature();
                }
                ReachesOper reachOper = new ReachesOper();
                groups = reachOper.GroupReaches(ref reaches);

                IFeatureClass pFClass = pFL.FeatureClass;
                IGeoDataset pGeoDataset = (IGeoDataset)pFClass;
                ISpatialReference pFSR = pGeoDataset.SpatialReference;
                IFields pFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;

                //set attribute field
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = (IFieldEdit)pField;

                //create shape field
                pFieldEdit.Name_2 = "shape";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;

                //define geometry type by esriFieldTypeGeometry
                IGeometryDef pGeoDef = new GeometryDefClass();
                IGeometryDefEdit pGeoDefEdit = (IGeometryDefEdit)pGeoDef;
                pGeoDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                pGeoDefEdit.SpatialReference_2 = pFSR;

                pFieldEdit.GeometryDef_2 = pGeoDef;
                pFieldsEdit.AddField(pField);

                IField nameField = new FieldClass();
                IFieldEdit nameFieldEdit = (IFieldEdit)nameField;
                nameFieldEdit.Name_2 = "MainStream";
                nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                nameFieldEdit.Length_2 = 5;
                pFieldsEdit.AddField(nameField);

                nameField = new FieldClass();
                nameFieldEdit = (IFieldEdit)nameField;
                nameFieldEdit.Name_2 = "Tributary1";
                nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                nameFieldEdit.Length_2 = 5;
                pFieldsEdit.AddField(nameField);

                nameField = new FieldClass();
                nameFieldEdit = (IFieldEdit)nameField;
                nameFieldEdit.Name_2 = "Tributary2";
                nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                nameFieldEdit.Length_2 = 5;
                pFieldsEdit.AddField(nameField);

                nameField = new FieldClass();
                nameFieldEdit = (IFieldEdit)nameField;
                nameFieldEdit.Name_2 = "ElbowCondition";
                nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                nameFieldEdit.Length_2 = 1;
                pFieldsEdit.AddField(nameField);

                nameField = new FieldClass();
                nameFieldEdit = (IFieldEdit)nameField;
                nameFieldEdit.Name_2 = "roughCaptured";
                nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                nameFieldEdit.Length_2 = 1;
                pFieldsEdit.AddField(nameField);

                nameField = new FieldClass();
                nameFieldEdit = (IFieldEdit)nameField;
                nameFieldEdit.Name_2 = "chiPlotCaptured";
                nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                nameFieldEdit.Length_2 = 1;
                pFieldsEdit.AddField(nameField);

                //open workspace
                string shapeName = System.IO.Path.GetFileNameWithoutExtension(pFROI.savePath);
                string path = System.IO.Path.GetDirectoryName(pFROI.savePath);
                IWorkspaceFactory pWF = new ShapefileWorkspaceFactory();
                IFeatureWorkspace pFW = pWF.OpenFromFile(path, 0) as IFeatureWorkspace;
                IFeatureClass chiFC = pFW.CreateFeatureClass(shapeName, pFields, null, null, esriFeatureType.esriFTSimple, "Shape", "");

                for (int i = 0; i < groups.Count; i++)
                {
                    ESRI.ArcGIS.Geometry.IPoint pPoint = new ESRI.ArcGIS.Geometry.Point();
                    pPoint.X = groups[i].node.GetX();
                    pPoint.Y = groups[i].node.GetY();
                    //add attribute for a point;
                    IFeature feature = chiFC.CreateFeature();
                    feature.Shape = pPoint;
                    feature.Store();

                    feature.set_Value(2, groups[i].main);
                    feature.set_Value(3, groups[i].tributary1);
                    feature.set_Value(4, groups[i].tributary2);
                    feature.Store();
                }
                axMapControl1.AddShapeFile(path, shapeName + ".shp");
                groupLayer = axMapControl1.get_Layer(0);
            }
        }
        #endregion

        private void roughIdentificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlatformFeatureCaptureIdentification pPFCI = new PlatformFeatureCaptureIdentification(axMapControl1.Map, defaultRatio, defaultCaptureElbowThre, defaultTributaryAngleThre, defaultOppositeReachThre);
            pPFCI.ShowDialog();
            if (pPFCI.OKClicked)
            {
                groupLayer = pPFCI.roiLayer;
                bool riverLayerReaded = false;
                if (riverLayer != null)
                {
                    IDataLayer riverDLayer = riverLayer as IDataLayer;
                    IWorkspaceName riverWName = ((IDatasetName)(riverDLayer.DataSourceName)).WorkspaceName;
                    IDataLayer selectDLayer = pPFCI.riverLayer as IDataLayer;
                    IWorkspaceName selectWName = ((IDatasetName)(selectDLayer.DataSourceName)).WorkspaceName;
                    if (riverWName == selectWName && riverLayer.Name == pPFCI.riverLayer.Name)
                    {
                        riverLayerReaded = true;
                    }
                }
                if (!riverLayerReaded)
                {
                    riverLayer = pPFCI.riverLayer;
                    IFeatureLayer pFL = pPFCI.riverLayer as IFeatureLayer;
                    IFeatureCursor pFC = pFL.Search(null, false);
                    IFeature pFeature = pFC.NextFeature();
                    //int num = 0;
                    while (pFeature != null)
                    {
                        int num = Convert.ToInt32(pFeature.get_Value(0));
                        Reach reach = new Reach(pFeature, num);
                        //num++;
                        reaches.Add(reach);
                        pFeature = pFC.NextFeature();
                    }
                    ReachesOper reachOper = new ReachesOper();
                    groups = reachOper.GroupReaches(ref reaches);
                }
                for (int i = 0; i < reaches.Count; i++)
                    reaches[i].FlowDirection(pPFCI.ratio);

                for (int i = 0; i < groups.Count; i++)
                {
                    groups[i].roughCaptured = true;
                    groups[i].Judgement(reaches, 180 - pPFCI.captureElbowThre, pPFCI.tributaryAngleThre, pPFCI.oppositeReachThre, pPFCI.windGapJudged);
                }
                IFeatureLayer pRoiFL = pPFCI.roiLayer as IFeatureLayer;
                IFeatureCursor pRoiFC = pRoiFL.Search(null, false);
                IFeature pRoiFeature = pRoiFC.NextFeature();
                for (int i = 0; i < groups.Count; i++)
                {
                    if (groups[i].roughCaptured)
                    {
                        pRoiFeature.set_Value(6, 1);
                        pRoiFeature.Store();

                    }
                    else
                    {
                        pRoiFeature.set_Value(6, 0);
                        pRoiFeature.Store();
                    }
                    pRoiFeature = pRoiFC.NextFeature();
                }
                IFeatureClass pRoiFeatureClass = pRoiFL.FeatureClass;
                IGeoFeatureLayer pGFL = pRoiFL as IGeoFeatureLayer;
                IFeatureCursor pFeatureCursor = pRoiFeatureClass.Search(null, false);
                IUniqueValueRenderer pUVR = new UniqueValueRenderer();
                pUVR.FieldCount = 1;
                pUVR.set_Field(0, "RoughCaptu");
                ISimpleMarkerSymbol pSFS = new SimpleMarkerSymbolClass();
                pUVR.DefaultSymbol = pSFS as ISymbol;
                pUVR.UseDefaultSymbol = false;
                int n = pRoiFeatureClass.FeatureCount(null);
                IRandomColorRamp pRCR = new RandomColorRampClass();
                pRCR.StartHue = 0;
                pRCR.EndHue = 120;
                pRCR.MinSaturation = 50;
                pRCR.MaxSaturation = 100;
                pRCR.MinValue = 50;
                pRCR.MaxValue = 100;
                pRCR.UseSeed = false;
                pRCR.Size = 2;
                bool btrue = true;
                pRCR.CreateRamp(out btrue);
                IEnumColors pEC = pRCR.Colors;
                IColor color1 = pEC.Next();
                IColor color2 = pEC.Next();
                for (int i = 0; i < n; i++)
                {
                    IFeature pFeature = pFeatureCursor.NextFeature();
                    ISimpleMarkerSymbol pNextSymbol = new SimpleMarkerSymbolClass();
                    int value = Convert.ToInt32(pFeature.get_Value(6).ToString());
                    if (value == 0)
                    {
                        pNextSymbol.Color = color1;
                    }
                    else
                    {
                        pNextSymbol.Color = color2;
                    }
                    pUVR.AddValue(pFeature.get_Value(6).ToString(), "", (ISymbol)pNextSymbol);
                }
                IFeatureRenderer pFR = pUVR as IFeatureRenderer;
                pGFL.Renderer = pFR;
                axMapControl1.ActiveView.Refresh();
                axTOCControl1.Update();
                axMapControl1.Refresh();
            }
        }

        private void oneRiverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateOneRiverChiPlot pGORCP = new GenerateOneRiverChiPlot(axMapControl1.Map, defaultExponent, defaultChiPlotFolder);
            pGORCP.ShowDialog();
            if (pGORCP.OKClicked)
            {
                bool riverLayerReaded = false;
                if (riverLayer != null)
                {
                    IDataLayer riverDLayer = riverLayer as IDataLayer;
                    IWorkspaceName riverWName = ((IDatasetName)(riverDLayer.DataSourceName)).WorkspaceName;
                    IDataLayer selectDLayer = pGORCP.riverLayer as IDataLayer;
                    IWorkspaceName selectWName = ((IDatasetName)(selectDLayer.DataSourceName)).WorkspaceName;
                    if (riverWName == selectWName && riverLayer.Name == pGORCP.riverLayer.Name)
                    {
                        riverLayerReaded = true;
                    }
                }
                if (!riverLayerReaded)
                {
                    riverLayer = pGORCP.riverLayer;
                    IFeatureLayer pFL = pGORCP.riverLayer as IFeatureLayer;
                    IFeatureCursor pFC = pFL.Search(null, false);
                    IFeature pFeature = pFC.NextFeature();
                    //int num = 0;
                    while (pFeature != null)
                    {
                        int num = Convert.ToInt32(pFeature.get_Value(0));
                        Reach reach = new Reach(pFeature, num);
                        //num++;
                        reaches.Add(reach);
                        pFeature = pFC.NextFeature();
                    }
                    ReachesOper reachOper = new ReachesOper();
                    groups = reachOper.GroupReaches(ref reaches);
                }
                Matrix accRaster = new Matrix(pGORCP.accLayer);
                Matrix heightRaster = new Matrix(pGORCP.heightLayer);
                for (int i = 0; i < reaches.Count; i++)
                {
                    reaches[i].GetProfilePoints(pGORCP.step);
                    accRaster.Nearest(reaches[i], ref reaches[i].acc);
                    heightRaster.Bilinear(reaches[i], ref reaches[i].elevation);
                }
                ChiOper chiOper = new ChiOper();
                string print = chiOper.GenerateOneRiverChi(pGORCP.riverLayer, ref reaches, pGORCP.exponent, pGORCP.minHeight, pGORCP.savePath);
                MessageBox.Show(print);
            }
        }

        private void allGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateAllGroupsChiPlot pGAGCP = new GenerateAllGroupsChiPlot(axMapControl1.Map, defaultExponent, defaultChiPlotFolder);
            pGAGCP.ShowDialog();
            if (pGAGCP.OKClicked)
            {
                groupLayer = pGAGCP.groupLayer;
                groupPath = pGAGCP.savePath;
                Matrix accRaster = new Matrix(pGAGCP.accLayer);
                Matrix heightRaster = new Matrix(pGAGCP.heightLayer);
                for (int i = 0; i < reaches.Count; i++)
                {
                    reaches[i].GetProfilePoints(pGAGCP.step);
                    accRaster.Nearest(reaches[i], ref reaches[i].acc);
                    heightRaster.Bilinear(reaches[i], ref reaches[i].elevation);
                }
                for (int i = 0; i < groups.Count; i++)
                {
                    //ChiOper pchiOper = new ChiOper();
                    //pchiOper.GenerateAllGroups(ref reaches, groups[i], pGAGCP.exponent, pGAGCP.minHeight, pGAGCP.beheadedChoice, pGAGCP.savePath);
                    if (groups[i].roughCaptured)
                    {
                        ChiOper pchiOper = new ChiOper();
                        pchiOper.GenerateAllGroups(ref reaches, groups[i], pGAGCP.exponent, pGAGCP.minHeight, pGAGCP.beheadedChoice, pGAGCP.savePath);
                    }
                }
            }
        }

        private void oneGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OneGroupIdentificationByChiPlot pOGIBCP = new OneGroupIdentificationByChiPlot();
            pOGIBCP.ShowDialog();
            if (pOGIBCP.OKClicked)
            {
                ChiIdentification pIden = new ChiIdentification(pOGIBCP.captureFile, pOGIBCP.reservedFile, pOGIBCP.beheadedFile);
                //ChiIdentification pIden = new ChiIdentification(pOGIBCP.captureFile, pOGIBCP.reservedFile);
                pIden.BaseLevelUnify();
                bool result = pIden.Identification();
                if (result)
                    MessageBox.Show("Involving capture!");
                else
                    MessageBox.Show("Not involving capture!");
            }
        }

        private void allGroupsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AllGroupsIdentificationByChiPlot pAGIBCP = new AllGroupsIdentificationByChiPlot(axMapControl1.Map);
            pAGIBCP.ShowDialog();
            if (pAGIBCP.OKClicked)
            {
                groupLayer = pAGIBCP.groupLayer;
                groupPath = pAGIBCP.groupPath;
                DirectoryInfo root = new DirectoryInfo(groupPath);
                DirectoryInfo[] dics = root.GetDirectories();
                foreach (DirectoryInfo dic in dics)
                {
                    string groupFolder = dic.FullName;
                    int mainID = Convert.ToInt32(dic.Name.Split('_')[0]);
                    DirectoryInfo groupFiles = new DirectoryInfo(groupFolder);
                    FileInfo[] files = groupFiles.GetFiles();
                    string capturePath = groupFolder + "//capture.csv";
                    string reversedPath = groupFolder + "//reversed.csv";
                    string beheadedPath = groupFolder + "//beheaded.csv";
                    ChiIdentification pIden = new ChiIdentification(capturePath, reversedPath, beheadedPath);
                    //ChiIdentification pIden = new ChiIdentification(capturePath, reversedPath);
                    pIden.BaseLevelUnify();
                    bool result = pIden.Identification();
                    if (result)
                    {
                        for (int i = 0; i < groups.Count; i++)
                        {
                            groups[i].chiPlotCaptured = false;
                            if (groups[i].main == mainID)
                            {
                                groups[i].chiPlotCaptured = true;
                            }
                        }
                    }
                }
                IFeatureLayer pRoiFL = pAGIBCP.groupLayer as IFeatureLayer;
                IFeatureCursor pRoiFC = pRoiFL.Search(null, false);
                IFeature pRoiFeature = pRoiFC.NextFeature();
                for (int i = 0; i < groups.Count; i++)
                {
                    if (groups[i].chiPlotCaptured)
                    {
                        pRoiFeature.set_Value(7, 1);
                        pRoiFeature.Store();

                    }
                    else
                    {
                        pRoiFeature.set_Value(7, 0);
                        pRoiFeature.Store();
                    }
                    pRoiFeature = pRoiFC.NextFeature();
                }
                IFeatureClass pRoiFeatureClass = pRoiFL.FeatureClass;
                IGeoFeatureLayer pGFL = pRoiFL as IGeoFeatureLayer;
                IFeatureCursor pFeatureCursor = pRoiFeatureClass.Search(null, false);
                IUniqueValueRenderer pUVR = new UniqueValueRenderer();
                pUVR.FieldCount = 1;
                pUVR.set_Field(0, "chiPlotCap");
                ISimpleMarkerSymbol pSFS = new SimpleMarkerSymbolClass();
                pUVR.DefaultSymbol = pSFS as ISymbol;
                pUVR.UseDefaultSymbol = false;
                int n = pRoiFeatureClass.FeatureCount(null);
                IRandomColorRamp pRCR = new RandomColorRampClass();
                pRCR.StartHue = 0;
                pRCR.EndHue = 120;
                pRCR.MinSaturation = 50;
                pRCR.MaxSaturation = 100;
                pRCR.MinValue = 50;
                pRCR.MaxValue = 100;
                pRCR.UseSeed = false;
                pRCR.Size = 2;
                bool btrue = true;
                pRCR.CreateRamp(out btrue);
                IEnumColors pEC = pRCR.Colors;
                IColor color1 = pEC.Next();
                IColor color2 = pEC.Next();
                for (int i = 0; i < n; i++)
                {
                    IFeature pFeature = pFeatureCursor.NextFeature();
                    ISimpleMarkerSymbol pNextSymbol = new SimpleMarkerSymbolClass();
                    int value = Convert.ToInt32(pFeature.get_Value(7).ToString());
                    if (value == 0)
                    {
                        pNextSymbol.Color = color1;
                    }
                    else
                    {
                        pNextSymbol.Color = color2;
                    }
                    pUVR.AddValue(pFeature.get_Value(7).ToString(), "", (ISymbol)pNextSymbol);
                }
                IFeatureRenderer pFR = pUVR as IFeatureRenderer;
                pGFL.Renderer = pFR;
                axMapControl1.ActiveView.Refresh();
                axTOCControl1.Update();
                axMapControl1.Refresh();
            }
        }

        private void displayOneΧplotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayOneChiPlot pDOCP = new DisplayOneChiPlot();
            pDOCP.ShowDialog();
        }

        private void displayΧplotOfGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayGroupChiPlot pDGCP = new DisplayGroupChiPlot();
            pDGCP.ShowDialog();
        }

        #region settings
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings pSetting = new Settings(defaultROIFolder, defaultChiPlotFolder, defaultRatio, defaultCaptureElbowThre, defaultTributaryAngleThre, defaultOppositeReachThre, defaultExponent);
            pSetting.ShowDialog();
            if (pSetting.OKClicked)
            {
                defaultROIFolder = pSetting.defaultROIFolder;
                defaultChiPlotFolder = pSetting.defaultChiPlotFolder;
                defaultRatio = pSetting.defaultRatio;
                defaultCaptureElbowThre = pSetting.defaultCaptureElbowThre;
                defaultTributaryAngleThre = pSetting.defaultTributaryAngleThre;
                defaultOppositeReachThre = pSetting.defaultOppositeReachThre;
                defaultExponent = pSetting.defaultExponent;
            }
        }
        #endregion
        private ILayer pLayer;
        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button != 2)
                return;

            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap map = null;
            object other = null; object index = null;
            axTOCControl1.HitTest(e.x, e.y, ref item, ref map, ref pLayer, ref other, ref index);
        }

        private void axTOCControl1_OnMouseUp(object sender, ITOCControlEvents_OnMouseUpEvent e)
        {
            if (e.button == 2 && pLayer != null)
            {
                contextMenuStrip1.Show(e.x, e.y + menuStrip1.Height + axToolbarControl1.Height);
            }
        }

        private void tableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFeatureLayer pFL = (IFeatureLayer)pLayer;
            IFeatureClass pFC = pFL.FeatureClass;

            IFields pFields = pFC.Fields;
            string[] column = new string[pFields.FieldCount - 2];
            for (int i = 2; i < pFields.FieldCount; i++)
            {
                column.SetValue(pFields.Field[i].Name, i - 2);
            }

            IQueryFilter pQF = new QueryFilterClass();
            int featureCount = pFC.FeatureCount(pQF);
            string[][] tableList = new string[featureCount][];
            for (int i = 0; i < featureCount; i++)
            {
                IFeature pFeature = pFC.GetFeature(i);
                string[] row = new string[pFields.FieldCount - 2];
                for (int j = 2; j < pFields.FieldCount; j++)
                {
                    row[j - 2] = pFeature.get_Value(j).ToString();

                }
                tableList[i] = row;
            }


            AttributeTable table = new AttributeTable(column, tableList);
            table.ShowDialog();
        }

        private void axMapControl1_OnViewRefreshed(object sender, IMapControlEvents2_OnViewRefreshedEvent e)
        {
            if (axMapControl2.LayerCount > 0)
            {
                axMapControl2.ClearLayers();
            }
            //copy spatial reference
            axMapControl2.SpatialReference = axMapControl1.SpatialReference;
            for (int i = axMapControl1.LayerCount - 1; i >= 0; i--)
            {
                //copy layer order
                ILayer pLayer = axMapControl1.get_Layer(i);
                
                axMapControl2.AddLayer(pLayer);
                
                axMapControl2.Extent = axMapControl2.FullExtent;
                pEnv = axMapControl1.Extent as IEnvelope;
                //draw rectangle envelope
                DrawRectangle(pEnv);

                axMapControl2.ActiveView.Refresh();
            }
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (e.button == 2)
            {

                IPoint pPoint = new PointClass();
                pPoint.PutCoords(e.mapX, e.mapY);
                IGeometry pGeometry = pPoint as IGeometry;
                ITopologicalOperator pTopo = pPoint as ITopologicalOperator;
                IGeometry pBuffer = pTopo.Buffer(1000);
                IFeatureLayer pFL = groupLayer as IFeatureLayer;
                IFeatureClass pFC = pFL.FeatureClass;
                ISpatialFilter pSF = new SpatialFilterClass();
                pSF.Geometry = pBuffer;
                pSF.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                IQueryFilter pFilter = pSF;

                IFeatureCursor pFeatureCursor = pFL.Search(pFilter, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                if (pFeature != null)
                {
                    if (pFeature.get_Value(6).ToString() == "1")
                    {
                        contextMenuStrip2.Show(e.x + axTOCControl1.Width, e.y + menuStrip1.Height + axToolbarControl1.Height);
                        main = Convert.ToInt32(pFeature.get_Value(2).ToString());
                        t1 = Convert.ToInt32(pFeature.get_Value(3).ToString());
                        t2 = Convert.ToInt32(pFeature.get_Value(4).ToString());
                    }
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string path1 = groupPath + "/" + main.ToString() + "_" + t1.ToString() + "/";
            string path2 = groupPath + "/" + main.ToString() + "_" + t2.ToString() + "/";
            if (Directory.Exists(path1))
            {
                DisplayGroupChiPlot pDGCP = new DisplayGroupChiPlot(path1 + "capture.csv", path1 + "reversed.csv", path1 + "beheaded.csv");
                pDGCP.ShowDialog();
            }
            if (Directory.Exists(path2))
            {
                DisplayGroupChiPlot pDGCP = new DisplayGroupChiPlot(path2 + "capture.csv", path2 + "reversed.csv", path2 + "beheaded.csv");
                pDGCP.ShowDialog();
            }
            if (!Directory.Exists(path1) && !Directory.Exists(path2))
            {
                MessageBox.Show("File not found!");
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutUs pAU = new AboutUs();
            pAU.ShowDialog();
        }
    }
}