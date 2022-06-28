using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChisMethod
{
    class Program
    {
        public static Vector FP(double x)
        {
            Vector psi = new Vector(2);
            psi[0] = 1.0;
            if (x != 0.0) psi[1] = 1.0 / x;
            else psi[1] = double.NaN;
            return psi;
        }

        public static Vector PR(double t, Vector x)
        {
            Vector pr = new Vector(2);
            pr[0] = x[1];
            pr[1] = -x[0];
            return pr;
        }

        static void Main(string[] args)
        {
            
            Console.WriteLine("Test Vector!\n");

            double[] vector_1 = { 1.5, -2.0 };
            Vector a = new Vector(vector_1);
            double[] vector_2 = { 3.0, 4.0 };
            Vector b = new Vector(vector_2);

            //b[0] = 3.0; b[1] = 4.0;
            Vector c = a + b;
            Console.WriteLine("Сложение 2-ух векторов при помощи /operator/: {0} + {1} = {2}", a, b, c);
            Console.WriteLine("Сложение 2-ух векторов при помощи функции /Addition/: {0} + {1} = {2}", a, b, a.Addition(b));

            Vector d = (2 * a - b) + c * -0.5;
            Console.WriteLine("Все вместе: Скалярное произведение вектора на число/число на вектор/сложение/вычитание векторов:\n(2 * {0} - {1}) + {2} * (-0.5) = {3}\n", a, b, c, d);

            Console.WriteLine("Длина вектора {0} = {1}", a, a.Len());
            Console.WriteLine("Нормализация вектора {0} => Вектор(норм) = {1}\n", a, a.Normalization());

            //Vector_NumMet e = a * b;
            //Console.WriteLine("e = {0}", e);

            Console.WriteLine("Скалярное произведение 2-ух векторов при помощи /operator/: {0} * {1} = {2}", a, b, a * b);
            Console.WriteLine("Скалярное произведение 2-ух векторов при помощи функции /Multiplication/: {0} * {1} = {2}", a, b, a.Multiplication(b));
            Console.WriteLine("Скалярное произведение 2-ух векторов при помощи функции /Scalar Product/ {0} * {1} = {2}\n", a, b, a.ScalarProduct(b));

            

         

            Console.WriteLine("\nTest NumericalMet!\n");
            double[] a1 = { 3, 2, -5, 7 };
            double y = 3.0;
            Console.WriteLine("x={0}  Gorner={1}\n", y, Chislo.Gorner(a1, y));
            for (double xt = -2.0; xt <= 2.0; xt += 0.25)
            {
                Console.WriteLine(" x={0}\t SinP={1}\t Sin={2}", xt, Chislo.SinP(xt, 0.00001), Math.Sin(xt));
            }
            Console.WriteLine("\nSqrtP(2)={0}  Sqrt(2)={1}", Chislo.SqrtP(2.0, 0.00001), Math.Sqrt(2));
            Console.WriteLine("\nМетод половинного деления ={0}", Chislo.KorenPD(0.2, 3.0, 0.00001, x => x * x - 1.0));
            Console.WriteLine("Метод половинного деления ={0}", Chislo.KorenPD(-3, 0.0, 0.00001, x => x * x - 1.0));
            Console.WriteLine("Метод половинного деления ={0}\n", Chislo.KorenPD(1.5, 3.0, 0.00001, x => x * x - 1.0));


            Console.WriteLine("1) Newton: {0}\n", Chislo.Newton(0.001, 0.00001, x => x + Math.Exp(x)));

            Console.WriteLine("2) PosPr: {0}\n", Chislo.PosPr(-3, 0.00001, x => (x * x + 4) / 5));

            Console.WriteLine("3) Exp(x): {0}\n", Chislo.ExpX(3, 0.00001));

            Console.ReadKey();



            Console.WriteLine("Test Matrix!\n");

            double[,] matrix1 = { { 2, 1 }, { 4, 3 } };
            Matrix m1 = new Matrix(matrix1);
            //m1.Print();
            Vector p = m1 * b;
            Console.WriteLine("Умножение матрицы на вектор: m1 * b = {0}\n", p);
            Console.WriteLine("m1={0}\n", m1);


            double[,] mantrix2 = { { 3, 4 }, { 2, 5 } };
            Matrix m2 = new Matrix(mantrix2);
            m2.Print();

            double[,] matrix3 = new double[4, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 }, { 9, 10, 11 } };   //4-строки, 3-столбца
            Matrix m3 = new Matrix(matrix3);
            m3.Print();

            Console.WriteLine("Транспонирование матрицы:");
            Matrix m3tr = m3.Transp();
            m3tr.Print();

            Console.WriteLine("Умножение матриц:");
            Matrix umn = m1 * m2;
            umn.Print();

            Console.WriteLine("Умножение матрицы на число:");
            Matrix umnch = m1 * 5;
            umnch.Print();

            Console.WriteLine("Сложение матриц:");
            Matrix rez_matrix = m1 + m2;
            rez_matrix.Print();

            Console.WriteLine("Вычитание матриц:");
            Matrix sub = m1 - m2;
            sub.Print();

            Console.WriteLine("Матрица для методов СЛУ:");
            double[,] matrixForGauss = { { 1, 2, 2 }, { 2, 2, 1 }, { 6, 4, 2 } };
            Matrix mfg = new Matrix(matrixForGauss);
            mfg.Print();

            Console.WriteLine("Вектор для методов СЛУ:");
            double[] vectorForGauss = { 1.5, -2.0, 4 };
            Vector vfg = new Vector(vectorForGauss);
            Console.WriteLine("{0}\n", vfg);

            //Console.WriteLine("SLU_UP: {0}", Matrix_NumMet.SLU_UP(mfg, vfg));         //работает

            //Console.WriteLine("SLU_DOWN: {0}", Matrix_NumMet.SLU_DOWN(mfg, vfg));     //работает

            Console.WriteLine("Gauss: {0}\n", Matrix.Gauss(mfg, vfg));
            //mfg * rx - для проверки 

            Console.WriteLine("Givens: {0}\n", Matrix.MethodGivens(mfg, vfg));

            Console.WriteLine("Test Progonka");
            //Пример 1
            double[] vpr = { 1, 1, 1, 1 };
            Vector bpr = new Vector(vpr);
            Vector cpr = new Vector(vpr);
            Vector epr = new Vector(vpr);
            Vector dpr = 4.0 * cpr;
            Vector xpr = Matrix.Progonka_P(cpr, dpr, epr, bpr);
            Console.WriteLine("xpr={0}", xpr);
            Vector bUP = new Vector(4); bUP[0] = 1.0; bUP[1] = -5.0; bUP[2] = 2.0;
            Vector bSR = new Vector(4); bSR[0] = 2.0; bSR[1] = 10.0; bSR[2] = -5.0; bSR[3] = 4.0;
            Vector bDOWN = new Vector(4); bDOWN[0] = 0.0; bDOWN[1] = 1.0; bDOWN[2] = 1.0; bDOWN[3] = 1.0;
            Vector bres = new Vector(4); bres[0] = -5.0; bres[1] = -18.0; bres[2] = -40.0; bres[3] = -27.0;
            Console.WriteLine("ProgonkaV {0}\n", Matrix.Progonka_P(bDOWN, bSR, bUP, bres));// res= -3 1 5 -8

            //Пример 2
            Vector bUP1 = new Vector(3); bUP1[0] = 1.0; bUP1[1] = -5.0; bUP1[2] = 2.0;
            Vector bSR1 = new Vector(4); bSR1[0] = 2.0; bSR1[1] = 10.0; bSR1[2] = -5.0; bSR1[3] = 4.0;
            Vector bDOWN1 = new Vector(3); bDOWN1[0] = 1.0; bDOWN1[1] = 1.0; bDOWN1[2] = 1.0;
            Vector bres1 = new Vector(4); bres1[0] = -5.0; bres1[1] = -18.0; bres1[2] = -40.0; bres1[3] = -27.0;
            Console.WriteLine("ProgonkaS {0}\n", Matrix.Progonka_P(bDOWN1, bSR1, bUP1, bres1));// res= -3 1 5 -8

            //Console.ReadKey();

            Console.WriteLine("Test PosledPr");
            double[,] matrixForPosledPr = { { 4, 0.24, -0.08 }, { 0.09, 3, -0.15 }, { 0.04, -0.08, 4 } };
            Matrix mfpp = new Matrix(matrixForPosledPr);
            double[] vectorForPosledPr = { 8, 9, 20 };
            Vector vfpp = new Vector(vectorForPosledPr);

            Vector xr = Matrix.PosledPr(mfpp, vfpp, 0.0001, 10);
            if (xr != null)
            {
                Console.WriteLine("Posled Pr: {0}", xr);
                Console.WriteLine("Test: {0}", mfpp * xr);
            }
            else Console.WriteLine("No Posled Pr");




            Console.WriteLine("\nTest 1 Splain");

            double[] x0sp = { 1, 2, 3, 4, 5, 6 };
            double[] y0sp = { 1.0002, 1.0341, 0.6, 0.40105, 0.1, 0.23975 };
            Vector xsp0 = new Vector(x0sp);
            Vector ysp0 = new Vector(y0sp);
            Spline test1sp = new Spline(xsp0, ysp0);

            test1sp.SolveSpline();
            for (double x = 1; x <= 6; x += 0.25)
                Console.WriteLine("xsp={0} ysp1={1}", x, test1sp.GetValue(x));

            Console.WriteLine("\nTest 2 Splain");
            // Generate points for Spalain
            double[] x1sp = new double[9];
            double[] y1sp = new double[9];
            for (int i = 0; i < 9; i++)
            {
                double x1 = -1.0 + i * 0.25;
                double y1 = Math.Sin(2 * x1);
                x1sp[i] = x1; y1sp[i] = y1;
            }
            Vector xspl = new Vector(x1sp);
            Vector yspl = new Vector(y1sp);
            Spline test2sp = new Spline(xspl, yspl);
            test2sp.SolveSpline();

            for (double x = -1; x <= 1.0; x += 0.1)
                Console.WriteLine("xsp={0} ysp1={1} tsp={2}", x, test2sp.GetValue(x), Math.Sin(2 * x));




            Console.WriteLine("\nTest MNK!");
            double[] xForMNK = { 2, 4, 6, 12 }; //входные данные Х
            double[] yForMNK = { 8, 5.25, 3.5, 3.25 }; //входные данные У
            Vector xx = new Vector(xForMNK);
            Vector yy = new Vector(yForMNK);
            MNK mn = new MNK(xx, yy, 2);
            mn.SolveMNK(FP);
            for (double xt = 2; xt <= 12; xt += 0.5)
            {
                Console.WriteLine("x = {0} ya = {1}\n", xt, mn.GetValue(xt, FP));   //ya - y аналитическое
            }

            Console.WriteLine("Test Integrals");
            double integR = Integral.MethodRectangle(x => Math.Sin(4.0 * x), 0.0, 1.0, 0.0001);
            Console.WriteLine("Calc Rectangle={0} Test={1}", integR, (1.0 - Math.Cos(4.0)) * 0.25);
            double integT = Integral.MethodTrapezesUpdate(x => Math.Sin(4.0 * x), 0.0, 1.0, 0.0001);
            Console.WriteLine("Calc Trapezium={0} Test={1}", integT, (1.0 - Math.Cos(4.0)) * 0.25);
            double integS = Integral.MethodSimpsonUpdate(x => Math.Sin(4.0 * x), 0.0, 1.0, 0.0001);
            Console.WriteLine("Calc Simpson={0} Test={1}", integS, (1.0 - Math.Cos(4.0)) * 0.25);
            //double integ2 = Integral.CalcSimpson2Var(4.0, 4.4, 2.0, 2.6, 4, (x1, y1) => 1.0 / (x1 * y1));
            //Console.WriteLine("CalcSimpson2Var={0}", integ2);

            Console.WriteLine("Monte Carlo: {0}", Integral.MonteCarlo2Integral(4.0, 4.4, 2.0, 2.6, 100000, (x1, y1) => 1.0 / (x1 * y1)));



            Console.WriteLine("\nTest Diff Ur");
            Console.WriteLine("Test Eiler Diff Ur");
            Vector xn = new Vector(2); xn[0] = 0; xn[1] = 1.0;
            Matrix reEiler = DiffUr.Eiler(0.0, 1.0, xn, 10, PR);
            Console.WriteLine("t        x1        x2         x1a        x2a");
            for (int k = 0; k <= 10; k++)
            {
                double t = k * 0.1;
                double x1a = Math.Sin(t); double x2a = Math.Cos(t);
                Vector el = reEiler.GetColumn(k);
                Console.WriteLine("t={0,4}\t x1={1,6}\t x2={2,6}\t   Analitic {3,6}\t  {4,6}", t, el[1], el[2], x1a, x2a);
            }

            Console.WriteLine("\nTest Runge Kutta 2 Diff Ur");
            Matrix reRK2 = DiffUr.RungeKutta2(0.0, 1.0, xn, 10, PR);
            Console.WriteLine("t        x1        x2         x1a        x2a");
            for (int k = 0; k <= 10; k++)
            {
                double t = k * 0.1;
                double x1a = Math.Sin(t); double x2a = Math.Cos(t);
                Vector el = reRK2.GetColumn(k);
                Console.WriteLine("t={0,4}\t x1={1,6}\t x2={2,6}\t   Analitic {3,6}\t  {4,6}", t, el[1], el[2], x1a, x2a);
            }

            Console.WriteLine("\nтest Runge Kutta 4 Diff Ur");
            Matrix reRK4 = DiffUr.RungeKutta4(0.0, 1.0, xn, 10, PR);
            Console.WriteLine("t        x1        x2         x1a        x2a");
            for (int k = 0; k <= 10; k++)
            {
                double t = k * 0.1;
                double x1a = Math.Sin(t); double x2a = Math.Cos(t);
                Vector el = reRK4.GetColumn(k);
                Console.WriteLine("t={0,4}\t x1={1,6}\t x2={2,6}\t   Analitic {3,6}\t  {4,6}", t, el[1], el[2], x1a, x2a);
            }

            Console.WriteLine("\nTest Adams 4 Diff Ur");
            Matrix reAd3 = DiffUr.Adams4(0.0, 1.0, xn, 10, PR);
            Console.WriteLine("t        x1        x2         x1a        x2a");
            for (int k = 0; k <= 10; k++)
            {
                double t = k * 0.1;
                double x1a = Math.Sin(t); double x2a = Math.Cos(t);
                Vector el = reAd3.GetColumn(k);
                Console.WriteLine("t={0,4}\t x1={1,6}\t x2={2,6}\t   Analitic {3,6}\t  {4,6}", t, el[1], el[2], x1a, x2a);
            }


            Console.ReadKey();
        }
    }
}
