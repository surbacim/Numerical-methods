using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChisMethod
{
    class Integral
    {
        public static double MethodRectangle(Fun f, double a, double b, double eps)    //Метод прямоугольников
        {
            int n = 1;  //Кол-во интревалов
            double oldres, curres = 0;
            oldres = f(a) * (b - a);
            double sum = 0;
            while (true)
            {
                n = n * 2;
                double h = (b - a) / n;

                for (int i = 1; i < n; i += 2)
                {
                    double x = a + i * h;
                    sum += f(x);
                }

                curres = h * sum;
                if (Math.Abs(curres - oldres) < eps) break;
                oldres = curres;
                //Console.WriteLine("Pr n={0} integ={1}", n, curres);    //Для проверки
            }
            //Console.WriteLine("Pr n={0}", n);     //Для проверки
            return curres;
        }

        public static double MethodTrapezesUpdate(Fun f, double a, double b, double eps)    //Метод трапеций
        {
            int n = 1;
            double oldinteg, curinteg = 0;
            double h = (b - a) / n;
            double sumn = (f(a) + f(b)) / 2.0;
            double sum = 0;
            oldinteg = sumn * (b - a);

            while (true)
            {
                n = n * 2;
                h = (b - a) / n;
                for (int i = 1; i < n; i += 2)
                {
                    double x = a + i * h;
                    sum += f(x);
                }

                curinteg = (sumn + sum) * h;
                if (Math.Abs(curinteg - oldinteg) < eps) break;
                oldinteg = curinteg;
            }
            return curinteg;
        }

        public static double MethodSimpsonUpdate(Fun f, double a, double b, double eps)   //Метод Симпсона
        {
            int n = 1;
            double x0, x1, raz, h = 0;
            double sumn = (f(a) + f(b)) / 2.0;
            double sum1 = 0;
            double sum2 = 0;
            x0 = sumn * (b - a);

            do
            {
                n = n * 2;
                h = (b - a) / n;    //длина отрезка
                for (int k = 1; k <= n; k += 2)
                {
                    double xk = a + k * h;  //чередующиеся границы и середины составных отрезков, на которых применяется формула Симпсона
                    if (k <= n - 1)
                    {
                        sum1 += f(xk);
                    }

                    double xk_1 = a + (k - 1) * h;
                    sum2 += f((xk + xk_1) / 2);
                }
                x1 = h / 3.0 * (1.0 / 2.0 * f(a) + sum1 + 2.0 * sum2 + 1.0 / 2.0 * f(b));
                raz = Math.Abs(x1 - x0);
                x0 = x1;
            } while (raz > eps);

            return x1;
        }

        public static double MethodTrapezes(Fun f, double a, double b, int n)    //Метод трапеций с фиксированным числом интервалов
        {
            double h = (b - a) / n;
            double sum = (f(a) + f(b)) / 2.0;
            //double sum = 0;
            for (int i = 1; i <= n - 1; i++)
            {
                double x = a + i * h;
                sum += f(x);
            }

            double result = ((a + b) / 2 + sum) * h;
            return result;
        }

        public static double MethodSimpson(Fun f, double a, double b, int n)     //Метод Симпсона с фиксированным числом интервалов
        {
            double h = (b - a) / n; //длина отрезка
            double sum1 = 0;
            double sum2 = 0;
            for (int k = 1; k <= n; k++)
            {
                double xk = a + k * h; //чередующиеся границы и середины составных отрезков, на которых применяется формула Симпсона.
                if (k <= n - 1)
                {
                    sum1 += f(xk);
                }

                double xk_1 = a + (k - 1) * h;      //X(k-1)
                sum2 += f((xk + xk_1) / 2);
            }

            double result = h / 3 * (1 / 2 * f(a) + sum1 + 2 * sum2 + 1 / 2 * f(b));
            return result;
        }

        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        //Вычисление двойного интеграла методом Монте-Карло
        public static double MonteCarlo2Integral(double xDown, double xUp, double yDown, double yUp, int N, Fun2 func)
        {
            //int point = 0;  //Количество точек внутри искомой функции
            double s = 0;   //Площадь искомой функции
            double totalS = (xUp - xDown) * (yUp - yDown);  //Общая площадь

            for (int i = 0; i < N; i++)
            {
                double x = GetRandomNumber(xDown, xUp); //Выбираем псевдо-случайные точки на фиксированном интервале
                double y = GetRandomNumber(yDown, yUp);

                //if ((yDown).CompareTo(func(x, y)) <= 0 && (yUp).CompareTo(func(x, y)) >= 0)
                //{
                //    point++;
                    s += func(x, y);
                //}

            }

            //double v = totalS * point / N;
            //double res = v * s / N; ;

            double v = s / N;
            double res = v * totalS;

            return res;
        }





       

    }
}
