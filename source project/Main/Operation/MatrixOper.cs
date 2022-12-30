using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main.Operation
{
    class MatrixOper
    {
        public double[][] Tranverse(double[][] A)
        {
            double[][] A_T = new double[A[0].Length][];
            for (int i = 0; i < A[0].Length; i++)
            {
                A_T[i] = new double[A.Length];
                for (int j = 0; j < A.Length; j++)
                {
                    A_T[i][j] = A[j][i]; 
                }
            }
            return A_T;
        }

        public double[][] dot(double[][] A, double[][] B)
        {
            if (A[0].Length == B.Length)
            {
                double[][] result_true = new double[A.Length][];
                for (int i = 0; i < A.Length; i++)
                {
                    result_true[i] = new double[B[0].Length];
                    for (int j = 0; j < B[0].Length; j++)
                    {
                        result_true[i][j] = 0;
                        for (int k = 0; k < A[0].Length; k++)
                        {
                            result_true[i][j] += A[i][k] * B[k][j];
                        }
                    }
                }
                return result_true;
            }
            else
            {
                double[][] result_err = new double[0][];
                result_err[0] = new double[0];
                result_err[0][0] = 0;
                return result_err;
            }
        }
        public double Sum(double[][] A)
        {
            double sum = 0;
            for (int i = 0; i < A.Length; i++)
            {
                for (int j = 0; j < A[0].Length; j++)
                {
                    sum += A[i][j];
                }
            }
            return sum;
        }

        public double[][] Minus(double[][] A, double[][] B)
        {
            if (A.Length == B.Length && A[0].Length == B[0].Length)
            {
                double[][] result_true = new double[A.Length][];
                for (int i = 0; i < A.Length; i++)
                {
                    result_true[i] = new double[A[0].Length];
                    for (int j = 0; j < A[0].Length; j++)
                    {
                        result_true[i][j] = A[i][j] - B[i][j];
                    }
                }
                return result_true;
            }
            else
            {
                double[][] result_err = new double[1][];
                result_err[0] = new double[0];
                result_err[0][0] = 0;
                return result_err;
            }
        }
    }
}
