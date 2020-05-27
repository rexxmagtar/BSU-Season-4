using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;


namespace LabMV2
{
    static class Program
    {
        public static int N = 10;
        public static double epsilon = 1e-10;
        public static double epsilonEquation = 1e-100;

        public static double[,] MainA = new double[,]
        {
            {94, 7, -3, 7, -79, 55, 14, 24, -31, 69}, {-19, 7, 19, -19, 19, -19, 0, 0, 19, -19},
            {23 / (1.0 * 2), 47 / (1.0 * 2), 31 / (1.0 * 2), 47 / (1.0 * 2), -23 / (1.0 * 2), 31 / (1.0 * 2), 0, -4, -39 / (1.0 * 2), 31 / (1.0 * 2)},
            {28, 9, 4, 35, -30, 27, -2, 5, -27, 30},
            {31, 1, -1, 1, -40, 36, -10, 15, -16, 21}, {71 / (1.0 * 2), 15 / (1.0 * 2), -15 / (1.0 * 2), 15 / (1.0 * 2), -119 / (1.0 * 2), 113 / (1.0 * 2), -24, 14, -43 / (1.0 * 2), 23 / (1.0 * 2)},
            {61 / (1.0 * 2), 13 / (1.0 * 2), -13 / (1.0 * 2), 13 / (1.0 * 2), -85 / (1.0 * 2), 61 / (1.0 * 2), -13, 12, -37 / (1.0 * 2), 37 / (1.0 * 2)},
            {-109 / (1.0 * 2), -5 / (1.0 * 2), -3 / (1.0 * 2), -5 / (1.0 * 2), 89 / (1.0 * 2), -63 / (1.0 * 2), -12, 0, 31 / (1.0 * 2), -53 / (1.0 * 2)}, {155, 33, -29, 33, -173, 127, -16, 48, -68, 99},
            {47, 15, -15, 15, -63, 47, -14, 16, -31, 32}
        };

        static void Main(string[] args)
        {

            //if you want test with another N - uncomment. Deafult matrix value is for N = 13.
            //N = 13;
            //MainA[0,0] = 1.0*1.0*(-7 + 15 * N) / 2; MainA[0,1] = 1.0*(1 + N) / 2; MainA[0,2] = 1.0*(7 - N) / 2; MainA[0,3] = 1.0*(1 + N) / 2; MainA[0,4] = 1.0*(11 - 13 * N) / 2; MainA[0,5] = 1.0*(-7 + 9 * N) / 2; MainA[0,6] = N + 1; MainA[0,7] = 2 * 1.0*(N - 1); MainA[0,8] = 1.0*(3 - 5 * N) / 2; MainA[0,9] = 1.0*(-5 + 11 * N) / 2;
            //MainA[1,0] = 1.0*(1 - 3 * N) / 2; MainA[1,1] = 1.0*(1 + N) / 2; MainA[1,2] = 1.0*(-1 + 3 * N) / 2; MainA[1,3] = 1.0*(1 - 3 * N) / 2; MainA[1,4] = 1.0*(-1 + 3 * N) / 2; MainA[1,5] = 1.0*(1 - 3 * N) / 2; MainA[1,6] = 0; MainA[1,7] = 0; MainA[1,8] = 1.0*(-1 + 3 * N) / 2; MainA[1,9] = 1.0*(1 - 3 * N) / 2;
            //MainA[2,0] = -14.5 + 2 * N; MainA[2,1] = -2.5 + 2 * N; MainA[2,2] = 2.5 + N; MainA[2,3] = -2.5 + 2 * N; MainA[2,4] = 14.5 - 2 * N; MainA[2,5] = -10.5 + 2 * N; MainA[2,6] = 0; MainA[2,7] = -4; MainA[2,8] = 6.5 - 2 * N; MainA[2,9] = -10.5 + 2 * N;
            //MainA[3,0] = 1.0*(17 + 3 * N) / 2; MainA[3,1] = 1.0*(5 + N) / 2; MainA[3,2] = 1.0*(-5 + N) / 2; MainA[3,3] = 2.5 * 1.0*(1 + N); MainA[3,4] = -1.5 * 1.0*(7 + N); MainA[3,5] = 1.5 * 1.0*(5 + N); MainA[3,6] = -2; MainA[3,7] = 5; MainA[3,8] = -1.5 * 1.0*(5 + N); MainA[3,9] = 1.5 * 1.0*(7 + N);
            //MainA[4,0] = 1.0*(-55 + 9 * N) / 2; MainA[4,1] = 1.0*(-11 + N) / 2; MainA[4,2] = 1.0*(11 - N) / 2; MainA[4,3] = 1.0*(-11 + N) / 2; MainA[4,4] = 1.0*(63 - 11 * N) / 2; MainA[4,5] = 4.5 * 1.0*(-5 + N); MainA[4,6] = 3 - N; MainA[4,7] = -11 + 2 * N; MainA[4,8] = 1.0*(33 - 5 * N) / 2; MainA[4,9] = 3.5 * 1.0*(-7 + N);
            //MainA[5,0] = -29.5 + 5 * N; MainA[5,1] = -5.5 + N; MainA[5,2] = 5.5 - N; MainA[5,3] = -5.5 + N; MainA[5,4] = 31.5 - 7 * N; MainA[5,5] = -21.5 + 6 * N; MainA[5,6] = 2 - 2 * N; MainA[5,7] = 2 * 1.0*(N - 6); MainA[5,8] = 17.5 - 3 * N; MainA[5,9] = -27.5 + 3 * N;
            //MainA[6,0] = -2 + 2.5 * N; MainA[6,1] =1.0 * N / 2; MainA[6,2] = -1.0*N / 2; MainA[6,3] = 1.0*N / 2; MainA[6,4] = 3 - 3.5 * N; MainA[6,5] = -2 + 2.5 * N; MainA[6,6] = -N; MainA[6,7] = N - 1; MainA[6,8] = 1 - 1.5 * N; MainA[6,9] = -1 + 1.5 * N;
            //MainA[7,0] = -2.5 - 4 * N; MainA[7,1] = -2.5; MainA[7,2] = -1.5; MainA[7,3] = -2.5; MainA[7,4] = 5.5 + 3 * N; MainA[7,5] = -5.5 - 2 * N; MainA[7,6] = 1 - N; MainA[7,7] = 0; MainA[7,8] = 2.5 + N; MainA[7,9] = -0.5 - 2 * N;
            //MainA[8,0] = 5 * 1.0*(5 + 2 * N); MainA[8,1] = 7 + 2 * N; MainA[8,2] = -3 - 2 * N; MainA[8,3] = 7 + 2 * N; MainA[8,4] = -30 - 11 * N; MainA[8,5] = 23 + 8 * N; MainA[8,6] = -3 - N; MainA[8,7] = 3 * 1.0*(3 + N); MainA[8,8] = -4 * 1.0*(4 + N); MainA[8,9] = 21 + 6 * N;
            //MainA[9,0] = 8 + 3 * N; MainA[9,1] = 2 + N; MainA[9,2] = -2 - N; MainA[9,3] = 2 + N; MainA[9,4] = -11 - 4 * N; MainA[9,5] = 8 + 3 * N; MainA[9,6] = -1 - N; MainA[9,7] = 3 + N; MainA[9,8] = -5 - 2 * N; MainA[9,9] = 6 + 2 * N;

            SolveGivenMatrix();

            DoLab();

            Console.ReadKey();
        }

