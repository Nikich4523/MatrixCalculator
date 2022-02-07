using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Matrix
{
    class MyMatrix : Object
    {
        public double[,] data; // 

        public int Rows
        {
            get { return data.GetLength(0); }
        }

        public int Columns
        {
            get { return data.GetLength(1); }
        }

        public MyMatrix(double[,] data)
        {
            this.data = data;
        }

        public static MyMatrix operator +(MyMatrix matrix1, MyMatrix matrix2)
        {
            if (matrix1.Rows == matrix2.Rows && matrix1.Columns == matrix2.Columns)
            {
                double[,] array = new double[matrix1.Rows, matrix1.Columns];

                for (int i = 0; i < matrix1.Rows; i++)
                {
                    for (int j = 0; j < matrix1.Columns; j++)
                    {
                        array[i, j] = matrix1.data[i, j] + matrix2.data[i, j];
                    }
                }

                return new MyMatrix(array);
            }
            else
            {
                throw new Exception("Размер матриц должен совпадать");
            }
        }

        public static MyMatrix operator -(MyMatrix matrix1, MyMatrix matrix2)
        {
            if (matrix1.Rows == matrix2.Rows && matrix1.Columns == matrix2.Columns)
            {
                double[,] array = new double[matrix1.Rows, matrix1.Columns];

                for (int i = 0; i < matrix1.Rows; i++)
                {
                    for (int j = 0; j < matrix1.Columns; j++)
                    {
                        array[i, j] = matrix1.data[i, j] - matrix2.data[i, j];
                    }
                }

                return new MyMatrix(array);
            }
            else
            {
                throw new Exception("Размер матриц должен совпадать");
            }
        }

        public static MyMatrix operator *(MyMatrix matrixA, MyMatrix matrixB)
        {
            if (matrixA.Rows != matrixB.Columns)
            {
                throw new Exception("Умножение матриц A и B возможно только в том случае, когда число столбцов матрицы A совпадает с числом строк в матрице B");
            }


            int K = matrixA.Columns;
            int M = matrixA.Rows;
            int N = matrixB.Columns;
            double[,] array = new double[M, N];

            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    double num = 0;
                    for (int k = 0; k < K; k++)
                    {
                        num += matrixA.data[i, k] * matrixB.data[k, j];
                    }

                    array[i, j] = num;
                }
            }

            return new MyMatrix(array);
        }

        public static MyMatrix operator *(MyMatrix matrix1, double num)
        {
            double[,] array = new double[matrix1.Rows, matrix1.Columns];

            for (int i = 0; i < matrix1.Rows; i++)
            {
                for (int j = 0; j < matrix1.Columns; j++)
                {
                    array[i, j] = matrix1.data[i, j] * num;
                }
            }

            return new MyMatrix(array);
        }

        public static MyMatrix operator *(double num, MyMatrix matrix1)
        {
            return matrix1 * num;
        }

        public static MyMatrix Transpose(MyMatrix matrix)
        {
            double[,] array = new double[matrix.Columns, matrix.Rows];

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    array[j, i] = matrix.data[i, j];
                }
            }

            return new MyMatrix(array);
        }

        public static MyMatrix RowReplace(MyMatrix matrix, int frstIndex, int scndIndex)
        {
            double[,] array = matrix.data;
            double[] boof = new double[matrix.Columns];

            for (int i = 0; i < matrix.Columns; i++)
            {
                boof[i] = matrix.data[frstIndex, i];
            }

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    if (i != frstIndex)
                    {
                        array[i, j] = matrix.data[i, j];
                    }
                    else
                    {
                        array[i, j] = matrix.data[scndIndex, j];
                    }
                }
            }

            for (int i = 0; i < matrix.Columns; i++)
            {
                array[scndIndex, i] = boof[i];
            }

            return new MyMatrix(array);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("", Rows * Columns * 2);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (j + 1 != Columns)
                    {
                        sb.Append($"{data[i, j]}, ");
                    }
                    else
                    {
                        sb.Append($"{data[i, j]}");
                    }
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}
