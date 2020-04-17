using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.CompilerServices;

namespace LabMv
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamWriter srStreamWriter=new StreamWriter("MaxMatrix.txt");

            float[,] A=new float[1,1];
            float[] Y=new float[1];
            float[] B=new float[1];

            float[,] MaxConditionMatrix;
            float[,] MinConditionMatrix=null;
            float maxConditionalNumber = float.MinValue;
            float minConditionalNumber = float.MaxValue;

            List<float> conditionalNumbers=new List<float>();

            List<float> inverseMatrixTime=new List<float>();

            List<float> GauseNormDiff=new List<float>();

            List<float> GauseTime = new List<float>();

            List<float> LUPDecomTime = new List<float>();

            List<float> LUPDecomNormDiff = new List<float>();

            List<float> LUPDecomSolveTime = new List<float>();

            List<float> SquareRootNormDiff = new List<float>();

            List<float> SquareRootSolveTime = new List<float>();

            List<float> ReflectionSolveTime = new List<float>();

            MaxConditionMatrix = null;
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"Doing iteration: {i}");
                GetInitialCondition(ref A, ref Y, ref B, 256);

                float conditionalNumber = GetConditionNumber(A);

                if (maxConditionalNumber < conditionalNumber)
                {
                    maxConditionalNumber = conditionalNumber;
                    MaxConditionMatrix = A;
                }
                if (minConditionalNumber > conditionalNumber)
                {
                    MinConditionMatrix = A;
                    minConditionalNumber = conditionalNumber;
                }
                conditionalNumbers.Add(conditionalNumber);


                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                GetInverseMatrix(A);
                stopwatch.Stop();
                inverseMatrixTime.Add(stopwatch.ElapsedMilliseconds);


                Stopwatch stopwatch1 = new Stopwatch();
                stopwatch1.Start();
                float[] X = SolveMatrix(A, B);
                stopwatch1.Stop();
                GauseTime.Add(stopwatch1.ElapsedMilliseconds);

                float[] diff = new float[X.GetLength(0)];

                for (int j = 0; j < X.GetLength(0); j++)
                {
                    diff[j] = X[j] - Y[j];
                }

                GauseNormDiff.Add(GetCubicVectorNorm(diff));


                float[,] L = new float[1, 1];
                float[,] U = new float[1, 1];
                int[] P = new int[1];

                Stopwatch stopwatch2 = new Stopwatch();
                stopwatch2.Start();
                GetLUP(ref L, ref U, ref P, A);
                stopwatch2.Stop();
                LUPDecomTime.Add(stopwatch2.ElapsedMilliseconds);

                float[] diff1 = new float[X.GetLength(0)];

                Stopwatch stopwatch3 = new Stopwatch();
                stopwatch3.Start();
                float[] X1 = SolveUpperTriangularMatrix(U, SolveLowerTriangularMatrix(L, GetSwapedMatrix(B, P)));
                stopwatch3.Stop();

                LUPDecomSolveTime.Add(stopwatch3.ElapsedMilliseconds);

                for (int j = 0; j < X1.GetLength(0); j++)
                {
                    diff1[j] = X1[j] - Y[j];
                }

                LUPDecomNormDiff.Add(GetCubicVectorNorm(diff1));



                Stopwatch stopwatch4 = new Stopwatch();
                stopwatch4.Start();
                X = SolveWithSquareRoot(A, B);
                stopwatch4.Stop();
                if (X == null)
                {
                    Console.WriteLine("Null X found");
                }
                if (X != null)
                {
                    SquareRootSolveTime.Add(stopwatch4.ElapsedMilliseconds);

                    float[] diff3 = new float[X.GetLength(0)];

                    for (int j = 0; j < X.GetLength(0); j++)
                    {
                        diff3[j] = X[j] - Y[j];
                    }

                    SquareRootNormDiff.Add(GetCubicVectorNorm(diff3));
                }


                Stopwatch stopwatch5 = new Stopwatch();
                stopwatch5.Start();
                X = SolveWithReflectionMethod(A, B);
                stopwatch5.Stop();
                ReflectionSolveTime.Add(stopwatch5.ElapsedMilliseconds);

            }

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    srStreamWriter.Write($" {MaxConditionMatrix[i,j]} |");
                }
                srStreamWriter.WriteLine("");
            }
            srStreamWriter.Flush();
            srStreamWriter.Close();

            Console.WriteLine($"Min conditional number: {conditionalNumbers.Min()}\n " +
                              $"Max conditional number: {conditionalNumbers.Max()} \n " +
                              $"Average: {conditionalNumbers.Average()}");

            Console.WriteLine($"Min inverse matrix time: {inverseMatrixTime.Min()}\n " +
                              $"Max inverse matrix time: {inverseMatrixTime.Max()} \n " +
                              $"Average: {inverseMatrixTime.Average()}");

            Console.WriteLine($"Min gause norm diff: {GauseNormDiff.Min()}\n " +
                              $"Max gause norm diff: {GauseNormDiff.Max()} \n " +
                              $"Average: {GauseNormDiff.Average()}");

            Console.WriteLine($"Min gause  time: {GauseTime.Min()}\n " +
                              $"Max gause time: {GauseTime.Max()} \n " +
                              $"Average: {GauseTime.Average()}");

            Console.WriteLine($"Min LUP decomposition  time: {LUPDecomTime.Min()}\n " +
                              $"Max LUP decomposition time: {LUPDecomTime.Max()} \n " +
                              $"Average: {LUPDecomTime.Average()}");

            Console.WriteLine($"Min LUP solve  time: {LUPDecomSolveTime.Min()}\n " +
                              $"Max LUP solve time: {LUPDecomSolveTime.Max()} \n " +
                              $"Average: {LUPDecomSolveTime.Average()}");

            Console.WriteLine($"Min LUP diff norm: {LUPDecomNormDiff.Min()}\n " +
                              $"Max LUP diff norm: {LUPDecomNormDiff.Max()} \n " +
                              $"Average: {LUPDecomNormDiff.Average()}");


            if (SquareRootNormDiff.Count != 0)
            {
                Console.WriteLine($"Min square root diff norm: {SquareRootNormDiff.Min()}\n " +
                                  $"Max square root diff norm: {SquareRootNormDiff.Max()} \n " +
                                  $"Average: {SquareRootNormDiff.Average()}");

                Console.WriteLine($"Min square root solve time: {SquareRootSolveTime.Min()}\n " +
                                  $"Max square root solve time: {SquareRootSolveTime.Max()} \n " +
                                  $"Average: {SquareRootSolveTime.Average()}");
            }
            else
            {
                Console.WriteLine("No positive matrixes found");
            }

            Console.WriteLine($"Reflection solve time: {ReflectionSolveTime.Min()}\n " +
                              $"Reflection solve time: {ReflectionSolveTime.Max()} \n " +
                              $"Average: {ReflectionSolveTime.Average()}");


            Console.WriteLine("Testing matrix with max conditional number\n");

            for (int i = 0; i < B.GetLength(0); i++)
            {
                B[i] = (float)GetRandomNumber(-8, 8);
            }

            float[] Xresult = SolveMatrix(MaxConditionMatrix, B);

            for (int l = 0; l < 5; l++)
            {


                for (int i = 0; i < B.GetLength(0); i++)
                {
                    B[i] += 0.1f;
                }

                float[] XR = SolveMatrix(MaxConditionMatrix, B);

                float[] diffR = new float[XR.GetLength(0)];

                for (int j = 0; j < XR.GetLength(0); j++)
                {
                    diffR[j] = XR[j] - Xresult[j];
                }
                Console.WriteLine($"Result diff = {GetCubicVectorNorm(diffR)}");
            }

            Console.WriteLine("Testing matrix with min conditional number\n");

            for (int i = 0; i < B.GetLength(0); i++)
            {
                B[i] = (float)GetRandomNumber(-8, 8);
            }

            Xresult = SolveMatrix(MinConditionMatrix, B);

            for (int l = 0; l < 5; l++)
            {


                for (int i = 0; i < B.GetLength(0); i++)
                {
                    B[i] += 0.1f;
                }

                float[] XR = SolveMatrix(MinConditionMatrix, B);

                float[] diffR = new float[XR.GetLength(0)];

                for (int j = 0; j < XR.GetLength(0); j++)
                {
                    diffR[j] = XR[j] - Xresult[j];
                }
                Console.WriteLine($"Result diff = {GetCubicVectorNorm(diffR)}");
            }


            ///Task for matrix 4X4