        public static void DoTest()
        {
            //for (int l = 0; l < 100; l++)
            //{


            double[,] A = GetRandomMatrix(10);

            A = new double[,]
            {
                {0, 0, 0},
                {2, 3, 13},
                {2, 1, 4}
            };


            //PrintMatrix(A);
            //Console.WriteLine("---------------------");
            //PrintMatrix(GetInverseMatrix(A));

            //Console.WriteLine("A eigen values= \n");

            var qrresult2 = GetEigenNumbersQR(A);

            Console.WriteLine("True qr Max eigenvalue:");

            Console.WriteLine(GetMax(qrresult2).String());

            PrintVector(qrresult2);
            var result = PowerMethod((A));

            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine("Max: \n");
                Console.WriteLine(result[i].Item1.String());
                Console.WriteLine("Vector: \n");
                PrintVector(result[i].Item2);
                Console.WriteLine("Norm = " + GetEigenNorm(A, result[i].Item1, result[i].Item2));

            }

            result = PowerMethod((A), false);

            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine("Min: \n");
                Console.WriteLine(result[i].Item1.String());
                Console.WriteLine("Vector: \n");
                PrintVector(result[i].Item2);
                Console.WriteLine("Norm = " + GetEigenNorm(A, result[i].Item1, result[i].Item2));

            }

            //var eigens = GetEigenNumbersQR(GetInverseMatrix(A));


            //Console.WriteLine("Eigen values");

            //for (int i = 0; i < eigens.Length; i++)
            //{
            //    Console.Write($" {eigens[i].String()} |");

