using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChisMethod
{
    class Spline
    {
        public int N; //кол-во точек
        public int countInterval;
        public Vector xz;    // х текущая
        public Vector yz;    // у текущая
        public Vector h; // расстояние между Xi и X(i-1)

        //Вспомогательные векторы
        public Vector A;
        public Vector B;
        public Vector C;
        public Vector D;

        public Spline(Vector xx, Vector yy)
        {
            N = xx.Size;
            countInterval = N - 1;
            xz = xx;
            yz = yy;
            h = new Vector(N);
            A = new Vector(N);
            B = new Vector(N);
            C = new Vector(N);
            D = new Vector(N);
        }

        public void SolveSpline()     //Интерполирование сплайнами
        {
            for (int i = 0; i < N; i++) A[i] = yz[i];
            for (int i = 1; i < N; i++) h[i] = xz[i] - xz[i - 1];
            Vector diag = new Vector(N - 2);
            Vector diagDown = new Vector(N - 3); //нижняя диаг
            Vector diagUp = new Vector(N - 3); //верхняя диаг
            Vector prav = new Vector(N - 2);   //правые части
            Vector res_tr = new Vector(N - 2); //результат трехдиагональная матрица

            for (int i = 0; i < N - 2; i++)
            {
                diag[i] = 2 * (h[i + 1] + h[i + 2]);
                prav[i] = 6.0 * ((yz[i + 2] - yz[i + 1]) / h[i + 2] - (yz[i + 1] - yz[i]) / h[i + 1]);
                if (i < N - 3) { diagDown[i] = h[i + 1]; diagUp[i] = h[i + 2]; }

            }

            //progonka
            res_tr = Matrix.Progonka_P(diagDown, diag, diagUp, prav);

            for (int i = 0; i < N - 2; i++)
            {
                C[i + 1] = res_tr[i];
            }

            for (int i = 0; i < N - 1; i++)
            {
                D[i + 1] = (C[i + 1] - C[i]) / h[i + 1];
                B[i + 1] = (h[i + 1] * C[i + 1] / 2.0) - (h[i + 1] * h[i + 1] * D[i + 1] / 6.0) + (yz[i + 1] - yz[i]) / h[i + 1];
            }

        }

        public double GetValue(double x)
        {
            double s = 0;
            if (x < xz[0] || x > xz[N - 1]) return double.NaN;

            //Поиск интервала
            for (int i = 1; i < N; i++)
            {
                if (x <= xz[i])
                {
                    double dx = (x - xz[i]);
                    s = A[i] + B[i] * dx + C[i] * dx * dx / 2.0 + D[i] * dx * dx * dx / 6.0;
                    return s;
                }
            }

            return s;
        }
    }
}
