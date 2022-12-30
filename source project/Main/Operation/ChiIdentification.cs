using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main.Operation
{
    class ChiIdentification
    {
        public ChiFile captureChi;
        public ChiFile reversedChi;
        public ChiFile beheadedChi;
        public ChiIdentification(string capture, string reversed, string beheaded)
        //public ChiIdentification(string capture, string reversed)
        {
            captureChi = new ChiFile(capture);
            reversedChi = new ChiFile(reversed);
            beheadedChi = new ChiFile(beheaded);
        }

        public void BaseLevelUnify()
        {
            double maxBase = Math.Max(captureChi.height[0], Math.Max(reversedChi.height[0], beheadedChi.height[0]));
            //double maxBase = Math.Max(captureChi.height[0], reversedChi.height[0]);
            int captureStart = 0;
            int reversedStart = 0;
            int beheadedStart = 0;
            for (int i = 0; i < captureChi.height.Count; i++)
            {
                if (captureChi.height[i] >= maxBase)
                {
                    captureStart = i;
                    break;
                }
            }
            double cutCapture = captureChi.chi[captureStart];
            for (int i = 0; i < captureChi.chi.Count; i++)
            {
                captureChi.chi[i] -= cutCapture;
            }
            for (int i = 0; i < captureStart; i++)
            {
                captureChi.chi.RemoveAt(0);
                captureChi.height.RemoveAt(0);
            }

            for (int i = 0; i < reversedChi.chi.Count; i++)
            {
                if (reversedChi.height[i] >= maxBase)
                {
                    reversedStart = i;
                    break;
                }
            }
            double cutReversed = reversedChi.chi[reversedStart];
            for (int i = 0; i < reversedChi.height.Count; i++)
            {
                reversedChi.chi[i] -= cutReversed;
            }
            for (int i = 0; i < reversedStart; i++)
            {
                reversedChi.chi.RemoveAt(0);
                reversedChi.height.RemoveAt(0);
            }

            for (int i = 0; i < beheadedChi.chi.Count; i++)
            {
                if (beheadedChi.height[i] >= maxBase)
                {
                    beheadedStart = i;
                    break;
                }
            }
            double cutBeheaded = beheadedChi.chi[beheadedStart];
            for (int i = 0; i < beheadedChi.height.Count; i++)
            {
                beheadedChi.chi[i] -= cutBeheaded;
            }
            for (int i = 0; i < beheadedStart; i++)
            {
                beheadedChi.chi.RemoveAt(0);
                beheadedChi.height.RemoveAt(0);
            }
        }
        //bending coefficient
        private double CurveIndex(ChiFile chiFile)
        {
            double lengthTotal = 0;
            for (int i = 0; i < chiFile.chi.Count - 1; i++)
            {
                lengthTotal += Math.Sqrt(Math.Pow(chiFile.chi[i + 1] - chiFile.chi[i], 2.0) + Math.Pow(chiFile.height[i + 1] - chiFile.height[i], 2.0));
            }
            double length = Math.Sqrt(Math.Pow(chiFile.chi[chiFile.chi.Count - 1] - chiFile.chi[0], 2.0) + Math.Pow(chiFile.height[chiFile.height.Count - 1] - chiFile.height[0], 2.0));
            return (lengthTotal / length);
        }
        //find the farest point
        private Tuple<int, double> FarestPoint(ChiFile chiFile, int start, int end)
        {
            double A = chiFile.height[end] - chiFile.height[start];
            double B = chiFile.chi[start] - chiFile.chi[end];
            double C = (chiFile.chi[end] - chiFile.chi[start]) * chiFile.height[start] + (chiFile.height[start] - chiFile.height[end]) * chiFile.chi[start];
            double max_dis = 0;
            int max_dis_index = 0;
            for (int i = start; i < end; i++)
            {
                double distance = Math.Abs(A * chiFile.chi[i] + B * chiFile.height[i] + C) / Math.Sqrt(Math.Pow(A, 2.0) + Math.Pow(B, 2.0));
                if (distance > max_dis)
                {
                    max_dis = distance;
                    max_dis_index = i;
                }
            }
            Tuple<int, double> tup = new Tuple<int, double>(max_dis_index, (A * chiFile.chi[max_dis_index] + B * chiFile.height[max_dis_index] + C) / Math.Sqrt(Math.Pow(A, 2.0) + Math.Pow(B, 2.0)));
            return tup;
        }
        //split the chi-plot to 3 parts
        private IList<int> SplitThreeSegments(ChiFile chiFile)
        {
            var farestP1 = FarestPoint(chiFile, 0, chiFile.chi.Count - 1);
            int farest1_index = farestP1.Item1;
            double farest1 = farestP1.Item2;
            var farestP2 = FarestPoint(chiFile, farest1_index, chiFile.chi.Count - 1);
            int farest2_index = farestP2.Item1;
            double farest2 = farestP2.Item2;
            var farestP3 = FarestPoint(chiFile, 0, farest1_index);
            int farest3_index = farestP3.Item1;
            double farest3 = farestP3.Item2;
            IList<int> result = new List<int>();
            if (Math.Abs(farest2) > Math.Abs(farest3))
            {

                if (farest1_index < farest2_index)
                {
                    result.Add(farest1_index);
                    result.Add(farest2_index);
                    return result;
                }
                else
                {
                    result.Add(farest2_index);
                    result.Add(farest1_index);
                    return result;
                }
            }
            else
            {
                if (farest1_index < farest3_index)
                {
                    result.Add(farest1_index);
                    result.Add(farest3_index);
                    return result;
                }
                else
                {
                    result.Add(farest3_index);
                    result.Add(farest1_index);
                    return result;
                }
            }
        }
        //slope
        private double K(double x0, double x1, double y0, double y1)
        {
            return ((y1 - y0) / (x1 - x0));
        }
        //identification by chi-plot
        public bool Identification()
        {

            double kCapture = K(captureChi.chi[0], captureChi.chi[captureChi.chi.Count - 1], captureChi.height[0], captureChi.height[captureChi.chi.Count - 1]);
            double kReversed = K(reversedChi.chi[0], reversedChi.chi[reversedChi.chi.Count - 1], reversedChi.height[0], reversedChi.height[reversedChi.chi.Count - 1]);
            double kBeheaded = K(beheadedChi.chi[0], beheadedChi.chi[beheadedChi.chi.Count - 1], beheadedChi.height[0], beheadedChi.height[beheadedChi.chi.Count - 1]);
            if (kCapture > kBeheaded && kReversed > kBeheaded)
            {
                double bc = CurveIndex(captureChi);
                double br = CurveIndex(reversedChi);
                if (bc > 1.5 && br > 1.5)
                {
                    IList<int> farestCapture = SplitThreeSegments(captureChi);
                    int farest1IndexCapture = farestCapture[0];
                    int farest2IndexCapture = farestCapture[1];
                    IList<int> farestReversed = SplitThreeSegments(reversedChi);
                    int farest1IndexReversed = farestReversed[0];
                    int farest2IndexReversed = farestReversed[1];

                    double k1Capture = K(captureChi.chi[0], captureChi.chi[farest1IndexCapture], captureChi.height[0], captureChi.height[farest1IndexCapture]);
                    double k2Capture = K(captureChi.chi[farest1IndexCapture], captureChi.chi[farest2IndexCapture], captureChi.height[farest1IndexCapture], captureChi.height[farest2IndexCapture]);
                    double k3Capture = K(captureChi.chi[farest2IndexCapture], captureChi.chi[captureChi.chi.Count - 1], captureChi.height[farest2IndexCapture], captureChi.height[captureChi.chi.Count - 1]);
                    double k1Reserved = K(reversedChi.chi[0], reversedChi.chi[farest1IndexReversed], reversedChi.height[0], reversedChi.height[farest1IndexReversed]);
                    double k2Reserved = K(reversedChi.chi[farest1IndexReversed], reversedChi.chi[farest2IndexReversed], reversedChi.height[farest1IndexReversed], reversedChi.height[farest2IndexReversed]);
                    double k3Reserved = K(reversedChi.chi[farest2IndexReversed], reversedChi.chi[reversedChi.chi.Count - 1], reversedChi.height[farest2IndexReversed], reversedChi.height[reversedChi.chi.Count - 1]);
                    if (((k1Capture > (k3Capture * 2)) || (k2Capture > (k3Capture * 2))) && ((k1Reserved > (k3Reserved * 2)) || (k2Reserved > (k3Reserved * 2))))
                    {
                        return true;
                    }
                }
            }
            //double bc = CurveIndex(captureChi);
            //double br = CurveIndex(reversedChi);
            //if (bc > 1.5 && br > 1.5)
            //{
            //    IList<int> farestCapture = SplitThreeSegments(captureChi);
            //    int farest1IndexCapture = farestCapture[0];
            //    int farest2IndexCapture = farestCapture[1];
            //    IList<int> farestReversed = SplitThreeSegments(reversedChi);
            //    int farest1IndexReversed = farestReversed[0];
            //    int farest2IndexReversed = farestReversed[1];

            //    double k1Capture = K(captureChi.chi[0], captureChi.chi[farest1IndexCapture], captureChi.height[0], captureChi.height[farest1IndexCapture]);
            //    double k2Capture = K(captureChi.chi[farest1IndexCapture], captureChi.chi[farest2IndexCapture], captureChi.height[farest1IndexCapture], captureChi.height[farest2IndexCapture]);
            //    double k3Capture = K(captureChi.chi[farest2IndexCapture], captureChi.chi[captureChi.chi.Count - 1], captureChi.height[farest2IndexCapture], captureChi.height[captureChi.chi.Count - 1]);
            //    double k1Reserved = K(reversedChi.chi[0], reversedChi.chi[farest1IndexReversed], reversedChi.height[0], reversedChi.height[farest1IndexReversed]);
            //    double k2Reserved = K(reversedChi.chi[farest1IndexReversed], reversedChi.chi[farest2IndexReversed], reversedChi.height[farest1IndexReversed], reversedChi.height[farest2IndexReversed]);
            //    double k3Reserved = K(reversedChi.chi[farest2IndexReversed], reversedChi.chi[reversedChi.chi.Count - 1], reversedChi.height[farest2IndexReversed], reversedChi.height[reversedChi.chi.Count - 1]);
            //    if (((k1Capture > (k3Capture * 2)) || (k2Capture > (k3Capture * 2))) && ((k1Reserved > (k3Reserved * 2)) || (k2Reserved > (k3Reserved * 2))))
            //    {
            //        return true;
            //    }
            //}
            return false;
        }
    }
}
