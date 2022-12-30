using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main.Operation
{
    class Regression
    {
        private double[][] X;
        private double[][] Y;
        public Regression(double[] x, double[] y)
        {
            X = new double[2][];
            Y = new double[1][];
            double[] X_0 = new double[x.Length];
            double[] X_1 = x;
            X[0] = X_0;
            X[1] = X_1;
            for (int i = 0; i < x.Length; i++)
            {
                X[0][i] = 1;
            }
            Y[0] = y;
        }
        public double RSquare()
        {
            MatrixOper pMO = new MatrixOper();
            double[][] XX_T = pMO.dot(X, pMO.Tranverse(X));
            double[][] XX_T_Inverse = new double[2][];
            double inverPara = (XX_T[0][0] * XX_T[1][1] - XX_T[0][1] * XX_T[1][0]);
            for (int i = 0; i < 2; i++)
            {
                XX_T_Inverse[i] = new double[2];
            }
            XX_T_Inverse[0][0] = XX_T[1][1] / inverPara;
            XX_T_Inverse[0][1] = -XX_T[0][1] / inverPara;
            XX_T_Inverse[1][0] = -XX_T[1][0] / inverPara;
            XX_T_Inverse[1][1] = XX_T[0][0] / inverPara;
            double[][] theta = pMO.dot(pMO.dot(XX_T_Inverse, X), pMO.Tranverse(Y));
            double SST = pMO.dot(Y, pMO.Tranverse(Y))[0][0] - 1 / Y[0].Length * pMO.Sum(Y) * pMO.Sum(Y);
            double SSE = pMO.dot(pMO.Minus(Y, pMO.Tranverse(pMO.dot(pMO.Tranverse(X), theta))), pMO.Tranverse(pMO.Minus(Y, pMO.Tranverse(pMO.dot(pMO.Tranverse(X), theta)))))[0][0];
            double r_Square = 1 - SSE / SST;
            return r_Square;
        }
    }
}
