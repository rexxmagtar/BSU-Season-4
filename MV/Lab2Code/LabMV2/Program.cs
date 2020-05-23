using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using Accord.Math.Decompositions;


namespace LabMV2
{
    static class Program
    {
        public static int N = 10;
        public static double epsilon = 1e-8;
        public static double epsilonEquation = 10e-10;
        public static double[,] MainA = new double[10,10]
        {
            {94, 7, -3, 7, -79, 55, 14, 24, -31, 69}, {-19, 7, 19, -19, 19, -19, 0, 0, 19, -19}, {23 / (1.0 * 2), 47 / (1.0 * 2), 31 / (1.0 * 2), 47 / (1.0 * 2), -23 / (1.0 * 2), 31 / (1.0 * 2), 0, -4, -39 / (1.0 * 2), 31 / (1.0 * 2)},
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
            
            GetStatisticForNutonOcverage();

            SolveGivenMatrix();

            DoLab();

            Console.ReadKey();
        }

        public static void DoTest()
        {
            for (int l = 0; l < 100; l++)
            {


                double[,] A = GetRandomMatrix(10);


                //PrintMatrix(A);
                //Console.WriteLine("---------------------");
                //PrintMatrix(GetInverseMatrix(A));

                //Console.WriteLine("A eigen values= \n");

                Console.WriteLine("True qr Max eigenvalue:");
                Console.WriteLine(GetMax(GetEigenNumbersQR(A)).String());

                var result = SolveGetMaxAbsoluteEigenValue((A));

                for (int i = 0; i < result.Length; i++)
                {
                    Console.WriteLine("Max: \n");
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
            }
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
            var resultQR = GetEigenNumbersQR(A);

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


            Console.WriteLine("Max Min Eigenvalues found using power method: ");

            var result = SolveGetMaxAbsoluteEigenValue((A));

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

            Console.WriteLine("Solving for inverse");

            result = SolveGetMaxAbsoluteEigenValue(inverseA);

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

            for (int i = 0; i < 100; i++)
            {
                Console.Clear();
                Console.WriteLine("Calculating... {0}% ", i);
                A = GetRandomMatrix(10);


                Stopwatch timer = new Stopwatch();

                timer.Start();

                var result = SolveGetMaxAbsoluteEigenValue(A);

                timer.Stop();

                timeTakenForPowerMethod.Add(timer.ElapsedMilliseconds);

                for (int j = 0; j < result.Length; j++)
                {

                    normsFound.Add(GetEigenNorm(A, result[j].Item1, result[j].Item2));
                }

                timer.Reset();

                timer.Start();

                var resultMin = SolveGetMaxAbsoluteEigenValue(GetInverseMatrix(A));

                timer.Stop();

                timeTakenForPowerMethod.Add(timer.ElapsedMilliseconds);

                for (int j = 0; j < resultMin.Length; j++)
                {

                    normsFound.Add(GetEigenNorm(GetInverseMatrix(A), resultMin[j].Item1, resultMin[j].Item2));
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


                a = 0;
                b = 1;
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

            h = 4;

            x = NutonMethodDiscret(a, h, out iterationCount);

            streamWriter.WriteLine($"Discret Nuton's method( h = {h}) : x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine($"Discret Nuton's method( h = {h}) : x = {x}, number of iterations: {iterationCount} ");


            a = 1;
            b = 1.5;


            streamWriter.WriteLine("x2 calculation:");
            Console.WriteLine("x2 calculation:");

            newRange = besectionMethod(a, b, out iterationCount);

            a = newRange.Item1;
            b = newRange.Item2;

            streamWriter.WriteLine($"New range after bisection method applied : [{a}; {b}] ,number of iterations: {iterationCount} ");
           Console.WriteLine($"New range after bisection method applied : [{a}; {b}] ,number of iterations: {iterationCount} ");

            x = NutonMethod(a, out iterationCount);

            streamWriter.WriteLine($"Classic Nuton's method: x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine($"Classic Nuton's method: x = {x}, number of iterations: {iterationCount} ");

            h = 5;

            x = NutonMethodDiscret(a, h, out iterationCount);

            streamWriter.WriteLine($"Discret Nuton's method( h = {h}) : x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine($"Discret Nuton's method( h = {h}) : x = {x}, number of iterations: {iterationCount} ");

            a = 1.5;
            b = 2;


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

            h = 4;

            x = NutonMethodDiscret(a, h, out iterationCount);

            streamWriter.WriteLine($"Discret Nuton's method( h = {h}) : x = {x}, number of iterations: {iterationCount} ");
            Console.WriteLine($"Discret Nuton's method( h = {h}) : x = {x}, number of iterations: {iterationCount} ");


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

        public static Tuple<Complex, Complex[]>[] SolveGetMaxAbsoluteEigenValue(double[,] A)
        {
            //Tuple<Complex, Complex[]>[] result = new Tuple<Complex, Complex[]>[A.GetLength(0)];

            //result = GetMaxEigenValueCase1(A);

            //if (result == null)
            //{
            //    result = GetMaxEigenValueCase3(A);

            //}

            //if (result == null)
            //{
            //    result = GetMaxEigenValueCase4Norm(A);
            //}

            return GetMaxEigenValues(A);
        }

#region CasesPowerMethod

        public static Tuple<Complex, Complex[]>[] GetMaxEigenValueCase1(double[,] A_)
        {
            Complex[,] A = ToComplex(A_);

            // initial approximation
            Complex[] u = new Complex[A.GetLength(0)];
            u[0] = 1;
            u[1] = (Complex)(1 + 2m);

            Complex eigenValue = 0.082;

            int iterationCount = 0;

            Complex[] checkVector = u;
            Complex checkEigen = eigenValue;

            while (GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, u)), VecotorWithScalarMultiplication(u, eigenValue))).Magnitude > epsilon)
            {
                //Console.WriteLine(GetMagnitude(VectorSubtraction(MatrixMultiplication(A, u).ToArray(), VecotorWithScalarMultiplication(u, eigenValue))));

                iterationCount++;

                Complex[] v = MatrixMultiplication(A, u);

                eigenValue = GetMax(v);

                u = VectorWithScalarDivison(v, eigenValue);


                if (iterationCount > 10000)
                {
                    //Console.WriteLine("First case method could not succeed... Iterations done: " + iterationCount);
                    return null;

                    double precisionNow = GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, u)), VecotorWithScalarMultiplication(u, eigenValue))).Magnitude;
                    double precisionPast = GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, checkVector)), VecotorWithScalarMultiplication(u, checkEigen))).Magnitude;
                    if (Math.Abs(precisionNow) - Math.Abs(precisionPast) > 0)
                    {
                        //No chance here... aborting...

                        Console.WriteLine("First case method could not succeed... Iterations done: " + iterationCount);
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

            int iterationCount = 0;

            Complex[] checkVector = u;
            Complex checkEigen = eigenValue;

            while (GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, u)), VecotorWithScalarMultiplication(u, eigenValue))).Magnitude > epsilon)
            {
                //Console.WriteLine(GetMagnitude(VectorSubtraction(MatrixMultiplication(A, u).ToArray(), VecotorWithScalarMultiplication(u, eigenValue))));

                iterationCount++;
                u = (MatrixMultiplication(A, MatrixMultiplication(A, u)));

                eigenValue = Complex.Sqrt((GetMax(u)));

                u = VectorWithScalarDivison(u, GetMax(u));

                nextUStep = MatrixMultiplication(A, u);

                if (iterationCount > 10000)
                {
                    //Console.WriteLine("Third case method could not succeed... Iterations done: " + iterationCount);
                    return null;

                    double precisionNow = GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, u)), VecotorWithScalarMultiplication(u, eigenValue))).Magnitude;
                    double precisionPast = GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, checkVector)), VecotorWithScalarMultiplication(u, checkEigen))).Magnitude;
                    if (Math.Abs(precisionNow) - Math.Abs(precisionPast) > 0)
                    {
                        //No chance here... aborting...
                        Console.WriteLine("Third case method could not succeed... Iterations done: " + iterationCount);
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

            //Console.WriteLine("Third case method could succeed! Iterations done: " + iterationCount);

            return result;
        }

        public static Tuple<Complex, Complex[]>[] GetMaxEigenValueCase4(double[,] A_)
        {
            Complex[,] A = ToComplex(A_);

            // initial approximation
            Complex[] u = new Complex[A.GetLength(0)];
            u[0] = 2.4;


            List<Complex[]> vNorm = new List<Complex[]>();
            List<Complex[]> uNorm = new List<Complex[]>();

            uNorm.Add(u);


            Complex eigenValue = 0;

            //precision

            int iterationCount = 0;

            List<Complex[]> vectors = new List<Complex[]>();

            vNorm.Add(new Complex[2]);

            while (true)
            {
                Complex[] v = MatrixMultiplication(A, u);

                u = v;

                vectors.Add(u);

                vNorm.Add(MatrixMultiplication(A, uNorm[uNorm.Count - 1]));
                uNorm.Add(VectorWithScalarDivison(vNorm[vNorm.Count - 1], GetMax(vNorm[vNorm.Count - 1])));


                iterationCount++;

                if (iterationCount > 5)
                {
                    double yk2 = (vectors[iterationCount - 1])[0].Real;
                    double yk1 = (vectors[iterationCount - 2])[0].Real;
                    double yk = (vectors[iterationCount - 3])[0].Real;
                    double ykm1 = (vectors[iterationCount - 4])[0].Real;

                    double r = Math.Sqrt(Math.Abs((yk * yk2 - yk1 * yk1)
                                                  / (ykm1 * yk1 - yk * yk)));

                    double cosPhi = (yk2 + r * r * yk) / (2 * r * yk1);
                    double sinPhi = Math.Sqrt(1 - cosPhi * cosPhi);

                    Complex number1 = new Complex(r * cosPhi, r * sinPhi);
                    Complex number2 = new Complex(r * cosPhi, -r * sinPhi);

                    Complex[] vector1 = VectorSubtraction(vNorm[iterationCount - 2], VecotorWithScalarMultiplication(uNorm[iterationCount - 3], number2));

                    Complex[] vector2 = VectorSubtraction(vNorm[iterationCount - 2], VecotorWithScalarMultiplication(uNorm[iterationCount - 3], number1));

                    //Console.WriteLine(GetMagnitude(VectorSubtraction(MatrixMultiplication(A, vector1).ToArray(), VecotorWithScalarMultiplication(vector1, number1))).Magnitude);

                    if (GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, vector1)), VecotorWithScalarMultiplication(vector1, number1))).Magnitude < epsilon)
                    {
                        Tuple<Complex, Complex[]>[] result = new Tuple<Complex, Complex[]>[2];

                        result[0] = new Tuple<Complex, Complex[]>(number1, vector1);
                        result[1] = new Tuple<Complex, Complex[]>(number2, vector2);

                        //Console.WriteLine("Fourth case method could succeed! Iterations done: " + iterationCount);
                        return result;
                    }

                    //if (iterationCount  %100==0)
                    //{
                    //    Console.WriteLine("Fourth case method could not succeed! Iterations done: " + iterationCount);
                    //}
                }
            }

        }

        public static Tuple<Complex, Complex[]>[] GetMaxEigenValueCase4Norm(double[,] A_)
        {
            Complex[,] A = ToComplex(A_);

            // initial approximation
            Complex[] u = new Complex[A.GetLength(0)];
            u[0] = 0.01;


            List<Complex[]> vNorm = new List<Complex[]>();
            List<Complex[]> uNorm = new List<Complex[]>();

            uNorm.Add(u);


            Complex eigenValue = 0;

            //precision

            int iterationCount = 0;

            List<Complex[]> vectors = new List<Complex[]>();

            vNorm.Add(new Complex[2]);

            while (true)
            {
                Complex[] v = MatrixMultiplication(A, u);

                u = v;

                vectors.Add(u);

                vNorm.Add(MatrixMultiplication(A, uNorm[uNorm.Count - 1]));
                uNorm.Add(VectorWithScalarDivison(vNorm[vNorm.Count - 1], GetMax(vNorm[vNorm.Count - 1])));


                iterationCount++;

                if (iterationCount > 5)
                {
                    int k = iterationCount - 3;

                    double vk2 = (vNorm[k + 2])[0].Real;
                    double vk1 = (vNorm[k + 1])[0].Real;
                    double vk = (vNorm[k])[0].Real;
                    double ukm1 = (uNorm[k - 1])[0].Real;
                    double uk = (uNorm[k])[0].Real;

                    double r = Math.Sqrt(((vk * vk2 * GetMax(vNorm[k + 1]).Real - vk1 * vk1 * GetMax(vNorm[k]).Real)
                                          / (ukm1 * vk1 - uk * uk * GetMax(vNorm[k]).Real)));

                    double cosPhi = Math.Clamp((vk2 * GetMax(vNorm[k + 1]).Real + r * r * uk) / (2 * r * vk1), -1, 1);
                    double sinPhi = Math.Sqrt(1 - cosPhi * cosPhi);

                    Complex number1 = new Complex(r * cosPhi, r * sinPhi);
                    Complex number2 = new Complex(r * cosPhi, -r * sinPhi);

                    Complex[] vector1 = VectorSubtraction(vNorm[k + 1], VecotorWithScalarMultiplication(uNorm[k], number2));

                    Complex[] vector2 = VectorSubtraction(vNorm[k + 1], VecotorWithScalarMultiplication(uNorm[k], number1));

                    Console.WriteLine(GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, vector1)), VecotorWithScalarMultiplication(vector1, number1))).Magnitude);

                    if (GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, vector1)), VecotorWithScalarMultiplication(vector1, number1))).Magnitude < epsilon)
                    {
                        Tuple<Complex, Complex[]>[] result = new Tuple<Complex, Complex[]>[2];

                        result[0] = new Tuple<Complex, Complex[]>(number1, vector1);
                        result[1] = new Tuple<Complex, Complex[]>(number2, vector2);

                        //Console.WriteLine("Fourth case method could succeed! Iterations done: " + iterationCount);
                        return result;
                    }

                    //if (iterationCount  %100==0)
                    //{
                    //    Console.WriteLine("Fourth case method could not succeed! Iterations done: " + iterationCount);
                    //}
                }
            }

        }


        public static Tuple<Complex, Complex[]>[] GetMaxEigenValues(double[,] A_)
        {
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
                vNorm.Add(MatrixMultiplication(A, uNorm[uNorm.Count - 1]));
                uNorm.Add(VectorWithScalarDivison(vNorm[vNorm.Count - 1], GetMax(vNorm[vNorm.Count - 1])));


                iterationCount++;

                //Check case 1 (one max absolute eigenvalue)

                number1 = vNorm[vNorm.Count - 1][0] / uNorm[vNorm.Count - 2][0];
                vector1 = uNorm[vNorm.Count - 2];

                if (GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A,vector1)), VecotorWithScalarMultiplication(vector1, number1))).Magnitude < epsilon)
                {
                    Console.WriteLine("First case method could succeed! Iterations done: " + iterationCount);
                    Tuple<Complex, Complex[]>[] result = new Tuple<Complex, Complex[]>[1];

                    result[0] = new Tuple<Complex, Complex[]>(number1, vector1);

                    return  result;
                }

                //Check case 3 (two opposite but absolutely equal eigenvalues)
                if (iterationCount > 3)
                {
                    int k = iterationCount - 3;

                    double vk2 = (vNorm[k + 2])[0].Real;
                    double uk = (uNorm[k])[0].Real;


                    number1 =Math.Sqrt( ( vk2*GetMax(vNorm[k+1]).Real)/uk);
                    vector1 =VectorSum( vNorm[k+1],VecotorWithScalarMultiplication(uNorm[k],number1));

                    if (GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, vector1)), VecotorWithScalarMultiplication(vector1, number1))).Magnitude < epsilon)
                    {
                        vector2 = VectorSubtraction(vNorm[k + 1], VecotorWithScalarMultiplication(uNorm[k], number1));

                        Console.WriteLine("Third case method could succeed! Iterations done: " + iterationCount);
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

                    double vk2 = (vNorm[k + 2])[0].Real;
                    double vk1 = (vNorm[k + 1])[0].Real;
                    double vk = (vNorm[k])[0].Real;
                    double ukm1 = (uNorm[k - 1])[0].Real;
                    double uk = (uNorm[k])[0].Real;

                    double r = Math.Sqrt(((vk * vk2 * GetMax(vNorm[k + 1]).Real - vk1 * vk1 * GetMax(vNorm[k]).Real)
                                          / (ukm1 * vk1 - uk * uk * GetMax(vNorm[k]).Real)));

                    double cosPhi = Math.Clamp((vk2 * GetMax(vNorm[k + 1]).Real + r * r * uk) / (2 * r * vk1), -1, 1);
                    double sinPhi = Math.Sqrt(1 - cosPhi * cosPhi);

                    number1 = new Complex(r * cosPhi, r * sinPhi);
                    number2 = new Complex(r * cosPhi, -r * sinPhi);

                    vector1 = VectorSubtraction(vNorm[k + 1], VecotorWithScalarMultiplication(uNorm[k], number2));

                    vector2 = VectorSubtraction(vNorm[k + 1], VecotorWithScalarMultiplication(uNorm[k], number1));

                    //Console.WriteLine(GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, vector1)), VecotorWithScalarMultiplication(vector1, number1))).Magnitude);

                    if (GetMagnitude(VectorSubtraction(Enumerable.ToArray(MatrixMultiplication(A, vector1)), VecotorWithScalarMultiplication(vector1, number1))).Magnitude < epsilon)
                    {
                        Tuple<Complex, Complex[]>[] result = new Tuple<Complex, Complex[]>[2];

                        result[0] = new Tuple<Complex, Complex[]>(number1, vector1);
                        result[1] = new Tuple<Complex, Complex[]>(number2, vector2);

                        Console.WriteLine("Fourth case method could succeed! Iterations done: " + iterationCount);
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

        public static float ScalarMultiplication(float[] A, float[] B)
        {
            float sum = 0;

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

            List<Complex> result = new List<Complex>();

            double[] prevLambda = new double[A.GetLength(0)];

            for (int i = 0; i < prevLambda.Length; i++)
            {
                prevLambda[i] = A[i, i];
            }

            while (true)
            {
                double[,] Q = new double[1, 1];
                double[,] R = new double[1, 1];

                GetQR(A, ref Q, ref R);

                //QrDecomposition decomposition = new QrDecomposition(A);

                //Q = decomposition.OrthogonalFactor;
                //R = decomposition.UpperTriangularFactor;

                A = MatrixMultiplication(R, Q);

                Complex[] currentValues=new Complex[A.GetLength(0)];

                for (int i = 0; i < A.GetLength(0); i++)
                {
                    if (i < A.GetLength(0) - 1 && Math.Abs(A[i + 1, i]) > 0.0000001)
                    {
                        double D = (A[i, i] + A[i + 1, i + 1]) * (A[i, i] + A[i + 1, i + 1]) - 4 * ((A[i, i] * A[i + 1, i + 1]) - (A[i + 1, i] * A[i, i + 1]));

                        Complex x1 = (-(A[i, i] + A[i + 1, i + 1]) + Complex.Sqrt(D)) / 2;
                        Complex x2 = (-(A[i, i] + A[i + 1, i + 1]) - Complex.Sqrt(D)) / 2;

                        currentValues[i] = x1;
                        currentValues[i + 1] = x2;
                      
                        i++;
                    }
                    else
                    {
                        currentValues[i] = A[i, i];
                    }
                }

                double maxDiff = double.MinValue;

                for (int i = 0; i < prevLambda.Length; i++)
                {
                    if (maxDiff < Math.Abs(prevLambda[i] - currentValues[i].Magnitude))
                    {
                        maxDiff = Math.Abs(prevLambda[i] - currentValues[ i].Magnitude);
                    }
                }


                //Console.WriteLine(maxDiff);

                if (maxDiff < 0.000001)
                {
                    break;
                }

                for (int i = 0; i < prevLambda.Length; i++)
                {
                    prevLambda[i] = currentValues[i].Magnitude;
                }
            }

            //PrintMatrix(A);
            for (int i = 0; i < A.GetLength(0); i++)
            {
                if (i < A.GetLength(0) - 1 && Math.Abs(A[i + 1, i]) > epsilon)
                {
                    double D = (A[i, i] + A[i + 1, i + 1]) * (A[i, i] + A[i + 1, i + 1]) -4*((A[i, i] * A[i + 1, i + 1])- (A[i+1, i] * A[i, i + 1]));

                    Complex x1 = (-(A[i, i] + A[i + 1, i + 1]) + Complex.Sqrt(D)) / 2;
                    Complex x2 = (-(A[i, i] + A[i + 1, i + 1]) - Complex.Sqrt(D)) / 2;

                    result.Add(x1);
                    result.Add(x2);

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

            double[,] FinalQ;

            //if (QList.Count == 0)
            //{
            //    FinalQ = GetOnesMatrix(A.GetLength(0));
            //}
            //else
            //{


                FinalQ = ExpandMatrix(GetTransposedMatrix(QList[0]), A.GetLength(0));

                for (int i = 1; i < QList.Count; i++)
                {
                    FinalQ = MatrixMultiplication(FinalQ, GetTransposedMatrix(ExpandMatrix(QList[i], FinalQ.GetLength(0))));
                }
            //}
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
                double newX = x - Func(x) / h;

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

            List<Tuple<int,double>> classicNutomCoverage = new List<Tuple<int, double>>();

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

            for (int i=0;i<30;i++)
            {
                double newX = x - Func(x) / FuncP(x);

                classicNutomCoverage.Add(new Tuple<int, double>(i+1,Math.Abs(x-newX)));

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

            h = 5;

            for (int i = 0; i < 30; i++)
            {
                double newX = x - Func(x) / h;

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

            h = 2;

            for (int i = 0; i < 30; i++)
            {
                double newX = x - Func(x) / h;

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

            h = 10;

            for (int i = 0; i < 30; i++)
            {
                double newX = x - Func(x) / h;

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