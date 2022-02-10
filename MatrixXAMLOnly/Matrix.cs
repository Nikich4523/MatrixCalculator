using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Matrix
{
    public class MyMatrix : Object
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

        public static MyMatrix RowReplaceByVector(MyMatrix matrix, int[] vector)
        {
            int[] nowVector = new int[vector.Length];
            for (int i = 0; i < nowVector.Length; i++)
            {
                nowVector[i] = i;
            }

            MyMatrix result = null; 
            for (int i = 0; i < vector.Length; i++)
            {
                if (nowVector[i] != vector[i])
                {
                    int index = 0;
                    for (int j = 0; j < vector.Length; j++)
                    {
                        if (nowVector[j] == vector[i])
                        {
                            index = j;
                        }
                    }

                    result = MyMatrix.RowReplace(matrix, i, index);

                    int boof = nowVector[i];
                    nowVector[i] = nowVector[index];
                    nowVector[index] = boof;
                }
            }

            return result;
        }

        public static MyMatrix Inverse(MyMatrix matrix)
        {
            // Проверяем, существует ли обратная матрица?
            if (!matrix.IsMayInverse())
            {
                throw new Exception("Матрица не обратима");
            }

            // Транспонируем исходную матрицу
            MyMatrix transposedMatrix = Transpose(matrix);

            // Вычисляем элементы союзной матрицы как алгебраические дополнения транспонированной матрицы
            double[,] friendArray = new double[transposedMatrix.Rows, transposedMatrix.Columns];
            for (int i = 0; i < transposedMatrix.Rows; i++)
            {
                for (int j = 0; j < transposedMatrix.Columns; j++)
                {
                    double minor = transposedMatrix.DeleteRowAndColumn(i, j).GetDeterminante();
                    double cofactor = minor * Math.Pow(-1, i + 1 + j + 1);
                    friendArray[i, j] = cofactor;
                }
            }
            MyMatrix friendMatrix = new MyMatrix(friendArray);

            // Применить формулу: умножить число, обратное определителю матрицы A, на союзную матрицу
            MyMatrix inverseMatrix = 1 / matrix.GetDeterminante() * friendMatrix;

            return inverseMatrix;
        }

        public bool IsMayInverse()
        {
            if (Rows != Columns)
            {
                return false;
            }

            if (GetDeterminante() == 0)
            {
                return false;
            }

            return true;
        }

        public double GetDeterminante()
        {
            double determinante = 0;

            if (Rows == 3)
            {
                determinante = data[0, 0] * data[1, 1] * data[2, 2] -
                               data[0, 0] * data[1, 2] * data[2, 1] -
                               data[0, 1] * data[1, 0] * data[2, 2] +
                               data[0, 1] * data[1, 2] * data[2, 0] +
                               data[0, 2] * data[1, 0] * data[2, 1] -
                               data[0, 2] * data[1, 1] * data[2, 0];
            }
            else
            {
                for (int j = 0; j < Columns; j++)
                {
                    double minor = this.DeleteRowAndColumn(0, j).GetDeterminante();
                    double cofactor = minor * Math.Pow(-1, 1 + j + 1);
                    determinante += cofactor * data[0, j];
                }
            }

            return determinante;
        }

        public MyMatrix DeleteRowAndColumn(int rowIndex, int columnIndex)
        {
            double[,] array = data;
            double[,] newArray = new double[Rows - 1, Columns - 1];

            for (int i = 0, k = 0; i < Rows; i++, k++)
            {
                if (i == rowIndex)
                {
                    k--;
                    continue;
                }


                for (int j = 0, l = 0; j < Columns; j++, l++)
                {
                    if (j == columnIndex)
                    {
                        l--;
                        continue;
                    }

                    newArray[k, l] = array[i, j];
                }
            }

            return new MyMatrix(newArray);
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