#region Matrix4X4

            float[,] MyMatrix = new float[,] {{1, 0,-1,1}, {0, 2, 4,1}, {-1, 4, 14,0},{1,1,0,2}};


            float[] MyB = new float[] {2, 12, 19,2};

            Console.WriteLine("Matrix = ");
            PrintMatrix(MyMatrix);
            Console.WriteLine("");

            Console.WriteLine("Conditional number: " + GetConditionNumber(MyMatrix));
           
            Console.WriteLine("Inverse matrix = ");
            PrintMatrix(GetInverseMatrix(MyMatrix));
            Console.WriteLine("");

            Console.WriteLine("solved by calssic Gaus method:\n X = ");
            PrintMatrix( SolveMatrix(MyMatrix, MyB));
            Console.WriteLine("");

            float[,] Lm = new float[1, 1];
            float[,] Um = new float[1, 1];
            int[] Pm = new int[1];

            GetLUP(ref Lm, ref Um, ref Pm, MyMatrix);
            
            Console.WriteLine("L = ");
            PrintMatrix(Lm);
            Console.WriteLine("");

            Console.WriteLine("U = ");
            PrintMatrix(Um);
            Console.WriteLine("");

            Console.WriteLine("P = ");
            PrintMatrix(Pm);
            Console.WriteLine("");

            Console.WriteLine("Solved by LUP decomposition: X = ");
            PrintMatrix(SolveUpperTriangularMatrix(Um, SolveLowerTriangularMatrix(Lm, GetSwapedMatrix(MyB, Pm))));
            Console.WriteLine("");

            Console.WriteLine("Solved by Square root method: X = ");
            PrintMatrix(SolveWithSquareRoot(MyMatrix,MyB));
            Console.WriteLine("");

            Console.WriteLine("Solved by reflection method: X = ");
            PrintMatrix(SolveWithReflectionMethod(MyMatrix, MyB));
            Console.WriteLine("");

            #endregion


        }

        public static float[] SolveWithSquareRoot(float[,] A, float[] B)
        {
            float[,] Ut = GetSquareRootOfMatrix(A);
            if (Ut == null)
            {
                return null;
            }
            float[,] U = GetTransposedMatrix(Ut);

            return SolveUpperTriangularMatrix(Ut, SolveLowerTriangularMatrix(U, B));

        }

        public static float GetCubicVectorNorm(float[] A)
        {
            float max = float.MinValue;
            
            for (int i = 0; i < A.GetLength(0); i++)
            {
                max = Math.Max(max,Math.Abs( A[i]));
            }
            return max;
        }

        public static float[] SolveWithReflectionMethod(float[,] A, float[] B)
        {
            A = (float[,])A.Clone();

            int size = A.GetLength(0);

            for (int i = 0; i < size-1; i++)
            {

                float[] w = new float[size-i];

                float[] xs=new float[size-i];

                float[] aVector = GetVectorFromMatrix(A, i, i, size - 1);

                xs[0] = GetMagnitude(aVector);


                for (int j = 0; j < w.GetLength(0); j++)
                {
                    w[j] = aVector[j] - xs[j];
                }

                float magnitudeW = GetMagnitude(w);

                for (int j = 0; j < w.GetLength(0); j++)
                {
                    w[j]/= magnitudeW;
                }


                int index = 0;
                for (int j = i; j < size; j++)
                {
                    A[j, i] = xs[index++];
                }



                for (int k = i+1; k <size ; k++)
                {
                    float[] aP = GetVectorFromMatrix(A, i, k,size-1);
                    float scalarMultiplic = ScalarMultiplication(aP, w);

                    int counterW = 0;
                    for (int j = i; j < size; j++)
                    {
                        A[j, k] = A[j, k] - 2 * (scalarMultiplic) * w[counterW++];
                    }
                }

                float[] bP = GetSubVector(B,i,size-1);

                float scalarMultiplicB = ScalarMultiplication(bP, w);

                int counterWB = 0;
                for (int j = i; j < size; j++)
                {
                    B[j] = B[j] - 2 * (scalarMultiplicB) * w[counterWB++];
                }
            }

            return SolveUpperTriangularMatrix(A,B);
        }

        public static float[] GetSubVector(float[] Vector,int start, int end)
        {
            float[] subVector = new float[(end - start) + 1];

            for (int i = 0; i < subVector.GetLength(0); i++)
            {
                subVector[i] = Vector[start + i];
            }

            return subVector;
        }

        public static float[] GetVectorFromMatrix(float[,] A, int startRow, int startColumn, int endRow)
        {
            float[] vector = new float[(endRow-startRow)+1];

            for (int i = 0; i < vector.GetLength(0); i++)
            {
                vector[i] = A[startRow+i, startColumn];
            }

            return vector;
        }

        public static float ScalarMultiplication(float[] A, float[] B)
        {
            float sum = 0;

            for (int i = 0; i < A.GetLength(0); i++)
            {
                sum += A[i] * B[i];
            }

            return sum;
        }

        public static float GetMagnitude(float[] A)
        {
            float sum = 0;

            int size = A.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                sum += (float)A[i]*A[i];
            }

            return (float)Math.Sqrt(sum);
        }

        public static float GetMagnitude(float[,] A,int row, int column)
        {
            float sum = 0;

            int size = A.GetLength(0);

            for (int i = row; i < size; i++)
            {
                sum +=(float)Math.Pow(A[i,column], 2);
            }

            return (float)Math.Sqrt(sum);
        }

        public static float[,] GetTransposedMatrix(float[,] A)
        {
            A = (float[,])A.Clone();

            int size = A.GetLength(0);

            float[,] Answer = new float[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Answer[i, j] = A[j, i];
                }
            }

            return Answer;
        }
        public static float[,] GetSquareRootOfMatrix(float[,] A)
        {
            A = (float[,])A.Clone();
            
            if (A[0, 0] < 0)
            {
                return null;
            }

            int size = A.GetLength(0);

            float[,] U = new float[size, size];

            U[0, 0] = (float)Math.Sqrt(A[0, 0]);

            for (int i = 1; i < size; i++)
            {
                U[0, i] = A[0, i] / U[0, 0];
            }

            for (int i = 1; i < size; i++)
            {
                float sum = 0;
                for (int k = 0; k < i; k++)
                {
                    sum +=(float)Math.Pow(U[k, i], 2);
                }

                //If give matrix was not positive
                if (A[i, i] - sum < 0)
                {
                    Console.WriteLine("Found less than zero");
                    return null;
                }
                U[i, i] = (float)Math.Sqrt(A[i, i] - sum);

                for (int j = i+1; j < size; j++)
                {
                    float sum2 = 0;

                    for (int k = 0; k < i; k++)
                    {
                        sum2 += U[k, i] * U[k, j];
                    }

                    U[i, j] = (A[i, j] - sum2) / U[i, i];
                }

            }

            for (int j = 0; j < size; j++)
            {
                for (int i = j+1; i < size; i++)
                {
                    U[i, j] = 0;
                }
            }
            return U;
        }

        public static float[,] GetSwapedMatrix(float[,] A, int[] P)
        {
            float[,] answer = (float[,])A.Clone();
            P = (int[])P.Clone();

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(0); j++)
                {
                    answer[i,j] = A[P[i],j];
                }

            }

            return answer;
        }

        public static float[] GetSwapedMatrix(float[] A, int[] P)
        {
            float[] answer = (float[])A.Clone();
            P = (int[])P.Clone();

            for (int i = 0; i < A.GetLength(0); i++)
            {
                answer[i] = A[P[i]];

            }

            return answer;
        }

        public static float[] SolveUpperTriangularMatrix(float[,] A, float[] B)
        {
            A = (float[,])A.Clone();
            B = (float[])B.Clone();

            int size = A.GetLength(0);

            float[] X = new float[size];

            for (int i = size - 1; i >= 0; i--)
            {
                float sum = 0;
                for (int j = size-1; j > i; j--)
                {
                    sum += X[j] * A[i, j];
                }
                sum = B[i] - sum;
                X[i] = sum / A[i, i];
            }
            return X;
        }

        public static float[] SolveLowerTriangularMatrix(float[,] A, float[] B)
        {
            A = (float[,])A.Clone();
            B = (float[])B.Clone();

            int size = A.GetLength(0);

            float[] X = new float[size];

            for (int i = 0; i < size; i++)
            {
                float sum = 0;
                for (int j = 0; j < i; j++)
                {
                    sum += X[j] * A[i, j];
                }
                sum = B[i] - sum;
                X[i] = sum / A[i, i];
            }
            return X;
        }

        public static void GetInitialCondition(ref float[,] A, ref float[] Y, ref float[] B,int size)
        { 
            A=new float[size,size];
            Y=new float[size];
            B=new float[size];

            int N = 10;

            float leftBorder = -(float)Math.Pow(2, N / 4.0f);
            float rightBorder = (float)Math.Pow(2, N / 4.0f);

            Random rng = new Random(228);

            for (int i = 0; i < size; i++)
            {
                Y[i] = (float)GetRandomNumber(leftBorder, rightBorder);
                for (int j = i+1; j < size; j++)
                {

                    A[i, j] =(float)GetRandomNumber(leftBorder, rightBorder);
                   
                }

            }

            for (int i = 1; i < size; i++)
            {
                for (int j = 0; j < i; j++)
                {

                    A[i, j] = A[j,i];
                }
            }

            for (int i = 0; i < size; i++)
            {
                float sum = 0;
                for (int j = 0; j < size; j++)
                {
                    if (i != j)
                    {
                        sum += A[i, j];
                    }
                }
                A[i, i] = sum;
            }
            B = MatrixMultiplication(A, Y);



        }

       public static float GetConditionNumber(float[,] A)
       {
           return GetCubicNorm(A) * GetCubicNorm(GetInverseMatrix(A));
       }
        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            double result = random.NextDouble() * (maximum - minimum) + minimum;

            while (result==maximum)
            {
                result = random.NextDouble() * (maximum - minimum) + minimum;
            }
            return result;
        }

        public static float[,] MatrixMultiplication(float[,] A, float[,] B)
        {
            int n = A.GetLength(0);
            int m = A.GetLength(1);
            int k = B.GetLength(1);

            float [,] C = new float[n,k];

            for (int i = 0; i < n; i++)
            {
                for (int z = 0; z < k; z++)
                {
                    float sum = 0;

                    for (int j = 0; j < m; j++)
                    {
                        sum += A[i, j] * B[j, z];
                    }
                    C[i, z] = sum;
                    //TODO: comment to do not change precision
                    if (Math.Abs(C[i, z]) < 0.0000001)
                    {
                        C[i, z] = 0;
                    }

                }
            }
            return C;
        }

        public static float[] MatrixMultiplication(float[,] A, float[] B)
        {
            int n = A.GetLength(0);
            int m = A.GetLength(1);
            int k =1;

            float[] C = new float[n];

            for (int i = 0; i < n; i++)
            {
                float sum = 0;

                    for (int j = 0; j < m; j++)
                    {
                        sum += A[i, j] * B[j];
                    }
                    C[i] = sum;
                
            }
            return C;
        }

        public static void PrintMatrix(float[,] A)
        {
            for (int i = 0; i < A.GetLength(0); i++)
            {
               
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    Console.Write($" {A[i,j]}|");
                }
                Console.WriteLine("");
            }
        }

        public static void PrintMatrix(float[] A)
        {
            for (int i = 0; i < A.GetLength(0); i++)
            {
                //Console.WriteLine("----------------------------------");
                Console.WriteLine($" {A[i]}|");
            }
        }

        public static void PrintMatrix(int[] A)
        {
            for (int i = 0; i < A.GetLength(0); i++)
            {
                //Console.WriteLine("----------------------------------");
                Console.WriteLine($" {A[i]}|");
            }
        }

        public static float[,] GetInverseMatrix(float[,] A)
        {
            A =(float[,])A.Clone();
            int size = A.GetLength(0);
            float[,] B=new float[size,size];

            for (int i = 0; i < size; i++)
            {
                B[i, i] = 1;
            }
            for (int j = 0; j < size - 1; j++)
            {
                float leadValue = A[j, j];

                for (int i = j + 1; i < size; i++)
                {
                    float coef = -A[i, j] * 1 / leadValue;

                    for (int k = 0; k < size; k++)
                    {
                        A[i, k] += A[j, k] * coef;
                        B[i,k] += B[j,k] * coef;
                        //TODO: comment to do not change precision
                        if (Math.Abs(A[i, k]) < 0.00001)
                        {
                            A[i, k] = 0;
                        }
                    }

                   
                }
            }

            for (int j = size - 1; j > 0; j--)
            {
                float leadValue = A[j, j];

                for (int i = j - 1; i >= 0; i--)
                {
                    float coef = -A[i, j] * 1 / leadValue;

                    for (int k = 0; k < size; k++)
                    {
                        A[i, k] += A[j, k] * coef;
                        B[i, k] += B[j, k] * coef;
                        //TODO: comment to do not change precision
                        if (Math.Abs(A[i, k]) < 0.00001)
                        {
                            A[i, k] = 0;
                        }
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

        public static float[] SolveMatrix(float[,] A, float[] B)
        {
            A = (float[,])A.Clone();
            B = (float[])B.Clone();

            int size = A.GetLength(0);

            for (int j = 0; j < size-1; j++)
            {
               KillColumn(A,B,j,j);
            }

            for (int j = size-1; j >0 ; j--)
            {
                KillColumn(A, B, j, j);
            }

            float[] X = new float[size];
            
            for (int i = 0; i < size; i++)
            {
                X[i] = B[i] / A[i, i];
            }
            return X;
        }
        public static void KillColumn(float[,] A, float[] B,int mainRowIndex, int columnIndex, float[,] L=null ,bool whole=true)
        {
            int size = A.GetLength(0);

            float leadValue = A[mainRowIndex, columnIndex];


            int i;

            if (whole)
            {
                i = 0;
            }
            else
            {
                i = mainRowIndex + 1;
            }

            for ( i=i ; i < size; i++)
            {
                if (i == mainRowIndex)
                {
                    continue;
                }

                float coef = -A[i, columnIndex] * 1 / leadValue;
                
                if (L != null)
                {
                    L[i, columnIndex] = -coef;
                }

                for (int k = 0; k < size; k++)
                {
                    A[i, k] += A[columnIndex, k] * coef;

                    //TODO: comment to do not change precision
                    //if (Math.Abs(A[i, k]) < 0.000001)
                    //{
                    //    A[i, k] = 0;
                    //}
                }
                B[i] += B[columnIndex] * coef;
            }
        }

        public static float[] SolveMatrixMainElementInMatrix(float[,] A, float[] B)
        {
            A = (float[,])A.Clone();
            B = (float[])B.Clone();

            int size = A.GetLength(0);

            int[] xOrder = new int[size];
            
            for (int i = 0; i < size; i++)
            {
                xOrder[i] = i;
            }


            for (int z = 0; z < size - 1; z++)
            {
                int mI=0;
                int mJ=0;
                float maxElement = float.MinValue;

                for (int i = z; i < size; i++)
                {
                    for (int j = z; j < size; j++)
                    {
                        if (maxElement < A[i, j])
                        {
                            maxElement =Math.Abs( A[i, j]);
                            mI = i;
                            mJ = j;
                        }
                       
                    }
                }
                SwapRows(ref A,mI,z);
                float temp = B[z];
                B[z] = B[mI];
                B[mI] = temp;

                SwapColumns(ref A, mJ, z);
                int tempX = xOrder[z];
                xOrder[z] = xOrder[mJ];
                xOrder[mJ] = tempX;

                KillColumn(A,B,z,z);
            }

            for (int j = size - 1; j > 0; j--)
            {
                KillColumn(A, B, j, j);
            }

            float[] X = new float[size];
            for (int i = 0; i < size; i++)
            {
                X[i] = B[i] / A[i, i];
            }
            float[] Xresult=new float[size];

            for (int i = 0; i < size; i++)
            {
                Xresult[i] = X[xOrder[i]];
            }

            return Xresult;
        }

        public static void SwapRows(ref float[,] A, int rowA, int rowB)
        {
            int size = A.GetLength(0);

            float[] temp = new float[size];

            for (int i = 0; i < size; i++)
            {
                temp[i] = A[rowA, i];
            }

            for (int i = 0; i < size; i++)
            {
                 A[rowA, i]=A[rowB,i];
            }

            for (int i = 0; i < size; i++)
            {
                A[rowB, i] = temp[i];
            }
        }

        public static void SwapColumns(ref float[,] A, int columnA, int columnB)
        {
            int size = A.GetLength(0);

            float[] temp = new float[size];

            for (int i = 0; i < size; i++)
            {
                temp[i] = A[i,columnA];
            }

            for (int i = 0; i < size; i++)
            {
                A[i,columnA] = A[i,columnB];
            }

            for (int i = 0; i < size; i++)
            {
                A[i,columnB] = temp[i];
            }
        }

        public static void GetLUP(ref float[,] L, ref float[,] U, ref int[] P, float[,] A)
        {
            int size = A.GetLength(0);
            A = (float[,])A.Clone();
            P=new int[size];
            L=new float[size,size];

            for (int i = 0; i < size; i++)
            {
                L[i, i] = 1;
            }

            for (int i = 0; i < size; i++)
            {
                P[i]=i;
            }

            for (int i = 0; i < size; i++)
            {
                float max = float.MinValue;
                int maxIndex = 0;

                for (int j = i; j < size; j++)
                {
                    if(max<Math.Abs(A[j, i]))
                    {
                        max = Math.Abs(A[j, i]);
                        maxIndex = j;
                    }
                }


                SwapRows(ref A,i,maxIndex);
                SwapInt(P,i,maxIndex);

                for (int j = 0; j < i; j++)
                {
                    float temp = L[i, j];
                    L[i, j] = L[maxIndex, j];
                    L[maxIndex, j] = temp;
                }

                KillColumn(A,new float[size],i,i ,L,false);
            }

            U = (float[,])A.Clone();
        }

        public static void SwapIntValues(int[] array, int valueA, int valueB)
        {
            int aIndex = 0;
            int bIndex = 0;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                if (array[i] == valueA)
                {
                    aIndex = i;
                }
                if (array[i] == valueB)
                {
                    bIndex = i;
                }
            }

            SwapInt(array, aIndex,bIndex);
        }

        public static void SwapInt(int[] array, int indexA, int indexB)
        {
            int temp = array[indexA];
            array[indexA] = array[indexB];
            array[indexB] = temp;
        }
        public static float GetCubicNorm(float[,] A)
        {
            float maxSum = float.MinValue;

            for (int i = 0; i < A.GetLength(0); i++)
            {
                float sum = 0;

                for (int j = 0; j < A.GetLength(0); j++)
                {
                    sum += Math.Abs(A[i, j]);
                }

                maxSum = Math.Max(maxSum, sum);

            }

            return maxSum;
        }
        
    }
}
