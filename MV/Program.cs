using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Json;

namespace LabMV2
{
    class Program
    {
        public static int N = 10;

        static void Main(string[] args)
        {


            //Testing

            double[,] A = {{2, 2, 3,2}, {0, 5, 6,3}, {3, 1, 10,4},{1,2,-1,2}};

            PrintMatrix(A);
            Console.WriteLine("---------------------");
            PrintMatrix(GetInverseMatrix(A));

            double[,] Q = new double[1, 1];
            double[,] R = new double[1, 1];

            GetQR(A, ref Q, ref R);

            Console.WriteLine("Q = \n");

            PrintMatrix(Q);

            Console.WriteLine("R= \n");
            PrintMatrix(R);

            Console.WriteLine("Q*R= \n");
            PrintMatrix(MatrixMultiplication(Q, R));

            Console.WriteLine("A eigen vakues= \n");
            PrintMatrix(GetEigenNumbersQR(A));

            //Console.WriteLine(GetMaxEigenValueCase1(A));

            //Console.WriteLine(1 / GetMaxEigenValueCase1(GetInverseMatrix(A)));
        }


        public static double[,] GetRandomMatrix()
        {
            double[,] A = new double[10, 10];

            Random random = new Random(1010);

            double leftBorder = -Math.Pow(2, 2.5);
            double rightBorder = Math.Pow(2, 2.5);

            double borderLength = rightBorder - leftBorder;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    A[i, j] = (random.NextDouble() * borderLength) + leftBorder;
                }
            }

