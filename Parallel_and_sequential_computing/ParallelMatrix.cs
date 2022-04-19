using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel_and_sequential_computing
{
    internal class ParallelMatrix
    {
        private int n;
        private double[,] matrix;
        private Random random = new Random();
        private int parts;

        public int N
        {
            get => n;
            set
            {
                if (value > 0)
                {
                    n = value;
                }
            }
        }
        public ParallelMatrix() { }
        public ParallelMatrix(int n)
        {
            this.n = n;
            parts = Math.Min(16, n);
            matrix = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    matrix[i, j] = Math.Round(200 * random.NextDouble() - 100, 2);
        }
        public ParallelMatrix(int n, int zero)
        {
            this.n = n;
            parts = Math.Min(16, n);
            matrix = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    matrix[i, j] = zero;
        }
        public ParallelMatrix(ParallelMatrix a)
        {
            n = a.N;
            parts = Math.Min(8, n);
            matrix = a.matrix;
        }
        public double this[int i, int j]
        {
            get => matrix[i, j];
            set
            {
                matrix[i, j] = Math.Round(value, 2);
            }
        }
        public ParallelMatrix ParallelSum(ParallelMatrix b)
        {
            ParallelMatrix res = new ParallelMatrix(n, 0);
            int partSize = n / parts;
            int ost = n % parts;
            int st;
            int fin = 0;
            for (int i = 0; i < parts; i++)
            {
                st = fin;
                if (ost > 0)
                {
                    fin = st + partSize + 1;
                    ost--;
                }
                else
                    fin = st + partSize;
                Thread thread = new Thread(() => forSumThread(st, fin, this, b, res));
                thread.Start();
                thread.Join();
            }
            return res;
        }
        private void forSumThread(int start, int finish, ParallelMatrix a, ParallelMatrix b, ParallelMatrix c)
        {
            for (int i = start; i < finish; i++)
                for (int j = 0; j < n; j++)
                    c[i, j] = a[i, j] + b[i, j];
        }
        public static ParallelMatrix operator + (ParallelMatrix a, ParallelMatrix b)
        {
            if (!(a.N == b.N))
                throw new Exception("Ошибка: размеры матриц не совпадают.");
            return a.ParallelSum(b);
        }

        public ParallelMatrix ParallelkProduct(double k)
        {
            ParallelMatrix res = new ParallelMatrix(n, 0);
            int partSize = n / parts;
            int ost = n % parts;
            int st;
            int fin = 0;
            for (int i = 0; i < parts; i++)
            {
                st = fin;
                if (ost > 0)
                {
                    fin = st + partSize + 1;
                    ost--;
                }
                else
                    fin = st + partSize;
                Thread thread = new Thread(() => forkProductThread(st, fin, k, this, res));
                thread.Start();
                thread.Join();
            }
            return res;
        }
        private void forkProductThread(int start, int finish, double k, ParallelMatrix a, ParallelMatrix c)
        {
            for (int i = start; i < finish; i++)
                for (int j = 0; j < n; j++)
                    c[i, j] = k * a[i, j];
        }
        public static ParallelMatrix operator *(double k, ParallelMatrix a)
        {
            return a.ParallelkProduct(k);
        }
        public ParallelMatrix ParallelProduct(ParallelMatrix b)
        {
            ParallelMatrix res = new ParallelMatrix(n, 0);
            int partSize = n / parts;
            int ost = n % parts;
            int st;
            int fin = 0;
            for (int i = 0; i < parts; i++)
            {
                st = fin;
                if (ost > 0)
                {
                    fin = st + partSize + 1;
                    ost--;
                }
                else
                    fin = st + partSize;
                Thread thread = new Thread(() => forProductThread(st, fin, this, b, res));
                thread.Start();
                thread.Join();
            }
            return res;
        }
        private void forProductThread(int start, int finish, ParallelMatrix a, ParallelMatrix b, ParallelMatrix c)
        {
            for (int i = start; i < finish; i++)
                for (int j = 0; j < a.N; j++)
                    for (int k = 0; k < a.N; k++)
                        c[i, j] += a[i, k] * b[k, j];
        }
        public static ParallelMatrix operator *(ParallelMatrix a, ParallelMatrix b)
        {
            if (a.N != b.N)
                throw new Exception("Матрицы нельзя перемножать.");
            return a.ParallelProduct(b);
        }
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    s += matrix[i, j] + "\t";
                }
                s += "\n";
            }
            return s;
        }
    }
}
