using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChisMethod
{
    delegate double Fun(double x);
    public delegate double Fun2(double x, double y);  

    class Chislo
    {
        public static double Gorner(double[] a, double x)
        {
            double s = a[0];
            for (int i = 1; i < a.Length; i++)
                s = (s * x + a[i]);
            return s;
        }

        public static double SinP(double x, double eps)
        {
            double s = 0, p = x;
            int k = 1;
            while (Math.Abs(p) > eps)
            {
                s += p;
                k += 2;
                p = -p * x * x / (k * (k - 1));
            }
            return s;
        }

        public static double SqrtP(double a, double eps)
        {
            if (a <= 0) return Double.NaN;
            double xt = a;
            double xs;
            double dx;
            do
            {
                xs = (xt + a / xt) * 0.5;
                dx = xs - xt;
                xt = xs;
            } while (Math.Abs(dx) > eps);
            return xs;
        }

        public static double KorenPD(double a, double b, double eps, Fun f)
        {
            double fa, fb, c, fc;
            fa = f(a); fb = f(b);
            if (fa * fb > 0) return Double.NaN;
            while (b - a > eps)
            {
                c = (a + b) / 2.0; fc = f(c);
                if (fa * fc <= 0) { b = c;
                }
                else { a = c; fa = fc; }
            }
            return (a + b) / 2.0;
        }

        public static double Diff(Fun f, double x, double dx)        //Нахождение производной
        {
            return (f(x + dx) - f(x)) / dx;
        }


        public static double Newton(double x1, double eps, Fun f)
        {
            double dx;
            double olddx = double.MaxValue;
            double xn = x1;
            double xk = 0;
            do
            {
                xk = x1 - f(x1) / Diff(f, x1, eps / 2);
                dx = xk - x1;
                if (Math.Abs(dx) > Math.Abs(olddx)) return double.NaN;
                x1 = xk;
            } while (Math.Abs(dx) > eps);
            return xk;

        }

      
        public static double PosPr(double xk, double eps, Fun f)
        {
            double xPred;                          //xk - приближение; xPred - x предыдущий
            double dxk = double.MaxValue;
            double x = 0.0;

            while (Math.Abs(dxk) > eps)
            {
                xPred = xk;
                if (Math.Abs(x - xPred) <= eps) return double.NaN;
                x = f(xPred);
                dxk = xk - xPred;
            }

            return x;
        }

        public static double PosPr1(double xn, double eps, Fun f)
        {
            double xPred;                          //xk - приближение; xPred - x предыдущий
            double dxk = double.MaxValue;
            double xs = 0.0, xk = 0;
            double xt = xn;

            while (Math.Abs(dxk) > eps)
            {
             
                xs = f(xt);
                double dx = xs - xt;
                dxk = xt - xk;
                xPred = (f((dx / 2) + eps / 5.0) - xs) / (eps / 5.0);
                if (Math.Abs(dx) >= xPred * xk) return Double.NaN;
                xt = xs;

            }

            return xs;
        }

        public static double PosledPribleg(double xn, double eps, Fun fi)
        {
            double xk = 0;
            double xt = xn;
            double dx;
            double xs;
            do
            {
                xs = fi(xt);
                dx = xs - xt;
                _ = xt - xk;
                double prt = (fi((dx / 2) + eps / 5.0) - xs) / (eps / 5.0);
                if (Math.Abs(dx) >= prt * xk) return Double.NaN;
                xt = xs;
            } while (Math.Abs(dx) > eps);
            return xs;
        }

        //Ряд экспоненты
        public static double ExpX(double x, double eps)
        {
            double q = 1;
            double sum = 0;

            for (int n = 0; ; n++)
            {
                sum += q;
                q = x * q / (n + 1);        //exp(x) = ∑(x^k/k!)

                if (Math.Abs(q) < eps) break;
            }
            return sum;
        }

        public static double MyPow(double a, int pow) //Возвести в степень
        {
            double result = 1;
            for (int i = 0; i < pow; i++) result *= a;
            return result;
        }

       
    }
}
