using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Json;

namespace LabMV2
{
    public class DesktopDrawingTests
    {
        private const int DCX_WINDOW = 0x00000001;
        private const int DCX_CACHE = 0x00000002;
        private const int DCX_LOCKWINDOWUPDATE = 0x00000400;

        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetDCEx(IntPtr hwnd, IntPtr hrgn, uint flags);


        public void TestDrawingOnDesktop()
        {
            IntPtr hdc = GetDCEx(GetDesktopWindow(),
                IntPtr.Zero,
                DCX_WINDOW | DCX_CACHE | DCX_LOCKWINDOWUPDATE);

            using (Graphics g = Graphics.FromHdc(hdc))
            {
                g.FillEllipse(Brushes.Red, 0, 0, 400, 400);
            }
        }
    }

    static class Program
    {
        public static int N = 10;

        static void Main(string[] args)
        {


            //Testing

            double[,] RealA = GetRandomMatrix();

            double[,] A = GetRandomMatrix();

            List<double> normsFound = new List<double>();
            List<long> timeTakenForPowerMethod = new List<long>();
            List<long> timeTakenForQRMethod = new List<long>();

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Calculating... {0}% ", i/100);
                A = GetRandomMatrix();

                PrintMatrix(A);

                Stopwatch timer=new Stopwatch();

                timer.Start();
                
                var result = SolveGetMaxAbsoluteEigenValue(A);
                
                timer.Stop();

                timeTakenForPowerMethod.Add(timer.ElapsedMilliseconds);

                for (int j = 0; j < result.Length; j++)
                {

                    normsFound.Add(GetEigenNorm(A,result[j].Item1,result[j].Item2));
                }

                timer.Reset();

                timer.Start();

                var resultMin = SolveGetMaxAbsoluteEigenValue(GetInverseMatrix(A));

                timer.Stop();

                timeTakenForPowerMethod.Add(timer.ElapsedMilliseconds);

                for (int j = 0; j < resultMin.Length; j++)
                {

                    normsFound.Add(GetEigenNorm(A, resultMin[j].Item1, resultMin[j].Item2));
                }


                timer.Reset();

                timer.Start();

                Complex[] qrResult = GetEigenNumbersQR(A);

                timer.Stop();

                timeTakenForQRMethod.Add(timer.ElapsedMilliseconds);

                Console.WriteLine();

            }

            Console.WriteLine($@"Average norm = {normsFound.Average()}");

            Console.WriteLine($@"Min time to find value using power method  = {timeTakenForPowerMethod.Min()}");

            Console.WriteLine($@"Max time to find value using power method  = {timeTakenForPowerMethod.Max()}");

            Console.WriteLine($@"Average time to find value using power method  = {timeTakenForPowerMethod.Average()}");

            Console.WriteLine($@"Min time to find all values using QR method  = {timeTakenForQRMethod.Min()}");

            Console.WriteLine($@"Max time to find all values using QR method  = {timeTakenForQRMethod.Max()}");

            Console.WriteLine($@"Average time to find all values using QR method  = {timeTakenForQRMethod.Average()}");

            //PrintMatrix(A);
            //Console.WriteLine("---------------------");
            //PrintMatrix(GetInverseMatrix(A));

            //double[,] Q = new double[1, 1];
            //double[,] R = new double[1, 1];

            //GetQR(A, ref Q, ref R);

            //Console.WriteLine("Q = \n");

            //PrintMatrix(Q);

            //Console.WriteLine("R= \n");
            //PrintMatrix(R);

            //Console.WriteLine("Q*R= \n");
            //PrintMatrix(MatrixMultiplication(Q, R));

            //Console.WriteLine("A eigen values= \n");
            //PrintVector(GetEigenNumbersQR(A));

            //var result = SolveGetMaxAbsoluteEigenValue(GetInverseMatrix( A));

            //for (int i = 0; i < result.Length; i++)
            //{
            //    Console.WriteLine("Max: \n");
            //    Console.WriteLine(result[i].Item1.String());
            //    Console.WriteLine("Vector: \n");
            //    PrintVector(result[i].Item2);
            //}

            //var eigens = GetEigenNumbersQR(GetInverseMatrix(A));