            return A;
        }

        public static void PrintMatrix(double[,] A)
        {
            for (int i = 0; i < A.GetLength(0); i++)
            {

                for (int j = 0; j < A.GetLength(1); j++)
                {
                    Console.Write($" {A[i, j]}|");
                }
                Console.WriteLine("");
            }
        }


        public static double GetMaxEigenValueCase3(double[,] A)
        {
            // initial approximation
            double[] u = new double[A.GetLength(0)];
            u[0] = 1;

            double eigenValue = 0;

            //precision
            double epsilon = 0.001;

            while (GetMagnitude(VectorSubtraction(MatrixMultiplication(A, u).ToArray(), VecotorWithScalarMultiplication(u, eigenValue))) > epsilon)
            {
                double[] v = MatrixMultiplication(A, u);

                u = (MatrixMultiplication(A, v));

                eigenValue = Math.Sqrt(u.Max());

                u = VectorWithScalarDivison(u, u.Max());
            }

            return eigenValue;
        }

        //TODO: solve issue where some vtime it does no scare
        public static double GetMaxEigenValueCase1(double[,] A)
        {
            // initial approximation
            double[] u = new double[A.GetLength(0)];
            u[0] = 1;
            u[1] = 1;

            double eigenValue = 0.082;

            //precision
            double epsilon = 0.001;

            while (GetMagnitude(VectorSubtraction(MatrixMultiplication(A, u).ToArray(), VecotorWithScalarMultiplication(u, eigenValue))) > epsilon)
            {
                Console.WriteLine(GetMagnitude(VectorSubtraction(MatrixMultiplication(A, u).ToArray(), VecotorWithScalarMultiplication(u, eigenValue))));

                double[] v = MatrixMultiplication(A, u);

                eigenValue = v.Max();

                u = VectorWithScalarDivison(v, eigenValue);
            }

            return eigenValue;
        }


        public static double[,] GetEigenNumbersQR(double[,] A)
        {
            A = (double[,])A.Clone();

            for (int i = 0; i < 1000000; i++)
            {
                double[,] Q = new double[1, 1];
                double[,] R = new double[1, 1];

                GetQR(A, ref Q, ref R);
                A = MatrixMultiplication(R, Q);


            }

            for (int i = 0; i < A.GetLength(0); i++)
            {
                if (i < A.GetLength(0) - 1 && Math.Abs(A[i + 1, i]) > double.Epsilon)
                {
                    double a = (A[i, i] + A[i + 1, i + 1]) / 2;
                    double b =Math.Sqrt(Math.Abs( Math.Pow(a, 2) - A[i + 1, i + 1] * A[i, i] + A[i + 1, i] * A[i, i + 1]));

                    A[i, i] = a;
                    A[i, i + 1] = b;
                    A[i+1, i ] = -b;
                }
            }

            return A;
        }


        public static void PrintMatrix(double[] A)
        {
            for (int i = 0; i < A.GetLength(0); i++)
            {
                //Console.WriteLine("----------------------------------");
                Console.WriteLine($" {A[i]}|");
            }
        }

        public static double[] VecotorWithScalarMultiplication(double[] vector, double scalar)
        {
            vector = (double[])vector.Clone();
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] *= scalar;
            }

            return vector;
        }

        public static double[,] MatrixMultiplication(double[,] A, double[,] B)
        {
            int n = A.GetLength(0);
            int m = A.GetLength(1);
            int k = B.GetLength(1);

            double[,] C = new double[n, k];

            for (int i = 0; i < n; i++)
            {
                for (int z = 0; z < k; z++)
                {
                    double sum = 0;

                    for (int j = 0; j < m; j++)
                    {
                        sum += A[i, j] * B[j, z];
                    }
                    C[i, z] = sum;
                    //TODO: comment to do not change precision
                    //if (Math.Abs(C[i, z]) < 0.0000001)
                    //{
                    //    C[i, z] = 0;
                    //}

                }
            }
            return C;
        }

        public static double[] VectorSubtraction(double[] a, double[] b)
        {
            a = (double[])a.Clone();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] -= b[i];
            }

            return a;
        }

        public static double[] VectorSum(double[] a, double[] b)
        {
            a = (double[])a.Clone();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] += b[i];
            }

            return a;
        }

        public static double[] VectorWithScalarDivison(double[] a, double b)
        {
            a = (double[])a.Clone();
            return VecotorWithScalarMultiplication(a, 1 / b);
        }

        public static double[] MatrixMultiplication(double[,] A, double[] B)
        {
            int n = A.GetLength(0);
            int m = A.GetLength(1);
            int k = 1;

            double[] C = new double[n];

            for (int i = 0; i < n; i++)
            {
                double sum = 0;

                for (int j = 0; j < m; j++)
                {
                    sum += A[i, j] * B[j];
                }
                C[i] = sum;

            }
            return C;
        }

        public static double GetCubicNorm(double[,] A)
        {
            double maxSum = double.MinValue;

            for (int i = 0; i < A.GetLength(0); i++)
            {
                double sum = 0;

                for (int j = 0; j < A.GetLength(0); j++)
                {
                    sum += Math.Abs(A[i, j]);
                }

                maxSum = Math.Max(maxSum, sum);

            }

            return maxSum;
        }


        public static double GetCubicVectorNorm(double[] A)
        {
            double max = double.MinValue;

            for (int i = 0; i < A.GetLength(0); i++)
            {
                max = Math.Max(max, Math.Abs(A[i]));
            }
            return max;
        }


        public static double GetMagnitude(double[,] A, int row, int column)
        {
            double sum = 0;

            int size = A.GetLength(0);

            for (int i = row; i < size; i++)
            {
                sum += (double)Math.Pow(A[i, column], 2);
            }

            return (double)Math.Sqrt(sum);
        }

        public static double GetMagnitude(double[] A)
        {
            double sum = 0;

            int size = A.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                sum += (double)A[i] * A[i];
            }

            return (double)Math.Sqrt(sum);
        }


        public static double[,] GetInverseMatrix(double[,] A)
        {
            A = (double[,])A.Clone();
            int size = A.GetLength(0);
            double[,] B = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                B[i, i] = 1;
            }
            for (int j = 0; j < size - 1; j++)
            {
                double leadValue = A[j, j];

                for (int i = j + 1; i < size; i++)
                {
                    double coef = -A[i, j] * 1 / leadValue;

                    for (int k = 0; k < size; k++)
                    {
                        A[i, k] += A[j, k] * coef;
                        B[i, k] += B[j, k] * coef;
                        //TODO: comment to do not change precision
                        //if (Math.Abs(A[i, k]) < 0.00001)
                        //{
                        //    A[i, k] = 0;
                        //}
                    }


                }
            }

            for (int j = size - 1; j > 0; j--)
            {
                double leadValue = A[j, j];

                for (int i = j - 1; i >= 0; i--)
                {
                    double coef = -A[i, j] * 1 / leadValue;

                    for (int k = 0; k < size; k++)
                    {
                        A[i, k] += A[j, k] * coef;
                        B[i, k] += B[j, k] * coef;
                        ////TODO: comment to do not change precision
                        //if (Math.Abs(A[i, k]) < 0.00001)
                        //{
                        //    A[i, k] = 0;
                        //}
                    }

                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    B[i, j] /= A[i, i];
                }
            }

            return B;
        }


        public static double ScalarMultiplication(double[] A, double[] B)
        {
            double sum = 0;

            for (int i = 0; i < A.GetLength(0); i++)
            {
                sum += A[i] * B[i];
            }

            return sum;
        }


        public static void GetQR(double[,] A, ref double[,] Q, ref double[,] R)
        {
            A = (double[,])A.Clone();

            int size = A.GetLength(0);

            List<double[,]> QList = new List<double[,]>();


            for (int i = 0; i < size - 1; i++)
            {

                double[] w = new double[size - i];

                double[] xs = new double[size - i];

                double[] aVector = GetVectorFromMatrix(A, i, i, size - 1);

                xs[0] = GetMagnitude(aVector);


                for (int j = 0; j < w.GetLength(0); j++)
                {
                    w[j] = aVector[j] - xs[j];
                }

                double magnitudeW = GetMagnitude(w);

                if (magnitudeW == 0)
                {
                    continue;
                }

                for (int j = 0; j < w.GetLength(0); j++)
                {
                    w[j] /= magnitudeW;
                }

                double[,] Qn = GetOnesMatrix(w.GetLength(0));

                double[,] wMatrix = new double[1, w.Length];
                double[,] wMatrixTransposed = new double[w.Length, 1];

                for (int j = 0; j < w.Length; j++)
                {
                    wMatrix[0, j] = w[j];
                    wMatrixTransposed[j, 0] = w[j];
                }

                var multiplicationWW = MatrixMultiplication(wMatrixTransposed, wMatrix);

                Qn = MatrixSubtraction(Qn, MatrixWithScalarMultiplication(multiplicationWW, 2));

                QList.Add(Qn);

                int index = 0;

                for (int j = i; j < size; j++)
                {
                    A[j, i] = xs[index++];
                }

                for (int k = i + 1; k < size; k++)
                {
                    double[] aP = GetVectorFromMatrix(A, i, k, size - 1);
                    double scalarMultiplic = ScalarMultiplication(aP, w);

                    int counterW = 0;
                    for (int j = i; j < size; j++)
                    {
                        A[j, k] = A[j, k] - 2 * (scalarMultiplic) * w[counterW++];
                    }
                }

            }

            double[,] FinalQ = ExpandMatrix(GetTransposedMatrix(QList[0]), A.GetLength(0));

            for (int i = 1; i < QList.Count; i++)
            {
                FinalQ = MatrixMultiplication(FinalQ, GetTransposedMatrix(ExpandMatrix(QList[i], FinalQ.GetLength(0))));
            }
            Q = FinalQ;
            R = A;

        }


        public static double[,] GetOnesMatrix(int size)
        {
            double[,] ones = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                ones[i, i] = 1;
            }

            return ones;
        }


        public static double[,] GetTransposedMatrix(double[,] A)
        {
            A = (double[,])A.Clone();

            int size = A.GetLength(0);

            double[,] Answer = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Answer[i, j] = A[j, i];
                }
            }

            return Answer;
        }

        public static double[] GetSubVector(double[] Vector, int start, int end)
        {
            double[] subVector = new double[(end - start) + 1];

            for (int i = 0; i < subVector.GetLength(0); i++)
            {
                subVector[i] = Vector[start + i];
            }

            return subVector;
        }

        public static double[,] ExpandMatrix(double[,] A, int newSize)
        {
            A = (double[,])A.Clone();

            var ones = GetOnesMatrix(newSize);

            for (int i = newSize - A.GetLength(0); i < newSize; i++)
            {
                for (int j = newSize - A.GetLength(0); j < newSize; j++)
                {
                    ones[i, j] = A[i - (newSize - A.GetLength(0)), j - (newSize - A.GetLength(0))];
                }
            }

            return ones;

        }

        public static double[] GetVectorFromMatrix(double[,] A, int startRow, int startColumn, int endRow)
        {
            double[] vector = new double[(endRow - startRow) + 1];

            for (int i = 0; i < vector.GetLength(0); i++)
            {
                vector[i] = A[startRow + i, startColumn];
            }

            return vector;
        }

        public static double[,] MatrixMax(double[,] A, double[,] B)
        {
            A = (double[,])A.Clone();

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    A[i, j] += B[i, j];
                }
            }

            return A;
        }

        public static double[,] MatrixSubtraction(double[,] A, double[,] B)
        {
            A = (double[,])A.Clone();

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    A[i, j] -= B[i, j];
                }
            }

            return A;
        }


        public static double[,] MatrixWithScalarMultiplication(double[,] A, double B)
        {
            A = (double[,])A.Clone();

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    A[i, j] *= B;
                }
            }

            return A;
        }
    }
}