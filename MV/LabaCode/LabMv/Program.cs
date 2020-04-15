using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.CompilerServices;

namespace LabMv
{
    class Program
    {
        static void Main(string[] args)
        {
            float[,] A=new float[1,1];
            float[] Y=new float[1];
            float[] B=new float[1];

            GetInitialCondition(ref A,ref Y,ref B,5);

            Console.WriteLine("------------\n A = ");
            PrintMatrix(A);

            Console.WriteLine("------------\n Y = ");
            PrintMatrix(Y);

            Console.WriteLine("------------\n B = ");
            PrintMatrix(B);


            Console.WriteLine("------------\n ");
            //A = new float[,] {{4, 3},{ 3, 2}};
            //B = new float[]{3, 4};

            float[,] L=new float[1,1];
            float[,] U=new float[1,1];
            int[] P=new int[1];

            //A=new float[,]{{-2,2,3,3},{4,5,6,2},{7,8,9,3},{1,2,3,9}};
            Console.WriteLine("A = ");
            PrintMatrix(A);
            Console.WriteLine("");

            GetLUP(ref L,ref U,ref P,A);

            //Console.WriteLine("L = ");
            //PrintMatrix(L);
            //Console.WriteLine("");

            //Console.WriteLine("U = ");
            //PrintMatrix(U);
            //Console.WriteLine("");

            //Console.WriteLine("P = ");
            //PrintMatrix(P);
            //Console.WriteLine("");

            Console.WriteLine("P * A = ");
            PrintMatrix(GetSwapedMatrix(A, P));
            Console.WriteLine("");

            Console.WriteLine("L * U = ");
            PrintMatrix(MatrixMultiplication(L, U));
            Console.WriteLine("");

            Console.WriteLine("AX = B\n X = ");
            PrintMatrix(SolveMatrix(U,SolveMatrix(L,GetSwapedMatrix(B,P))));
            Console.WriteLine("");

            Console.WriteLine($"condition number = {GetConditionNumber(A)}");

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

            Console.WriteLine("A = ");
            PrintMatrix(A);
            Console.WriteLine("");
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