            //Console.WriteLine("Eigen values");

            //for (int i = 0; i < eigens.Length; i++)
            //{
            //    Console.Write($@" {eigens[i].String()} |");

            //}

            //Console.WriteLine();
            //Console.WriteLine("Min: \n");

            //result = GetMaxEigenValueCase3(GetInverseMatrix(A))[0];

            //Console.WriteLine((1/result.Item1).String());

            //Console.WriteLine("Vector: \n");
            //PrintVector(result.Item2);

            //Console.WriteLine("Check diff: \n");
            //PrintVector(VectorSubtraction(MatrixMultiplication(ToComplex( A), result.Item2).ToArray(), VecotorWithScalarMultiplication(result.Item2, 1/result.Item1)));

            //Console.WriteLine(double.Epsilon);
            double x;

            //Console.WriteLine("Classic nuton \n");

            //Console.WriteLine("\n x1 = ");
            // x = solveXWithNormalNuton(0, 1);

            //Console.WriteLine(x);
            //Console.WriteLine("\n f(x1) = " + Func(x));


            //Console.WriteLine("\n x2 = ");
            //x = solveXWithNormalNuton(1, 1.5);

            //Console.WriteLine(x);
            //Console.WriteLine("\n f(x2) = " + Func(x));

            //Console.WriteLine("\n x3 = ");

            //x = solveXWithNormalNuton(1.5, 2);

            //Console.WriteLine(x);
            //Console.WriteLine("\n f(x3) = " + Func(x));


            //Console.WriteLine("Discrete nuton \n");

            //double h = 5;

            //Console.WriteLine($@"h = {h} ");

            //Console.WriteLine("\n x1 = ");
            //x = solveXWithDiscretNuton(0, 1,h);

            //Console.WriteLine(x);
            //Console.WriteLine("\n f(x1) = " + Func(x));


            //Console.WriteLine("\n x2 = ");
            //x = solveXWithDiscretNuton(1, 1.5,h);

            //Console.WriteLine(x);
            //Console.WriteLine("\n f(x2) = " + Func(x));

            //Console.WriteLine("\n x3 = ");

            //x = solveXWithDiscretNuton(1.5, 2,h);

            //Console.WriteLine(x);
            //Console.WriteLine("\n f(x3) = " + Func(x));

            //DesktopDrawingTests drawing=new DesktopDrawingTests();

            //drawing.TestDrawingOnDesktop();
            //Console.ReadKey();
        }

        public static void PrintVector(Complex[] vector)
        {
            for (int i = 0; i < vector.GetLength(0); i++)
            {
                Console.Write(" " + vector[i].String() + "|");
            }

            Console.WriteLine("");
        }

        public static double GetEigenNorm(double[,] A_, Complex eigenValue, Complex[] eigenVector)
        {
            Complex[,] A = ToComplex(A_);

            return GetMagnitude(VectorSubtraction(MatrixMultiplication(A, eigenVector).ToArray(),
                VecotorWithScalarMultiplication(eigenVector, eigenValue))).Magnitude;

        }
        public static string String(this Complex numComplex)
        {
            string outPut = "";

            if (numComplex.Magnitude == 0)
            {
                return "0";
            }

            if (Math.Abs(numComplex.Real) > Double.Epsilon)
            {
                outPut += numComplex.Real;
            }

            if (Math.Abs(numComplex.Imaginary) > Double.Epsilon)
            {
                if (outPut != "")
                {
                    outPut += " + ";
                }
                outPut += numComplex.Imaginary;

                outPut += "i";
            }

            return outPut;
        }

        public static Tuple<Complex, Complex[]>[] SolveGetMaxAbsoluteEigenValue(double[,] A)
        {
            Tuple<Complex, Complex[]>[] result = new Tuple<Complex, Complex[]>[A.GetLength(0)];

            result = GetMaxEigenValueCase1(A);

            if (result == null)
            {
                result = GetMaxEigenValueCase3(A);

            }

            if (result == null)
            {
                result = GetMaxEigenValueCase4(A);
            }

            return result;
        }

