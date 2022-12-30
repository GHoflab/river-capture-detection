using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main.Operation
{
    class Group
    {
        public int main = -1;
        public int tributary1 = -1;   //two tributaries
        public int tributary2 = -1;
        public Point node;
        public int elbowCondition = 0;
        public bool roughCaptured = true;
        public bool chiPlotCaptured = false;
        private void CaptureElbow(double captureElbowThre, IList<Reach> reaches)
        {
            elbowCondition = 0;
            double angle1 = IncludedAngle(reaches[tributary1].flowDirection, reaches[main].flowDirection);
            double angle2 = IncludedAngle(reaches[tributary2].flowDirection, reaches[main].flowDirection);
            if (angle1 > captureElbowThre * Math.PI / 180)
                elbowCondition += 1;
            else if (angle2 > captureElbowThre * Math.PI / 180)
                elbowCondition += 2;
            else
                roughCaptured = roughCaptured && false;
        }
        //judge angle between tributaries(180°)
        private void TributaryAngle(double tributaryAngleThre, IList<Reach> reaches)
        {
            if (IncludedAngle(reaches[tributary1].flowDirection, reaches[tributary2].flowDirection) > tributaryAngleThre * Math.PI / 180)
                roughCaptured = roughCaptured && true;
            else
                roughCaptured = roughCaptured && false;
        }
        //judge tributaries are on a line
        private void OppositeRiver(double oppositeReachThre, IList<Reach> reaches)
        {
            //calculate the azimuths of the connection lines of the two feature points
            double angle1To2 = reaches[tributary1].fPoint.ReachAngle(reaches[tributary2].fPoint);
            double angle2To1 = reaches[tributary2].fPoint.ReachAngle(reaches[tributary1].fPoint);
            //calculate the included angle between the azimuths and tributaries
            if (IncludedAngle(angle1To2, reaches[tributary2].flowDirection) > oppositeReachThre * Math.PI / 180
                && IncludedAngle(angle2To1, reaches[tributary1].flowDirection) > oppositeReachThre * Math.PI / 180)
                roughCaptured = roughCaptured && true;
            else
                roughCaptured = roughCaptured && false;
        }
        //judge the indegree of the reversed river
        private void Indegree(IList<Reach> reaches)
        {
            if (elbowCondition == 1)
            {
                if (reaches[tributary2].InDeg == 0)
                    roughCaptured = roughCaptured && true;
                else
                    roughCaptured = roughCaptured && false;
            }
            else if (elbowCondition == 2)
            {
                if (reaches[tributary1].InDeg == 0)
                    roughCaptured = roughCaptured && true;
                else
                    roughCaptured = roughCaptured && false;
            }
            else if (elbowCondition == 3)
            {
                if (reaches[tributary1].InDeg == 0 || reaches[tributary2].InDeg == 0)
                    roughCaptured = roughCaptured && true;
                else
                    roughCaptured = roughCaptured && false;
            }
            else
                roughCaptured = roughCaptured && false;
        }

        public void Judgement(IList<Reach> reaches, double captureElbowThre, double tributaryAngleThre, double oppositeReachThre, bool windGapJudged)
        {
            CaptureElbow(captureElbowThre, reaches);
            if (roughCaptured)
            {
                TributaryAngle(tributaryAngleThre, reaches);
                if (roughCaptured)
                {
                    OppositeRiver(oppositeReachThre, reaches);
                    if (roughCaptured && windGapJudged)
                    {
                        Indegree(reaches);
                    }
                }
            }
        }
        //included angle between two angles
        private double IncludedAngle(double a, double b)
        {
            return (Math.Abs(a - b) < Math.PI) ? (Math.Abs(a - b)) : (Math.PI * 2 - Math.Abs(a - b));
        }
        //find the closest reach from the reversed reach as beheaded river
        public int FindClosestReach(IList<Reach> reaches, Reach reach)
        {
            double minLength = -1;
            int minIndex = -1;
            int index = reach.ID;
            for (int i = 0; i < reaches.Count; i++)
            {
                if (i != index && reaches[i].upstream1 == -1)
                {
                    double length = reach.points[reach.points.Count - 1].Length(reaches[i].points[reaches[i].points.Count - 1]);
                    if (minIndex == -1)
                    {
                        
                        minLength = length;
                        minIndex = i;
                    }
                    else
                    {
                        if (length < minLength)
                        {
                            minLength = length;
                            minIndex = i;
                        }
                    }
                }
            }
            return minIndex;
        }
        //find the closest reach in a fan of angle from the reversed reach as beheaded river
        public int FindClosestWithinLimit(IList<Reach> reaches, Reach reach)
        {
            double minLength = -1;
            int minIndex = -1;
            int index = reach.ID;
            for (int i = 0; i < reaches.Count; i++)
            {
                if (i != index && reaches[i].upstream1 == -1)
                {
                    //double angle = reach.points[reach.points.Count - 1].ReachAngle(reaches[i].points[reaches[i].points.Count - 1]);
                    //if (IncludedAngle(angle, reach.flowDirection) > 150 * Math.PI / 180)
                    if (IncludedAngle(reach.flowDirection, reaches[i].flowDirection) > 150 * Math.PI / 180)
                    {
                        double length = reach.points[reach.points.Count - 1].Length(reaches[i].points[reaches[i].points.Count - 1]);
                        if (minIndex == -1)
                        {
                            minLength = length;
                            minIndex = i;
                        }
                        else
                        {
                            if (length < minLength)
                            {
                                minLength = length;
                                minIndex = i;
                            }
                        }
                    }
                }
            }
            return minIndex;
        }
    }
}
