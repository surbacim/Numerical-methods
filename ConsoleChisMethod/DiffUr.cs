using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChisMethod
{
    delegate Vector PravDU(double t, Vector x);

    class DiffUr
    {
        //Метод Эйлера
        public static Matrix Eiler(double tn, double tk, Vector xn, int m, PravDU prDU)
        {
            //m - кол-во разбиений интервала
            int n = xn.Size;    //кол-во шагов
            Matrix result = new Matrix(n + 1, m + 1);
            Vector pr;   //правые части
            Vector xt;   //x текущая
            double t = tn;
            double dt = (tk - tn) / m;  //кол-во интервалов
            Vector columnrez = new Vector(n + 1);
            columnrez[0] = t;
            for (int i = 0; i < n; i++) columnrez[i + 1] = xn[i];
            result.SetColumn(0, columnrez);
            xt = xn.Copy();
            for (int k = 1; k <= m; k++)
            {
                pr = prDU(t, xt);   //передаем правые части и получаем производные
                xt = xt + dt * pr;
                t = t + dt; //шаг дискретности (изменение времени)
                columnrez[0] = t; //tttttt
                for (int i = 0; i < n; i++) columnrez[i + 1] = xt[i];
                result.SetColumn(k, columnrez);
            }
            return result;

        }

        //Метод Рунге — Кутты 2 порядка
        public static Matrix RungeKutta2(double tn, double tk, Vector xn, int m, PravDU prDu)
        {
            //m - кол-во разбиений интервала
            int n = xn.Size;    //кол-во шагов
            Matrix result = new Matrix(n + 1, m + 1);
            Vector k1;   //первый коэффициент правой части
            Vector k2;   //второй коэффициент правой части
            Vector xt, xs;   //x текущая и следующая
            double t = tn;
            double dt = (tk - tn) / m;  //кол-во интервалов 
            Vector columnrez = new Vector(n + 1);
            columnrez[0] = t;

            for (int i = 0; i < n; i++) columnrez[i + 1] = xn[i];
            result.SetColumn(0, columnrez);
            xt = xn.Copy();

            for (int k = 1; k <= m; k++)
            {
                k1 = prDu(t, xt);   //Находим первый коэффициент

                xs = xt + dt * k1;  //Аргумент функции для к2
                t = t + dt; //шаг дискретности (изменение времени)
                k2 = prDu(t, xs);   //Находим второй коэффициент

                xt = xt + (dt / 2) * (k1 + k2);  //Приближенное решение в точке Х[i+1]

                columnrez[0] = t;
                for (int i = 0; i < n; i++) columnrez[i + 1] = xt[i];
                result.SetColumn(k, columnrez);
            }
            return result;
        }

        //Метод Рунге — Кутты 4 порядка
        public static Matrix RungeKutta4(double tn, double tk, Vector xn, int m, PravDU prDu)
        {
            //m - кол-во разбиений интервала
            int n = xn.Size;    //кол-во шагов
            Matrix result = new Matrix(n + 1, m + 1);
            Vector k1;   //первый коэффициент правой части
            Vector k2;   //второй коэффициент правой части
            Vector k3;   //третий коэффициент правой части
            Vector k4;   //четвертый коэффициент правой части
            Vector xt, xs, xss, xsss;   //x текущая, следующая и тд...
            double t = tn;
            double dt = (tk - tn) / m;  //кол-во интервалов 
            Vector columnrez = new Vector(n + 1);
            columnrez[0] = t;

            for (int i = 0; i < n; i++) columnrez[i + 1] = xn[i];
            result.SetColumn(0, columnrez);
            xt = xn.Copy();

            for (int k = 1; k <= m; k++)
            {
                k1 = prDu(t, xt);   //Находим первый коэффициент

                xs = xt + (dt / 2) * k1;   //Аргумент функции для к2
                t = t + (dt / 2);
                k2 = prDu(t, xs);   //Находим второй коэффициент

                xss = xt + (dt / 2) * k2;
                k3 = prDu(t, xss);  //Находим третий коэффициент

                xsss = xt + dt * k3;
                t = t + (dt / 2);
                k4 = prDu(t, xsss); //Находим четвертый коэффициент

                xt = xt + dt / 6.0 * (k1 + 2.0 * k2 + 2.0 * k3 + k4); //Приближенное решение в точке Х[i+1]

                columnrez[0] = t;
                for (int i = 0; i < n; i++) columnrez[i + 1] = xt[i];
                result.SetColumn(k, columnrez);
            }
            return result;
        }

        //Метод Адамса 4 порядка
        public static Matrix Adams4(double tn, double tk, Vector xn, int m, PravDU prDu)
        {
            //m - кол-во разбиений интервала
            int n = xn.Size;    //кол-во шагов
            Matrix result = new Matrix(n + 1, m + 1);
            Vector k1;   //первый коэффициент правой части
            Vector k2 = null;   //второй коэффициент правой части
            Vector k3 = null;   //третий коэффициент правой части
            Vector k4 = null;   //четвертый коэффициент правой части
            Vector xt;   //x текущая
            double t = tn;
            double dt = (tk - tn) / m;  //кол-во интервалов 
            Vector columnrez = new Vector(n + 1);
            columnrez[0] = t;

            for (int i = 0; i < n; i++) columnrez[i + 1] = xn[i];
            result.SetColumn(0, columnrez);
            xt = xn.Copy();

            Matrix useRK4 = RungeKutta4(tn, tn + 3.0 * dt, xn, 3, prDu);
            for (int k = 0; k <= 3; k++)
            {
                double tt = tn + k * dt;
                result.SetColumn(k, useRK4.GetColumn(k));
                for (int i = 0; i < n; i++)
                {
                    xt[i] = result[i + 1, k];
                }

                if (k == 0) { k4 = prDu(t, xt); }
                if (k == 1) { k3 = prDu(t, xt); }
                if (k == 2) { k2 = prDu(t, xt); }
            }

            for (int k = 4; k <= m; k++)
            {
                k1 = prDu(t, xt);   //Находим первый коэффициент

                xt = xt + dt * ((55.0 / 24.0) * k1 - (59.0 / 24.0) * k2 + (37.0 / 24.0) * k3 - (9.0 / 24.0) * k4);  //Приближенное решение в точке Х[i+1]

                t = t + dt;
                k4 = k3.Copy();
                k3 = k2.Copy();
                k2 = k1.Copy();
                columnrez[0] = t;
                for (int i = 0; i < n; i++) columnrez[i + 1] = xt[i];
                result.SetColumn(k, columnrez);
            }
            return result;
        }


    }
}