        public static Tuple<Complex, Complex[]>[] GetMaxEigenValueCase4(double[,] A_)
        {
            Complex[,] A = ToComplex(A_);

            // initial approximation
            Complex[] u = new Complex[A.GetLength(0)];
            u[0] = 2.4;

            Complex eigenValue = 0;

            //precision
            double epsilon = 0.00000001;

            int iterationCount = 0;

            List<Complex[]> vectors = new List<Complex[]>();


            while (true )
            {
                Complex[] v = MatrixMultiplication(A, u);



                eigenValue = GetMax(v);

                u = v;

                vectors.Add(u);

                iterationCount++;

                if (iterationCount > 3)
                {
                    double yk2 = vectors[iterationCount - 1][0].Real;
                    double yk1 = vectors[iterationCount - 2][0].Real;
                    double yk = vectors[iterationCount - 3][0].Real;
                    double ykm1 = vectors[iterationCount - 4][0].Real;

                    double r = Math.Sqrt((yk * yk2 - yk1 * yk1) / (ykm1 * yk1 - yk * yk));

                    double cosPhi = (yk2 + r * r * yk) / (2 * r * yk1);
                    double sinPhi = Math.Sqrt(1 - cosPhi * cosPhi);

                    Complex number1 = new Complex(r * cosPhi, r * sinPhi);
                    Complex number2 = new Complex(r * cosPhi, -r * sinPhi);

                    Complex[] vector1 = VectorSubtraction(vectors[iterationCount - 2], VecotorWithScalarMultiplication(vectors[iterationCount - 3], number2));

                    Complex[] vector2 = VectorSubtraction(vectors[iterationCount - 2], VecotorWithScalarMultiplication(vectors[iterationCount - 3], number1));


                    if (GetMagnitude(VectorSubtraction(MatrixMultiplication(A, vector1).ToArray(), VecotorWithScalarMultiplication(vector1, number1))).Magnitude < epsilon)
                    {
                        Tuple<Complex, Complex[]>[] result = new Tuple<Complex, Complex[]>[2];

                        result[0] = new Tuple<Complex, Complex[]>(number1, vector1);
                        result[1] = new Tuple<Complex, Complex[]>(number2, vector2);

                        //Console.WriteLine("Fourth case method could succeed! Iterations done: " + iterationCount);
                        return result;
                    }
                }
            }

        }

        public static Complex[,] ToComplex(double[,] A)
        {
            Complex[,] newA = new Complex[A.GetLength(0), A.GetLength(1)];

            for (int i = 0; i < newA.GetLength(0); i++)
            {
                for (int j = 0; j < newA.GetLength(1); j++)
                {
                    newA[i, j] = A[i, j];
                }
            }

            return newA;
        }