            //}
            //}
        }

        public static void SolveWraper(double[,] A)
        {

            StreamWriter streamWriter = new StreamWriter("MatrixSolve.txt");


            for (int i = 0; i < A.GetLength(0); i++)
            {

                for (int j = 0; j < A.GetLength(1); j++)
                {
                    streamWriter.Write($" {A[i, j]}|");
                }
                streamWriter.WriteLine("");
            }

            streamWriter.WriteLine("------------------------------------\n");

            Stopwatch timer = new Stopwatch();

            timer.Start();
            var resultQR = GetEigenNumbersQR(A);
            timer.Stop();

            Console.WriteLine("True qr Max eigenvalue:");
            Console.WriteLine(GetMax(resultQR).String());

            double[,] inverseA = GetInverseMatrix(A);

            Console.WriteLine("True qr Max eigenvalue for inverse:");
            Console.WriteLine((GetMax(GetEigenNumbersQR(inverseA))).String());

            Console.WriteLine("\n Invers matrix eingvalues \n");
            PrintVector(GetEigenNumbersQR(inverseA));

            Console.WriteLine("True qr Min eigenvalue:");

            Console.WriteLine(GetMin(resultQR).String());

            Console.WriteLine("All eigenValues = ");
            PrintVector(resultQR);

            streamWriter.WriteLine("Eigenvalues found using QR");

            for (int i = 0; i < resultQR.Length; i++)
            {
                streamWriter.WriteLine($"Value {i} = {resultQR[i].String()}");
            }

            streamWriter.WriteLine("Time taken: " + timer.ElapsedMilliseconds + " ms");

            streamWriter.WriteLine("\n Eigenvalues found by power method: ");

            Console.WriteLine("Max Min Eigenvalues found using power method: ");

            timer.Reset();
            timer.Start();

            var result = PowerMethod(A);

            timer.Stop();

            streamWriter.WriteLine("Max Eigenvalues: ");

            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine("Max: \n");
                Console.WriteLine(result[i].Item1.String());
                Console.WriteLine("Vector: \n");
                PrintVector(result[i].Item2);
                Console.WriteLine("Norm = " + GetEigenNorm(A, result[i].Item1, result[i].Item2));

                streamWriter.WriteLine($"Value {i} = {result[i].Item1.String()}");

                streamWriter.Write($"Vector {i} ");
                for (int j = 0; j < result[i].Item2.Length; j++)
                {
                    streamWriter.Write($" {result[i].Item2[j].String()}; ");
                }

                streamWriter.WriteLine("");

                streamWriter.WriteLine("Norm = " + GetEigenNorm(A, result[i].Item1, result[i].Item2) + "\n");


            }

            streamWriter.WriteLine("Time taken: " + timer.ElapsedMilliseconds + " ms");

            timer.Reset();

            timer.Start();

            result = PowerMethod(A, false);

            timer.Stop();

            streamWriter.WriteLine("Min Eigenvalues: ");

            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine("Min: \n");
                Console.WriteLine(result[i].Item1.String());
                Console.WriteLine("Vector: \n");
                PrintVector(result[i].Item2);
                Console.WriteLine("Norm = " + GetEigenNorm(A, 1 / result[i].Item1, result[i].Item2));

                streamWriter.WriteLine($"Value {i} = {result[i].Item1.String()}");

                streamWriter.Write($"Vector {i} ");
                for (int j = 0; j < result[i].Item2.Length; j++)
                {
                    streamWriter.Write($" {result[i].Item2[j].String()}; ");
                }

                streamWriter.WriteLine("");

                streamWriter.WriteLine("Norm = " + GetEigenNorm(A, result[i].Item1, result[i].Item2) + "\n");

            }

            streamWriter.WriteLine("Time taken: " + timer.ElapsedMilliseconds + " ms");

            streamWriter.Flush();
        }

        public static void SolveGivenMatrix()
        {
            double[,] A = MainA;

            PrintMatrix(A);

            SolveWraper(A);

        }

        public static void DoLab()
        {

            //for (int l = 0; l < 100; l++)
            //{
            //    Console.Clear();

            double[,] A;

            List<double> normsFound = new List<double>();
            List<long> timeTakenForPowerMethod = new List<long>();
            List<long> timeTakenForQRMethod = new List<long>();

            int matrixCount = 100;

            for (int i = 0; i < matrixCount; i++)
            {
                Console.Clear();
                Console.WriteLine("Calculating... {0}% ", 100.0f * i / matrixCount);
                A = GetRandomMatrix(10);


                Stopwatch timer = new Stopwatch();

                timer.Start();

                var result = PowerMethod(A);

                timer.Stop();

                timeTakenForPowerMethod.Add(timer.ElapsedMilliseconds);

                for (int j = 0; j < result.Length; j++)
                {

                    normsFound.Add(GetEigenNorm(A, result[j].Item1, result[j].Item2));
                }

                timer.Reset();


                timer.Start();

                var resultMin = PowerMethod(A, false);

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

            StreamWriter streamWriter = new StreamWriter("LabResults.txt");

            streamWriter.WriteLine($@"Average norm = {normsFound.Average()}");

            streamWriter.WriteLine($@"Min time to find value using power method  = {timeTakenForPowerMethod.Min()} ms");

            streamWriter.WriteLine($@"Max time to find value using power method  = {timeTakenForPowerMethod.Max()} ms");

            streamWriter.WriteLine($@"Average time to find value using power method  = {timeTakenForPowerMethod.Average()} ms");

            streamWriter.WriteLine($@"Min time to find all values using QR method  = {timeTakenForQRMethod.Min()} ms");

            streamWriter.WriteLine($@"Max time to find all values using QR method  = {timeTakenForQRMethod.Max()} ms");

            streamWriter.WriteLine($@"Average time to find all values using QR method  = {timeTakenForQRMethod.Average()} ms");

            streamWriter.Flush();


            Console.WriteLine($@"Average norm = {normsFound.Average()}");

            Console.WriteLine($@"Min time to find value using power method  = {timeTakenForPowerMethod.Min()} ms");

            Console.WriteLine($@"Max time to find value using power method  = {timeTakenForPowerMethod.Max()} ms");

            Console.WriteLine($@"Average time to find value using power method  = {timeTakenForPowerMethod.Average()} ms");

            Console.WriteLine($@"Min time to find all values using QR method  = {timeTakenForQRMethod.Min()} ms");

            Console.WriteLine($@"Max time to find all values using QR method  = {timeTakenForQRMethod.Max()} ms");

            Console.WriteLine($@"Average time to find all values using QR method  = {timeTakenForQRMethod.Average()} ms");


            double x;

            double a;
            double b;
            double h;

            int iterationCount;

            Tuple<double, double> newRange;


            a = 0.08;
            b = 0.37;

            streamWriter.WriteLine("\n Equations solving: \n");

            streamWriter.WriteLine("x1 calculation:");
            Console.WriteLine("x1 calculation:");

            newRange = besectionMethod(a, b, out iterationCount);

            a = newRange.Item1;
            b = newRange.Item2;

            streamWriter.WriteLine($"New range after bisection method applied : [{a}; {b}] ,number of iterations: {iterationCount} ");


            x = NutonMethod(a, out iterationCount);

            streamWriter.WriteLine($"Classic Nuton's method: x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine($"Classic Nuton's method: x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine("F(x) = " + Func(x));

            h = 0.1;

            x = NutonMethodDiscret(a, h, out iterationCount);

            streamWriter.WriteLine($"Discret Nuton's method( h = {h}) : x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine($"Discret Nuton's method( h = {h}) : x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine("F(x) = " + Func(x));


            a = 0.37;
            b = 1.38;


            streamWriter.WriteLine("x2 calculation:");
            Console.WriteLine("x2 calculation:");

            newRange = besectionMethod(a, b, out iterationCount);

            a = newRange.Item1;
            b = newRange.Item2;

            streamWriter.WriteLine($"New range after bisection method applied : [{a}; {b}] ,number of iterations: {iterationCount} ");
            Console.WriteLine($"New range after bisection method applied : [{a}; {b}] ,number of iterations: {iterationCount} ");
            Console.WriteLine("F(x) = " + Func(x));

            x = NutonMethod(a, out iterationCount);

            streamWriter.WriteLine($"Classic Nuton's method: x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine($"Classic Nuton's method: x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine("F(x) = " + Func(x));

            h = 0.0001;

            x = NutonMethodDiscret(a, h, out iterationCount);

            streamWriter.WriteLine($"Discret Nuton's method( h = {h}) : x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine($"Discret Nuton's method( h = {h}) : x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine("F(x) = " + Func(x));

            a = 1.38;
            b = 1.9;


            streamWriter.WriteLine("x3 calculation:");
            Console.WriteLine("x3 calculation:");

            newRange = besectionMethod(a, b, out iterationCount);

            a = newRange.Item1;
            b = newRange.Item2;

            streamWriter.WriteLine($"New range after bisection method applied : [{a}; {b}] ,number of iterations: {iterationCount} ");
            Console.WriteLine($"New range after bisection method applied : [{a}; {b}] ,number of iterations: {iterationCount} ");

            x = NutonMethod(a, out iterationCount);
            streamWriter.WriteLine($"Classic Nuton's method: x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine($"Classic Nuton's method: x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine("F(x) = " + Func(x));

            h = 0.00001;

            x = NutonMethodDiscret(a, h, out iterationCount);

            streamWriter.WriteLine($"Discret Nuton's method( h = {h}) : x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine($"Discret Nuton's method( h = {h}) : x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine("F(x) = " + Func(x));

            streamWriter.Flush();

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

            return GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, eigenVector)),
                VecotorWithScalarMultiplication(eigenVector, eigenValue))).Magnitude;

        }
        public static string String(this Complex numComplex)
        {
            string outPut = "";

            if (numComplex.Magnitude == 0)
            {
                return "0";
            }

            if (Math.Abs(numComplex.Real) > double.Epsilon)
            {
                outPut += numComplex.Real;
            }

            if (Math.Abs(numComplex.Imaginary) > double.Epsilon)
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


        #region CasesPowerMethod


        public static bool HasZeroRow(double[,] A)
        {
            for (int i = A.GetLength(0) - 1; i >= 0; i--)
            {
                bool hasNotZeroElement = false;

                for (int j = 0; j < A.GetLength(0); j++)
                {
                    //not zero
                    if (Math.Abs(A[i, j]) > epsilon)
                    {
                        hasNotZeroElement = true;
                        break;
                    }
                }

                if (!hasNotZeroElement)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Solves eigen values partial problem. 
        /// </summary>
        /// <param name="A_"></param>
        /// <param name="finMax">if true gets max eigenvalue, over vise gets min eigenvalue</param>
        /// <returns></returns>
        public static Tuple<Complex, Complex[]>[] PowerMethod(double[,] A_, bool findMax = true)
        {
            double[,] L = new double[1, 1];
            double[,] U = new double[1, 1];
            int[] P = new int[1];

            if (findMax == false)
            {
                GetLUP(ref L, ref U, ref P, A_);

                //Adrenerated matrix detected
                if (HasZeroRow(U))
                {
                    Console.WriteLine("Adjenerated matrix detected!");

                    var result = new Tuple<Complex, Complex[]>[1];

                    Complex[] zeroes = new Complex[A_.GetLength(0)];

                    for (int i = 0; i < zeroes.Length; i++)
                    {
                        zeroes[i] = 0;
                    }

                    var vector = SolveLUPForm(L, U, P, zeroes);

                    result[0] = new Tuple<Complex, Complex[]>(0, (vector));

                    return result;
                }
                //LuDecomposition decomposition=new LuDecomposition(A_);

                //L = decomposition.LowerTriangularFactor;
                //U = decomposition.UpperTriangularFactor;
                //P = decomposition.PivotPermutationVector;

                //A_ = GetInverseMatrix(A_);
            }

            Complex[,] A = ToComplex(A_);

            // initial approximation
            Complex[] u = new Complex[A.GetLength(0)];
            u[0] = 0.01;


            List<Complex[]> vNorm = new List<Complex[]>();
            List<Complex[]> uNorm = new List<Complex[]>();

            uNorm.Add(u);

            int iterationCount = 0;


            vNorm.Add(new Complex[2]);

            Complex number1 = new Complex();
            Complex number2 = new Complex();


            Complex[] vector1 = new Complex[2];
            Complex[] vector2 = new Complex[2];

            while (true)
            {
                Complex[] v = MatrixMultiplication(A, u);

                u = v;

                //Doing next step

                if (findMax == false)
                {
                    Complex[] newV = (SolveLUPForm(L, U, P, (uNorm[uNorm.Count - 1])));

                    vNorm.Add(newV);

                }
                else
                {
                    vNorm.Add(MatrixMultiplication(A, uNorm[uNorm.Count - 1]));
                }

                uNorm.Add(VectorWithScalarDivison(vNorm[vNorm.Count - 1], GetMax(vNorm[vNorm.Count - 1])));


                iterationCount++;

                //Check case 1 (one max absolute eigenvalue)

                int indexToTake = GetMaxIndex(vNorm[vNorm.Count - 1]);


                number1 = vNorm[vNorm.Count - 1][indexToTake] / uNorm[vNorm.Count - 2][indexToTake];
                vector1 = uNorm[vNorm.Count - 2];

                if (!findMax && number1 != 0)
                {
                    number1 = 1 / number1;
                }

                //Console.WriteLine(GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, vector1)), VecotorWithScalarMultiplication(vector1, number1))).Magnitude);

                if (GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, vector1)), VecotorWithScalarMultiplication(vector1, number1))).Magnitude < epsilon)
                {
                    Console.WriteLine("First case method could succeed! Iterations done: " + iterationCount);
                    Tuple<Complex, Complex[]>[] result = new Tuple<Complex, Complex[]>[1];

                    result[0] = new Tuple<Complex, Complex[]>(number1, vector1);

                    return result;
                }

                //Check case 3 (two opposite but absolutely equal eigenvalues)
                if (iterationCount > 3)
                {
                    int k = iterationCount - 3;

                    indexToTake = GetMaxIndex(vNorm[k + 2]);

                    double vk2 = (vNorm[k + 2])[indexToTake].Real;
                    double uk = (uNorm[k])[indexToTake].Real;


                    number1 = Math.Sqrt((vk2 * GetMax(vNorm[k + 1]).Real) / uk);
                    vector1 = VectorSum(vNorm[k + 1], VecotorWithScalarMultiplication(uNorm[k], number1));
                    vector2 = VectorSubtraction(vNorm[k + 1], VecotorWithScalarMultiplication(uNorm[k], number1));

                    if (!findMax && number1 != 0)
                    {
                        number1 = 1 / number1;
                    }

                    //Console.WriteLine(GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, vector1)), VecotorWithScalarMultiplication(vector1, number1))).Magnitude);

                    if (GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, vector1)), VecotorWithScalarMultiplication(vector1, number1))).Magnitude < epsilon)
                    {
                        //vector2 = VectorSubtraction(vNorm[k + 1], VecotorWithScalarMultiplication(uNorm[k], number1));

                        Console.WriteLine("Second case method could succeed! Iterations done: " + iterationCount);
                        Tuple<Complex, Complex[]>[] result = new Tuple<Complex, Complex[]>[2];


                        result[0] = new Tuple<Complex, Complex[]>(number1, vector1);
                        result[1] = new Tuple<Complex, Complex[]>(-number1, vector2);

                        return result;
                    }

                }

                //Check case 4 ( complex pair)
                if (iterationCount > 5)
                {
                    int k = iterationCount - 3;

                    indexToTake = GetMaxIndex(vNorm[k + 2]);

                    double vk2 = (vNorm[k + 2])[indexToTake].Real;
                    double vk1 = (vNorm[k + 1])[indexToTake].Real;
                    double vk = (vNorm[k])[indexToTake].Real;
                    double ukm1 = (uNorm[k - 1])[indexToTake].Real;
                    double uk = (uNorm[k])[indexToTake].Real;

                    double r = Math.Sqrt(((vk * vk2 * GetMax(vNorm[k + 1]).Real - vk1 * vk1 * GetMax(vNorm[k]).Real)
                                          / (ukm1 * vk1 - uk * uk * GetMax(vNorm[k]).Real)));

                    double cosPhi = Math.Clamp((vk2 * GetMax(vNorm[k + 1]).Real + r * r * uk) / (2 * r * vk1), -1, 1);
                    double sinPhi = Math.Sqrt(1 - cosPhi * cosPhi);

                    number1 = new Complex(r * cosPhi, r * sinPhi);
                    number2 = new Complex(r * cosPhi, -r * sinPhi);

                    vector1 = VectorSubtraction(vNorm[k + 1], VecotorWithScalarMultiplication(uNorm[k], number2));

                    vector2 = VectorSubtraction(vNorm[k + 1], VecotorWithScalarMultiplication(uNorm[k], number1));

                    //Console.WriteLine(GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, vector1)), VecotorWithScalarMultiplication(vector1, number1))).Magnitude);

                    if (!findMax)
                    {
                        number1 = 1 / number1;
                        number2 = 1 / number2;
                    }

                    //Console.WriteLine(GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, vector1)), VecotorWithScalarMultiplication(vector1, number1))).Magnitude);

                    if (GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, vector1)), VecotorWithScalarMultiplication(vector1, number1))).Magnitude < epsilon)
                    {
                        Tuple<Complex, Complex[]>[] result = new Tuple<Complex, Complex[]>[2];

                        result[0] = new Tuple<Complex, Complex[]>(number1, vector1);
                        result[1] = new Tuple<Complex, Complex[]>(number2, vector2);

                        Console.WriteLine("Third case method could succeed! Iterations done: " + iterationCount);
                        return result;
                    }

                    //if (iterationCount  %100==0)
                    //{
                    //    Console.WriteLine("Fourth case method could not succeed! Iterations done: " + iterationCount);
                    //}
                }
            }

        }

        #endregion
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

        public static double ScalarMultiplication(double[] A, double[] B)
        {
            double sum = 0;

            for (int i = 0; i < A.GetLength(0); i++)
            {
                sum += A[i] * B[i];
            }

            return sum;
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

        public static int GetMaxIndex(Complex[] complexes)
        {
            Complex max = Complex.Zero;

            int index = 0;
            for (int i = 0; i < complexes.Length; i++)
            {
                if (max.Magnitude < complexes[i].Magnitude)
                {
                    max = complexes[i];
                    index = i;
                }
            }

            return index;
        }

        public static Complex GetMin(Complex[] complexes)
        {
            Complex min = Complex.Infinity;

            for (int i = 0; i < complexes.Length; i++)
            {
                if (min.Magnitude > complexes[i].Magnitude)
                {
                    min = complexes[i];
                }
            }

            return min;
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

        public static double[,] GetRandomMatrix(int size = 10)
        {
            double[,] A = new double[size, size];

            Random random = new Random();

            double leftBorder = -Math.Pow(2, 2.5);
            double rightBorder = Math.Pow(2, 2.5);

            double borderLength = rightBorder - leftBorder;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
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
            var Aconverted = MatrixToMatrix(A);

            List<Complex> result = new List<Complex>();

            double[] prevLambda = new double[A.GetLength(0)];

            for (int i = 0; i < prevLambda.Length; i++)
            {
                prevLambda[i] = A[i, i];
            }

            while (true)
            {

                GetQR(Aconverted, out double[][] q, out double[][] r);


                Aconverted = MatrixMultiplication(r, q);

                Complex[] currentValues = new Complex[A.GetLength(0)];

                for (int i = 0; i < Aconverted.GetLength(0); i++)
                {
                    if (i < Aconverted.GetLength(0) - 1 && Math.Abs(Aconverted[i + 1][i]) > epsilon)
                    {
                        double D = (Aconverted[i][i] + Aconverted[i + 1][i + 1]) * (Aconverted[i][i] + Aconverted[i + 1][i + 1]) - 4 * ((Aconverted[i][i] * Aconverted[i + 1][i + 1]) - (Aconverted[i + 1][i] * Aconverted[i][i + 1]));

                        Complex x1 = (-(Aconverted[i][i] + Aconverted[i + 1][i + 1]) + Complex.Sqrt(D)) / 2;
                        Complex x2 = (-(Aconverted[i][i] + Aconverted[i + 1][i + 1]) - Complex.Sqrt(D)) / 2;

                        currentValues[i] = x1;
                        currentValues[i + 1] = x2;

                        i++;
                    }
                    else
                    {
                        currentValues[i] = Aconverted[i][i];
                    }
                }

                double maxDiff = double.MinValue;

                for (int i = 0; i < prevLambda.Length; i++)
                {
                    if (maxDiff < Math.Abs(prevLambda[i] - currentValues[i].Magnitude))
                    {
                        maxDiff = Math.Abs(prevLambda[i] - currentValues[i].Magnitude);
                    }
                }


                //Console.WriteLine(maxDiff);

                if (maxDiff < epsilon * 100)
                {
                    break;
                }

                for (int i = 0; i < prevLambda.Length; i++)
                {
                    prevLambda[i] = currentValues[i].Magnitude;
                }
            }

            //PrintMatrix(Aconverted);
            for (int i = 0; i < Aconverted.GetLength(0); i++)
            {
                if (i < Aconverted.GetLength(0) - 1 && Math.Abs(Aconverted[i + 1][i]) > epsilon)
                {
                    double D = (Aconverted[i][i] + Aconverted[i + 1][i + 1]) * (Aconverted[i][i] + Aconverted[i + 1][i + 1]) - 4 * ((Aconverted[i][i] * Aconverted[i + 1][i + 1]) - (Aconverted[i + 1][i] * Aconverted[i][i + 1]));

                    Complex x1 = (-(Aconverted[i][i] + Aconverted[i + 1][i + 1]) + Complex.Sqrt(D)) / 2;
                    Complex x2 = (-(Aconverted[i][i] + Aconverted[i + 1][i + 1]) - Complex.Sqrt(D)) / 2;

                    result.Add(x1);
                    result.Add(x2);

                    i++;
                }
                else
                {
                    result.Add(new Complex(Aconverted[i][i], 0));
                }
            }


            return result.ToArray();
        }


        public static Complex[] SolveLUPForm(double[,] L, double[,] U, int[] P, Complex[] B)
        {
            return SolveUpperTriangularMatrix(U, SolveLowerTriangularMatrix(L, GetSwapedMatrix(B, P)));
        }

        public static double[][] MatrixToMatrix(double[,] A)
        {

            double[][] result = new double[A.GetLength(0)][];

            for (int i = 0; i < result.GetLength(0); i++)
            {
                result[i] = new double[A.GetLength(0)];

                for (int j = 0; j < A.GetLength(0); j++)
                {
                    result[i][j] = A[i, j];
                }
            }

            return result;
        }

        public static double[,] MatrixToMatrix(double[][] A)
        {

            double[,] result = new double[A.GetLength(0), A[0].Length];

            for (int i = 0; i < result.GetLength(0); i++)
            {


                for (int j = 0; j < A.GetLength(0); j++)
                {
                    result[i, j] = A[i][j];
                }
            }

            return result;
        }

        public static Complex[] GetSwapedMatrix(Complex[] A, int[] P)
        {
            Complex[] answer = (Complex[])A.Clone();

            for (int i = 0; i < A.GetLength(0); i++)
            {
                answer[i] = A[P[i]];

            }

            return answer;
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

            //return A.Inverse();
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


        static void GetQR(double[][] A,
            out double[][] Q, out double[][] R)
        {

            double[][] a = MatTranspose(A);
            double[][] u = MatDuplicate(a);
            int rows = a.Length; // of the transpose
            int cols = a[0].Length;

            Q = CreateMatrix(cols, rows);
            R = CreateMatrix(cols, rows);

            for (int j = 0; j < cols; ++j)
                u[0][j] = a[0][j];

            double[] accum = new double[cols];

            for (int i = 1; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {

                    accum = new double[cols];
                    for (int t = 0; t < i; ++t)
                    {
                        double[] proj = GetProjection(u[t], a[i]);
                        for (int k = 0; k < cols; ++k)
                            accum[k] += proj[k];
                    }
                }
                for (int k = 0; k < cols; ++k)
                    u[i][k] = a[i][k] - accum[k];
            }

            for (int i = 0; i < rows; ++i)
            {
                double norm = GetMagnitude(u[i]);
                if (norm == 0)
                {
                    norm = 1;
                }
                for (int j = 0; j < cols; ++j)
                    u[i][j] = u[i][j] / norm;
            }

            double[][] q = MatTranspose(u);
            for (int i = 0; i < q.Length; ++i)
                for (int j = 0; j < q[0].Length; ++j)
                    Q[i][j] = q[i][j];

            double[][] r = MatrixMultiplication(u, A);
            for (int i = 0; i < r.Length; ++i)
                for (int j = 0; j < r[0].Length; ++j)
                    R[i][j] = r[i][j];

        } // 

        static double[][] MatDuplicate(double[][] m)
        {
            int rows = m.Length;
            int cols = m[0].Length;
            double[][] result = CreateMatrix(rows, cols);
            for (int i = 0; i < rows; ++i)
                for (int j = 0; j < cols; ++j)
                    result[i][j] = m[i][j];
            return result;
        }


        static double[] GetProjection(double[] u, double[] a)
        {
            // proj(u, a) = (inner(u,a) / inner(u, u)) * u
            // u cannot be all 0s
            int n = u.Length;
            double dotUA = ScalarMultiplication(u, a);
            double dotUU = ScalarMultiplication(u, u);
            double[] result = new double[n];
            for (int i = 0; i < n; ++i)
                result[i] = (dotUA / dotUU) * u[i];
            return result;
        }

        static double[][] CreateMatrix(int rows, int cols)
        {
            double[][] result = new double[rows][];
            for (int i = 0; i < rows; ++i)
                result[i] = new double[cols];
            return result;
        }

        static double[][] MatrixMultiplication(double[][] matA,
            double[][] matB)
        {
            int aRows = matA.Length;
            int aCols = matA[0].Length;
            int bRows = matB.Length;
            int bCols = matB[0].Length;
            if (aCols != bRows)
                throw new Exception("Non-conformable matrices");

            double[][] result = CreateMatrix(aRows, bCols);

            for (int i = 0; i < aRows; ++i)
                for (int j = 0; j < bCols; ++j)
                    for (int k = 0; k < aCols; ++k)
                        result[i][j] += matA[i][k] * matB[k][j];

            return result;
        }

        static double[][] MatTranspose(double[][] m)
        {
            int nr = m.Length;
            int nc = m[0].Length;
            double[][] result = CreateMatrix(nc, nr); // note
            for (int i = 0; i < nr; ++i)
                for (int j = 0; j < nc; ++j)
                    result[j][i] = m[i][j];
            return result;
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

        public static Complex[] SolveUpperTriangularMatrix(double[,] A, double[] B)
        {
            int size = A.GetLength(0);

            Complex[] X = new Complex[size];

            for (int i = size - 1; i >= 0; i--)
            {
                double sum = 0;

                if (Math.Abs(A[i, i]) < epsilon)
                {
                    X[i] = 0;
                    continue;
                }

                for (int j = size - 1; j > i; j--)
                {
                    sum += X[j].Real * A[i, j];
                }
                sum = B[i] - sum;
                X[i] = sum / A[i, i];
            }
            return X;
        }

        public static double[] SolveLowerTriangularMatrix(double[,] A, Complex[] B)
        {
            int size = A.GetLength(0);

            double[] X = new double[size];

            for (int i = 0; i < size; i++)
            {
                double sum = 0;
                for (int j = 0; j < i; j++)
                {
                    sum += X[j] * A[i, j];
                }
                sum = B[i].Real - sum;
                X[i] = sum / A[i, i];
            }
            return X;
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


        public static void KillColumn(double[,] A, double[] B, int mainRowIndex, int columnIndex, double[,] L = null, bool whole = true)
        {
            int size = A.GetLength(0);

            double leadValue = A[mainRowIndex, columnIndex];


            int i;

            if (whole)
            {
                i = 0;
            }
            else
            {
                i = mainRowIndex + 1;
            }

            for (i = i; i < size; i++)
            {
                if (i == mainRowIndex)
                {
                    continue;
                }

                double coef = -A[i, columnIndex] * 1 / leadValue;

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

        public static void SwapRows(ref double[,] A, int rowA, int rowB)
        {
            int size = A.GetLength(0);

            double[] temp = new double[size];

            for (int i = 0; i < size; i++)
            {
                temp[i] = A[rowA, i];
            }

            for (int i = 0; i < size; i++)
            {
                A[rowA, i] = A[rowB, i];
            }

            for (int i = 0; i < size; i++)
            {
                A[rowB, i] = temp[i];
            }
        }

        public static void GetLUP(ref double[,] L, ref double[,] U, ref int[] P, double[,] A)
        {
            int size = A.GetLength(0);
            A = (double[,])A.Clone();
            P = new int[size];
            L = new double[size, size];

            for (int i = 0; i < size; i++)
            {
                L[i, i] = 1;
            }

            for (int i = 0; i < size; i++)
            {
                P[i] = i;
            }

            for (int i = 0; i < size; i++)
            {
                double max = double.MinValue;
                int maxIndex = 0;

                for (int j = i; j < size; j++)
                {
                    if (max < Math.Abs(A[j, i]))
                    {
                        max = Math.Abs(A[j, i]);
                        maxIndex = j;
                    }
                }


                SwapRows(ref A, i, maxIndex);
                SwapInt(P, i, maxIndex);

                for (int j = 0; j < i; j++)
                {
                    double temp = L[i, j];
                    L[i, j] = L[maxIndex, j];
                    L[maxIndex, j] = temp;
                }

                KillColumn(A, new double[size], i, i, L, false);
            }

            U = (double[,])A.Clone();
        }

        public static void SwapInt(int[] array, int indexA, int indexB)
        {
            int temp = array[indexA];
            array[indexA] = array[indexB];
            array[indexB] = temp;
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
            return -2 * Math.Sin(2 * x) / Math.Pow(Math.Cos(Math.Cos(2 * x)), 2) + 1 / x;
        }


        public static Tuple<double, double> besectionMethod(double a, double b, out int iterationCount)
        {
            double x;

            iterationCount = 0;

            while (Math.Abs(b - a) > 0.00001)
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

                iterationCount++;
            }

            Console.WriteLine($@"new range =[ {a} {b} ] ");


            return new Tuple<double, double>(a, b);
        }

        public static double NutonMethod(double startX, out int itCount)
        {
            double x = startX;

            Console.WriteLine("epsilonEquation = " + epsilonEquation);


            itCount = 1;
            while (true)
            {
                double newX = x - Func(x) / FuncP(x);

                if (Math.Abs(x - newX) < epsilonEquation)
                {
                    break;
                }

                itCount++;
                x = newX;
            }

            Console.WriteLine($@"Classic method exited in {itCount} iterations");

            return x;

        }

        public static double NutonMethodDiscret(double startX, double h, out int itCount)
        {
            double x = startX;

            itCount = 1;
            while (true)
            {
                double newX = x - ((Func(x) * h) / (Func(x + h) - Func(x)));

                if (Math.Abs(x - newX) < epsilonEquation)
                {
                    break;
                }
                itCount++;
                x = newX;
            }

            Console.WriteLine($@"Discrete method exited in {itCount} iterations");

            return x;

        }

        public static void GetStatisticForNutonOcverage()
        {
            StreamWriter writer = new StreamWriter("NutonSolveStats.txt");

            List<Tuple<int, double>> classicNutomCoverage = new List<Tuple<int, double>>();

            List<Tuple<int, double>> discreteNutomCoverage1 = new List<Tuple<int, double>>();
            List<Tuple<int, double>> discreteNutomCoverage2 = new List<Tuple<int, double>>();
            List<Tuple<int, double>> discreteNutomCoverage3 = new List<Tuple<int, double>>();

            double x;

            double b;
            double h;

            int iterationCount;


            var range = besectionMethod(0, 1, out int number);

            b = range.Item1;
            //Classic Nuton
            x = b;

            for (int i = 0; i < 30; i++)
            {
                double newX = x - Func(x) / FuncP(x);

                classicNutomCoverage.Add(new Tuple<int, double>(i + 1, Math.Abs(x - newX)));

                x = newX;
            }

            writer.Write("Classic Nuton: Errors [ ");
            foreach (var info in classicNutomCoverage)
            {
                writer.Write(info.Item2 + ", ");
            }

            writer.WriteLine("]");
            //Discrete Nuton


            x = b;

            h = 0.1;

            for (int i = 0; i < 30; i++)
            {
                double newX = x - ((Func(x) * h) / (Func(x + h) - Func(x)));

                discreteNutomCoverage1.Add(new Tuple<int, double>(i + 1, Math.Abs(x - newX)));

                x = newX;
            }

            writer.Write($"Discret Nuton h = {h} : Errors [ ");
            foreach (var info in discreteNutomCoverage1)
            {
                writer.Write(info.Item2 + ", ");
            }

            writer.WriteLine("]");

            x = b;

            h = 0.001;

            for (int i = 0; i < 30; i++)
            {
                double newX = x - ((Func(x) * h) / (Func(x + h) - Func(x)));

                discreteNutomCoverage2.Add(new Tuple<int, double>(i + 1, Math.Abs(x - newX)));

                x = newX;
            }

            writer.Write($"Discret Nuton h = {h} : Errors [ ");
            foreach (var info in discreteNutomCoverage2)
            {
                writer.Write(info.Item2 + ", ");
            }

            writer.WriteLine("]");


            x = b;

            h = 0.0001;

            for (int i = 0; i < 30; i++)
            {
                double newX = x - ((Func(x) * h) / (Func(x + h) - Func(x)));

                discreteNutomCoverage3.Add(new Tuple<int, double>(i + 1, Math.Abs(x - newX)));

                x = newX;
            }

            writer.Write($"Discret Nuton h = {h} : Errors [ ");
            foreach (var info in discreteNutomCoverage3)
            {
                writer.Write(info.Item2 + ", ");
            }

            writer.WriteLine("]");

            writer.Flush();

        }

        #endregion
    }
}