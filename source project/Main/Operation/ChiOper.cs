using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using System.IO;
using Main.Operation;

namespace Main.Operation
{
    class ChiOper
    {
        public string GenerateOneRiverChi(ILayer pLayer, ref IList<Reach> reaches, double exponent, double baseLevel, string savePath)
        {
            try
            {
                IFeatureLayer pFL = pLayer as IFeatureLayer;
                IFeatureClass pFC = pFL.FeatureClass;
                IFeatureSelection pFS = pFL as IFeatureSelection;
                ISelectionSet pSS = pFS.SelectionSet;
                ICursor pCursor;
                pSS.Search(null, true, out pCursor);
                IFeatureCursor pFeatCursor = pCursor as IFeatureCursor;
                IFeature pFeature = pFeatCursor.NextFeature();
                int FID = Convert.ToInt32(pFeature.get_Value(0).ToString());
                ChiDown(FID, ref reaches, exponent);
                if (reaches[FID].upstream1 != -1)
                {
                    //recursively calculate upstream chi values
                    ChiUp(reaches[FID].upstream1, ref reaches, exponent);
                    ChiUp(reaches[FID].upstream2, ref reaches, exponent);
                }

                StreamWriter sw = new StreamWriter(savePath, true, System.Text.Encoding.Default);
                sw.WriteLine("chi,elevation");
                sw.Close();
                double chiBase = 0;
                bool baseFound = false;
                ExportChiDown(FID, reaches, ref baseFound, ref chiBase, baseLevel, savePath);
                if (reaches[FID].upstream1 != -1 && baseFound)
                {
                    ExportChiUp(reaches[FID].upstream1, reaches, chiBase, savePath);
                    ExportChiUp(reaches[FID].upstream2, reaches, chiBase, savePath);
                }
                return "Complete!";
            }
            catch (Exception ex)
            {
                return "Please select a reach first!";
            }
        }
        //calculate downstream chi
        private int ChiDown(int index, ref IList<Reach> reaches, double exponent)
        {
            if (reaches[index].chi.Count == 0)
            {
                //if not the lowest reach
                if (reaches[index].downStream != -1)
                {
                    //calculate from the lowest reach
                    ChiDown(reaches[index].downStream, ref reaches, exponent);
                    //initialize list
                    for (int i = 0; i < reaches[index].distance.Count; i++)
                        reaches[index].chi.Add(0);
                    
                    
                    int temp = 0;
                    //filter out invalid points
                    for (int i = 1; i < reaches[index].distance.Count; i++)
                    {
                        if (reaches[index].acc[i] < reaches[index].acc[temp])
                        {
                            reaches[index].distance[temp] += reaches[index].distance[i];
                            reaches[index].distance[i] = 0;
                            reaches[index].acc[i] = 1;
                        }
                        else
                        {
                            temp = i;
                        }
                    }
                    if (reaches[index].acc[temp] > reaches[reaches[index].downStream].acc[0])
                    {
                        int downNormal = -1;
                        for (int i = 0; i < reaches[reaches[index].downStream].acc.Count; i++)
                        {
                            if (reaches[index].acc[temp] <= reaches[reaches[index].downStream].acc[i])
                            {
                                downNormal = i;
                                break;
                            }
                        }
                        if (downNormal != -1)
                        {
                            for (int i = 0; i < downNormal; i++)
                            {
                                reaches[index].distance[temp] += reaches[reaches[index].downStream].distance[i];
                                reaches[reaches[index].downStream].distance[i] = 0;
                                reaches[reaches[index].downStream].acc[i] = 1;
                                reaches[reaches[index].downStream].chi[i] = reaches[reaches[index].downStream].chi[downNormal];
                            }
                        }
                    }
                    //last point equal to first point of downstream, same chi
                    reaches[index].chi[reaches[index].chi.Count - 1] = reaches[reaches[index].downStream].chi[0];
                    //calculate chi from low to high elevations
                    for (int i = reaches[index].distance.Count - 2; i >= 0; i--)
                    {
                        //Point matrixP = MatrixXY(streams[index].profilePoints[i]);
                        //int a = (int)Math.Round(matrixP.x);
                        //int b = (int)Math.Round(matrixP.y);
                        reaches[index].chi[i] = reaches[index].chi[i + 1] + reaches[index].distance[i] * Math.Pow((1 / reaches[index].acc[i]), exponent);
                    }
                }
                //the lowest reach
                else
                {
                    //0 for the lowest point
                    for (int i = 0; i < reaches[index].distance.Count; i++)
                        reaches[index].chi.Add(0);
                    reaches[index].chi[reaches[index].distance.Count - 1] = 0;
                    int temp = 0;
                    //int count = streams[index].distance.Count;
                    for (int i = 1; i < reaches[index].distance.Count; i++)
                    {
                        if (reaches[index].acc[i] < reaches[index].acc[temp])
                        {
                            reaches[index].distance[temp] += reaches[index].distance[i];
                            reaches[index].distance[i] = 0;
                            reaches[index].acc[i] = 1;
                            //count -= 1;
                        }
                        else
                        {
                            temp = i;
                        }
                    }
                    for (int i = reaches[index].distance.Count - 2; i >= 0; i--)
                    {
                        //Point matrixP = acc.MatrixXY(streams[index].profilePoints[i]);
                        //int a = (int)Math.Round(matrixP.GetX());
                        //int b = (int)Math.Round(matrixP.GetY());
                        reaches[index].chi[i] = reaches[index].chi[i + 1] + reaches[index].distance[i] * Math.Pow((1 / reaches[index].acc[i]), exponent);
                    }
                }
                return 1;
            }
            else
            {
                reaches[index].chi.Clear();
                ChiDown(index, ref reaches, exponent);
                return 0;
            }

        }
        //calculate upstream chi
        private int ChiUp(int index, ref IList<Reach> reaches, double exponent)
        {
            if (reaches[index].chi.Count == 0)
            {
                for (int i = 0; i < reaches[index].distance.Count; i++)
                    reaches[index].chi.Add(0);
                reaches[index].chi[reaches[index].chi.Count - 1] = reaches[reaches[index].downStream].chi[0];
                int temp = 0;
                for (int i = 1; i < reaches[index].distance.Count; i++)
                {
                    if (reaches[index].acc[i] < reaches[index].acc[temp])
                    {
                        reaches[index].distance[temp] += reaches[index].distance[i];
                        reaches[index].distance[i] = 0;
                        reaches[index].acc[i] = 1;
                    }
                    else
                    {
                        temp = i;
                    }
                }
                for (int i = reaches[index].distance.Count - 2; i >= 0; i--)
                {
                    reaches[index].chi[i] = reaches[index].chi[i + 1] + reaches[index].distance[i] * Math.Pow((1 / reaches[index].acc[i]), exponent);
                }
                if (reaches[index].upstream1 != -1)
                {
                    ChiUp(reaches[index].upstream1, ref reaches, exponent);
                    //ChiUp(reaches[index].upstream2, ref reaches, exponent);
                }
                return 1;
            }
            else
            {
                reaches[index].chi.Clear();
                ChiUp(index, ref reaches, exponent);
                return 0;
            }
        }
        //export chi and elevation downstream
        private void ExportChiDown(int index, IList<Reach> reaches, ref bool baseFound, ref double chiBase, double baseLevel, string filePath)
        {
            if (reaches[index].downStream != -1)
            {
                ExportChiDown(reaches[index].downStream, reaches, ref baseFound, ref chiBase, baseLevel, filePath);
            }
            for (int i = reaches[index].distance.Count - 1; i > 0; i--)
            {
                if (reaches[index].elevation[i] >= baseLevel && baseFound == false)
                {
                    chiBase = reaches[index].chi[i];
                    baseFound = true;
                }
                if (baseFound)
                {
                    StreamWriter sw = new StreamWriter(filePath, true, System.Text.Encoding.Default);
                    string str = (reaches[index].chi[i] - chiBase).ToString() + "," + reaches[index].elevation[i].ToString();
                    sw.WriteLine(str);
                    sw.Close();
                }
            }
        }
        //export chi and elevation upstream
        private double ExportChiUp(int index, IList<Reach> reaches, double chiBase, string filePath)
        {
            for (int i = reaches[index].distance.Count - 1; i > 0; i--)
            {

                StreamWriter sw = new StreamWriter(filePath, true, System.Text.Encoding.Default);
                string str = (reaches[index].chi[i] - chiBase).ToString() + "," + reaches[index].elevation[i].ToString();
                sw.WriteLine(str);
                sw.Close();

            }
            if (reaches[index].upstream1 != -1)
            {
                ExportChiUp(reaches[index].upstream1, reaches, chiBase, filePath);
                //ExportChiUp(reaches[index].upstream2, reaches, chiBase, filePath);
            }
            return 0;
        }
        //generate chi-plot of one reach
        public void GenerateOneReachInGroup(ref IList<Reach> reaches, Group group, int FID, double exponent, double baseLevel, string savePath)
        {
            ChiDown(FID, ref reaches, exponent);
            if (reaches[FID].upstream1 != -1)
            {
                //recursively calculate upstream chi values
                ChiUp(reaches[FID].upstream1, ref reaches, exponent);
                //ChiUp(reaches[FID].upstream2, ref reaches, exponent);
            }
            StreamWriter sw = new StreamWriter(savePath, true, System.Text.Encoding.Default);
            sw.WriteLine("chi,elevation");
            sw.Close();
            double chiBase = 0;
            bool baseFound = false;
            ExportChiDown(FID, reaches, ref baseFound, ref chiBase, baseLevel, savePath);
            if (reaches[FID].upstream1 != -1 && baseFound)
            {
                ExportChiUp(reaches[FID].upstream1, reaches, chiBase, savePath);
                //ExportChiUp(reaches[FID].upstream2, reaches, chiBase, savePath);
            }
        }
        //generate chi-plot of all possible groups passed rough identification
        public void GenerateAllGroups(ref IList<Reach> reaches, Group group, double exponent, double baseLevel, bool beheadedChoice, string saveFolder)
        {
            //beheadedChoice = false;
            //string groupFolder = saveFolder + "//" + group.main.ToString() + "_" + reaches[group.tributary1].ID.ToString();
            //Directory.CreateDirectory(groupFolder);
            //GenerateOneReachInGroup(ref reaches, group, group.tributary1, exponent, baseLevel, groupFolder + "//capture.csv");
            //GenerateOneReachInGroup(ref reaches, group, group.tributary2, exponent, baseLevel, groupFolder + "//reversed.csv");
            int beheadedIndex;
            if (group.elbowCondition == 1)
            {
                if (beheadedChoice)
                {
                    beheadedIndex = group.FindClosestReach(reaches, reaches[group.tributary2]);
                }
                else
                {
                    beheadedIndex = group.FindClosestWithinLimit(reaches, reaches[group.tributary2]);
                }
                string groupFolder = saveFolder + "//" + group.main.ToString() + "_" + reaches[group.tributary1].ID.ToString();
                Directory.CreateDirectory(groupFolder);

                GenerateOneReachInGroup(ref reaches, group, group.tributary1, exponent, baseLevel, groupFolder + "//capture.csv");
                GenerateOneReachInGroup(ref reaches, group, group.tributary2, exponent, baseLevel, groupFolder + "//reversed.csv");
                GenerateOneReachInGroup(ref reaches, group, beheadedIndex, exponent, baseLevel, groupFolder + "//beheaded.csv");
            }

            else if (group.elbowCondition == 2)
            {
                if (beheadedChoice)
                {
                    beheadedIndex = group.FindClosestReach(reaches, reaches[group.tributary1]);
                }
                else
                {
                    beheadedIndex = group.FindClosestWithinLimit(reaches, reaches[group.tributary1]);
                }
                string groupFolder = saveFolder + "//" + reaches[group.main].ID.ToString() + "_" + group.tributary2.ToString();
                Directory.CreateDirectory(groupFolder);

                GenerateOneReachInGroup(ref reaches, group, group.tributary2, exponent, baseLevel, groupFolder + "//capture.csv");
                GenerateOneReachInGroup(ref reaches, group, group.tributary1, exponent, baseLevel, groupFolder + "//reversed.csv");
                GenerateOneReachInGroup(ref reaches, group, beheadedIndex, exponent, baseLevel, groupFolder + "//beheaded.csv");
            }

            else if (group.elbowCondition == 3)
            {
                if (beheadedChoice)
                {
                    beheadedIndex = group.FindClosestReach(reaches, reaches[group.tributary2]);
                }
                else
                {
                    beheadedIndex = group.FindClosestWithinLimit(reaches, reaches[group.tributary2]);
                }
                string groupFolder = saveFolder + "//" + group.main.ToString() + "_" + group.tributary1.ToString();
                Directory.CreateDirectory(groupFolder);

                GenerateOneReachInGroup(ref reaches, group, group.tributary1, exponent, baseLevel, groupFolder + "//capture.csv");
                GenerateOneReachInGroup(ref reaches, group, group.tributary2, exponent, baseLevel, groupFolder + "//reversed.csv");
                GenerateOneReachInGroup(ref reaches, group, beheadedIndex, exponent, baseLevel, groupFolder + "//beheaded.csv");

                if (beheadedChoice)
                {
                    beheadedIndex = group.FindClosestReach(reaches, reaches[group.tributary1]);
                }
                else
                {
                    beheadedIndex = group.FindClosestWithinLimit(reaches, reaches[group.tributary1]);
                }

                beheadedIndex = group.FindClosestReach(reaches, reaches[group.tributary2]);
                groupFolder = saveFolder + "//" + reaches[group.main].ID.ToString() + "_" + reaches[group.tributary2].ID.ToString();
                Directory.CreateDirectory(groupFolder);

                GenerateOneReachInGroup(ref reaches, group, group.tributary2, exponent, baseLevel, groupFolder + "//capture.csv");
                GenerateOneReachInGroup(ref reaches, group, group.tributary1, exponent, baseLevel, groupFolder + "//reversed.csv");
                GenerateOneReachInGroup(ref reaches, group, beheadedIndex, exponent, baseLevel, groupFolder + "//beheaded.csv");
            }
        }
    }
}