        public static Complex[] normalized(this Complex[] vector)
        {
            return VectorWithScalarDivison(vector, GetMagnitude(vector));
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


        public static Tuple<Complex, Complex[]>[] GetMaxEigenValueCase1(double[,] A_)
        {
            Complex[,] A = ToComplex(A_);

            // initial approximation
            Complex[] u = new Complex[A.GetLength(0)];
            u[0] = 1;
            u[1] = (Complex)(1 + 2m);

            Complex eigenValue = 0.082;

            //precision
            double epsilon = 0.000001;

            int iterationCount = 0;

            Complex[] checkVector = u;
            Complex checkEigen = eigenValue;

            while (GetMagnitude(VectorSubtraction(MatrixMultiplication(A, u).ToArray(), VecotorWithScalarMultiplication(u, eigenValue))).Magnitude > epsilon)
            {
                //Console.WriteLine(GetMagnitude(VectorSubtraction(MatrixMultiplication(A, u).ToArray(), VecotorWithScalarMultiplication(u, eigenValue))));

                iterationCount++;

                Complex[] v = MatrixMultiplication(A, u);

                eigenValue = GetMax(v);

                u = VectorWithScalarDivison(v, eigenValue);


                if (iterationCount % 10 == 0)
                {
                    double precisionNow = GetMagnitude(VectorSubtraction(MatrixMultiplication(A, u).ToArray(), VecotorWithScalarMultiplication(u, eigenValue))).Magnitude;
                    double precisionPast = GetMagnitude(VectorSubtraction(MatrixMultiplication(A, checkVector).ToArray(), VecotorWithScalarMultiplication(u, checkEigen))).Magnitude;
                    if (Math.Abs(precisionNow) - Math.Abs(precisionPast) > 0)
                    {
                        //No chance here... aborting...

                        //Console.WriteLine("First case method could not succeed... Iterations done: " + iterationCount);
                        return null;
                    }
                    else
                    {
                        checkVector = u;
                        checkEigen = eigenValue;
                    }
                }
            }


            Tuple<Complex, Complex[]>[] result = new Tuple<Complex, Complex[]>[1];

            result[0] = new Tuple<Complex, Complex[]>(eigenValue, u);

            //Console.WriteLine("First case method could succeed! Iterations done: " + iterationCount);

            return result;
        }

        public static Complex GetMax(Complex[] complexes)
        {
            Complex max = Complex.Zero;

            for (int i = 0; i < complexes.Length; i++)
            {
                if (max.Magnitude < complexes[i].Magnitude)
                {
                    max = complexes[i];
                }
            }

            return max;
        }

        public static Tuple<Complex, Complex[]>[] GetMaxEigenValueCase3(double[,] A_)
        {
            Complex[,] A = ToComplex(A_);

            // initial approximation
            Complex[] u = new Complex[A.GetLength(0)];
            Complex[] nextUStep = u;

            u[0] = 1;
            u[1] = (Complex)(1 + 2m);

            Complex eigenValue = 0.082;

            //precision
            double epsilon = 0.00001;

            int iterationCount = 0;

            Complex[] checkVector = u;
            Complex checkEigen = eigenValue;

            while (GetMagnitude(VectorSubtraction(MatrixMultiplication(A, u).ToArray(), VecotorWithScalarMultiplication(u, eigenValue))).Magnitude > epsilon)
            {
                //Console.WriteLine(GetMagnitude(VectorSubtraction(MatrixMultiplication(A, u).ToArray(), VecotorWithScalarMultiplication(u, eigenValue))));

                iterationCount++;
                u = (MatrixMultiplication(A, MatrixMultiplication(A, u)));

                eigenValue = Complex.Sqrt((GetMax(u)));

                u = VectorWithScalarDivison(u, GetMax(u));

                nextUStep = MatrixMultiplication(A, u);

                if (iterationCount % 10 == 0)
                {
                    double precisionNow = GetMagnitude(VectorSubtraction(MatrixMultiplication(A, u).ToArray(), VecotorWithScalarMultiplication(u, eigenValue))).Magnitude;
                    double precisionPast = GetMagnitude(VectorSubtraction(MatrixMultiplication(A, checkVector).ToArray(), VecotorWithScalarMultiplication(u, checkEigen))).Magnitude;
                    if (Math.Abs(precisionNow) - Math.Abs(precisionPast) > 0)
                    {
                        //No chance here... aborting...
                        //Console.WriteLine("Third case method could not succeed... Iterations done: " + iterationCount);
                        return null;
                    }
                    else
                    {
                        checkVector = u;
                        checkEigen = eigenValue;
                    }
                }

            }

            //Console.WriteLine("Difference Vector");
            //PrintVector(VectorSubtraction(MatrixMultiplication(A, u).ToArray(), VecotorWithScalarMultiplication(u, eigenValue)));

            Tuple<Complex, Complex[]>[] result = new Tuple<Complex, Complex[]>[2];

            result[0] = new Tuple<Complex, Complex[]>(eigenValue, VectorSum(nextUStep, VecotorWithScalarMultiplication(u, eigenValue)));
            result[1] = new Tuple<Complex, Complex[]>(-eigenValue, VectorSubtraction(nextUStep, VecotorWithScalarMultiplication(u, eigenValue)));

            //Console.WriteLine("Second case method could succeed! Iterations done: " + iterationCount);

            return result;
        }

        public static Complex[] VecotorWithScalarMultiplication(Complex[] vector, Complex scalar)
        {
            vector = (Complex[])vector.Clone();
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] *= scalar;
            }

