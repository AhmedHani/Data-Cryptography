using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternRecognition
{
    public static class MatrixOperations
    {
        public static double[,] Transpose(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            double[,] transposedMatrix = new double[columns, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    transposedMatrix[j, i] = matrix[i, j];
                }
            }

            return transposedMatrix;
        }
        public static double[,] MatrixMultiplication(double[,] firstMatrix, double[,] secondMatrix)
        {
            int rows = firstMatrix.GetLength(0);
            int multiplyElementsNumber = firstMatrix.GetLength(1);
            int columns = secondMatrix.GetLength(1);

            double[,] multipliedMatrix = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    for (int k = 0; k < multiplyElementsNumber; k++)
                    {
                        multipliedMatrix[i, j] += firstMatrix[i, k] * secondMatrix[k, j];
                    }
                }
            }

            return multipliedMatrix;
        }
        public static double[,] ScalerMultiplication(double[,] matrix, double constant)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            double[,] scaledMatrix = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    scaledMatrix[i, j] = matrix[i, j] * constant;
                }

            }

            return scaledMatrix;
        }
        public static double Determinant(double[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                return 0;

            return determinantHelper(matrix);
        }
        public static double[,] SubMatrix(double[,] matrix, int rowsNumber, int startRow, int columnsNumber, int startCol)
        {
            double[,] subMatrix = new double[rowsNumber, columnsNumber];

            for (int i = startRow; i < startRow + rowsNumber; i++)
            {
                for (int j = startCol; j < startCol + columnsNumber; j++)
                {
                    subMatrix[i - startRow, j - startCol] = matrix[i, j];
                }
            }
            return subMatrix;

        }

        public static double[,] SubMatrixDeterminant(double[,] matrix, int rowi, int colj)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            double[,] subMatrix = new double[rows - 1, columns - 1];

            bool rowSkip = false;
            for (int r = 0; r < rows; r++)
            {
                if (r == rowi)
                {
                    rowSkip = true;
                    continue;
                }

                bool columnSkip = false;
                for (int c = 0; c < columns; c++)
                {
                    if (c == colj)
                    {
                        columnSkip = true;
                        continue;
                    }
                    if (!rowSkip)
                    {
                        if (!columnSkip)
                            subMatrix[r, c] = matrix[r, c];
                        else
                            subMatrix[r, c - 1] = matrix[r, c];
                    }
                    else
                    {
                        if (!columnSkip)
                            subMatrix[r - 1, c] = matrix[r, c];
                        else
                            subMatrix[r - 1, c - 1] = matrix[r, c];
                    }
                }
            }
            return subMatrix;
        }
        private static double determinantHelper(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            if (rows == 1 && columns == 1)
                return 1.0;
            if (rows == columns && rows == 2)
            {
                return (matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]);
            }

            double res = 0.0;
            for (int j = 0, neg = 1; j < columns; j++, neg *= -1)
            {
                res += neg * matrix[0, j] * determinantHelper(SubMatrixDeterminant(matrix, 0, j));
            }
            return res;
        }
        public static double[,] Minors(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            double[,] minorsMatrix = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    minorsMatrix[i, j] = Determinant(SubMatrixDeterminant(matrix, i, j));
                }
            }
            return minorsMatrix;
        }
        public static double[,] Cofactors(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            double[,] cofactorMatrix = new double[rows, columns];

            int negative = 1;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    cofactorMatrix[i, j] = negative * matrix[i, j];
                    negative *= -1;
                }
            }

            return cofactorMatrix;
        }

        public static double[,] Inverse(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            if (rows != columns)
                throw new Exception("It's not square matrix");

            double[,] minorsMatrix = Minors(matrix);
            double[,] cofactorsMatrix = Cofactors(minorsMatrix);
            double[,] adjugateMatrix = Transpose(cofactorsMatrix);

            double determinant = 0.0;
            for (int j = 0, neg = 1; j < columns; j++, neg *= -1)
            {
                determinant += neg * minorsMatrix[0, j] * matrix[0, j];
            }

            if (determinant == 0)
                throw new Exception("This matrix is not invertable!");

            double[,] inversedMatrix = ScalerMultiplication(adjugateMatrix, 1 / determinant);

            return inversedMatrix;
        }
    }
}