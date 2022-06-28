using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChisMethod
{
    delegate Vector DelPsi(double x);

    class MNK
    {
        public int N;   //кол-во точек
        public int M;   //сколькими параметрами мы будем аппроксимировать (замена одних объектов другими, в каком-то смысле близкими к исходным, но более простыми)

        //входные данные
        public Vector xz; //х теоретическое
        public Vector yz; //у теоретическое
        public Vector p; //ветор коэффициентов

        public MNK(int n, int m)
        {
            N = n;
            xz = new Vector(N);
            yz = new Vector(n);

            M = m;
            p = new Vector(M);
        }

        public MNK(Vector xx, Vector yy, int m)
        {
            N = xx.Size;
            M = m;
            p = new Vector(M);
            xz = xx;
            yz = yy;
        }

        public void SolveMNK(DelPsi FPsi)
        {
            Matrix PSI = new Matrix(N, M);
            for (int i = 0; i < N; i++)
            {
                Vector pst = FPsi(xz[i]);
                PSI.SetRow(i, pst);
            }

            Matrix PSIT = PSI.Transp();
            Matrix D = PSIT * PSI;
            Vector b = PSIT * yz;

            p = Matrix.Gauss(D, b);

        }

        public double GetValue(double x, DelPsi FPsi)
        {
            double res = 0;
            Vector ps = FPsi(x);
            res = ps.ScalarProduct(p);
            return res;

        }

    }
}