            return vector;
        }


        public static Complex[] VectorSubtraction(Complex[] a, Complex[] b)
        {
            a = (Complex[])a.Clone();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] -= b[i];
            }

            return a;
        }

        public static Complex[] VectorSum(Complex[] a, Complex[] b)
        {
            a = (Complex[])a.Clone();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] += b[i];
            }

            return a;
        }


        public static Complex[] VectorWithScalarDivison(Complex[] a, Complex b)
        {
            a = (Complex[])a.Clone();
            return VecotorWithScalarMultiplication(a, 1 / b);
        }

        public static Complex[] MatrixMultiplication(Complex[,] A, Complex[] B)
        {
            int n = A.GetLength(0);
            int m = A.GetLength(1);
            int k = 1;

            Complex[] C = new Complex[n];

            for (int i = 0; i < n; i++)
            {
                Complex sum = 0;

                for (int j = 0; j < m; j++)
                {
                    sum += A[i, j] * B[j];
                }
                C[i] = sum;

            }
            return C;
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

        public static Complex GetMagnitude(Complex[] A)
        {
            Complex sum = 0;

            int size = A.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                sum += (Complex)A[i] * A[i];
            }

            return (Complex)Complex.Sqrt(sum);
        }


#region RealNumberOperation

        public static double[,] GetRandomMatrix()
        {
            double[,] A = new double[10, 10];

            Random random = new Random();

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


        public static Complex[] GetEigenNumbersQR(double[,] A)
        {
            A = (double[,])A.Clone();

            List<Complex> result = new List<Complex>();

            for (int i = 0; i < 10000; i++)
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
                    double b = Math.Sqrt(Math.Abs(Math.Pow(a, 2) - A[i + 1, i + 1] * A[i, i] + A[i + 1, i] * A[i, i + 1]));

                    A[i, i] = a;
                    A[i, i + 1] = b;
                    A[i + 1, i] = -b;
                    result.Add(new Complex(a, b));
                    result.Add(new Complex(a, -b));
                    i++;
                }
                else
                {
                    result.Add(new Complex(A[i, i], 0));
                }
            }

            return result.ToArray();
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

#endregion

#region UnlinearSolve

        public static double Func(double x)
        {
            return Math.Tan(Math.Cos(2 * x)) + Math.Log(2 * x);
        }

        public static double FuncP(double x)
        {
            return -2 * Math.Sin(2 * x) / Math.Cos(Math.Cos(2 * x)) + 1 / x;
        }


        public static Tuple<double, double> besectionMethod(double a, double b)
        {
            double epsilon = 0.0001;
            double x;

            while (Math.Abs(b - a) > epsilon)
            {
                x = (a + b) / 2;
                if (Func(x) * Func(a) < 0)
                {
                    b = x;
                }
                else
                {
                    a = x;
                }
            }

            Console.WriteLine($@"new range =[ {a} {b} ] ");

            return new Tuple<double, double>(a, b);
        }

        public static double NutonMethod(double startX)
        {
            double x = startX;
            double epsilon = 10e-16;

            Console.WriteLine("Epsilon = " + epsilon);


            int itCount = 0;
            while (true)
            {
                double newX = x - Func(x) / FuncP(x);

                if (Math.Abs(x - newX) < epsilon)
                {
                    break;
                }

                itCount++;
                x = newX;
            }

            Console.WriteLine($@"Discrete method exited in {itCount} iterations");

            return x;

        }

        public static double NutonMethodDiscret(double startX, double h)
        {
            double x = startX;
            double epsilon = 10e-16;

            int itCount = 0;
            while (true)
            {
                double newX = x - Func(x) / h;

                if (Math.Abs(x - newX) < epsilon)
                {
                    break;
                }
                itCount++;
                x = newX;
            }

            Console.WriteLine($@"Discrete method exited in {itCount} iterations");

            return x;

        }

        public static double solveXWithNormalNuton(double a, double b)
        {
            Tuple<double, double> newRange = besectionMethod(a, b);

            a = newRange.Item1;
            b = newRange.Item2;

            return NutonMethod(a);
        }

        public static double solveXWithDiscretNuton(double a, double b, double h)
        {
            Tuple<double, double> newRange = besectionMethod(a, b);

            a = newRange.Item1;
            b = newRange.Item2;

            return NutonMethodDiscret(a, h);
        }

#endregion
    }
}