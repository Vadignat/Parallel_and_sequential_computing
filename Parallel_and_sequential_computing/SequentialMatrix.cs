using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel_and_sequential_computing
{
    internal class SequentialMatrix
    {
        private int n;
        private double[,] matrix;
        private Random random = new Random();

        public int N
        {
            get => n;
            set
            {
                if (value > 0)
                    n = value;
            }
        }
        public SequentialMatrix() { }
        public SequentialMatrix(int n)
        {
            this.n = n;
            matrix = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    matrix[i, j] = Math.Round(200 * random.NextDouble() - 100, 2);
        }
        public SequentialMatrix(int n, int zero)
        {
            this.n = n;
            matrix = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    matrix[i, j] = zero;
        }
        public SequentialMatrix(SequentialMatrix a)
        {
            n = a.N;
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
        private static SequentialMatrix Sum(SequentialMatrix a,SequentialMatrix b)
        {
            SequentialMatrix res = new SequentialMatrix(b.N);
            for (int i = 0; i < b.N; i++)
                for (int j = 0; j < b.N; j++)
                    res[i, j] = a[i, j] + b[i, j];
            return res;
        }
        public static SequentialMatrix operator + (SequentialMatrix a, SequentialMatrix b)
        {
            if (!(a.N == b.N))
                throw new Exception("Ошибка: размеры матриц не совпадают.");
            return Sum(a, b);
        }
        private static SequentialMatrix kMatrix(double k, SequentialMatrix a)
        {
            SequentialMatrix res = new SequentialMatrix(a.N);
            for (int i = 0; i < a.N; i++)
            {
                for (int j = 0; j < a.N; j++)
                {
                    res[i, j] = a[i, j] * k;
                }
            }
            return res;
        }
        public static SequentialMatrix operator * (double k, SequentialMatrix a)
        {
            return kMatrix(k, a);
        }
        private static SequentialMatrix Multiplication(SequentialMatrix a, SequentialMatrix b)
        {
            SequentialMatrix res = new SequentialMatrix(a.N, 0);
            for (int i = 0; i < a.N; i++)
                for (int j = 0; j < a.N; j++)
                    for (int k = 0; k < a.N; k++)
                        res[i, j] += a[i, k] * b[k, j];

            return res;
        }
        public static SequentialMatrix operator * (SequentialMatrix a, SequentialMatrix b)
        {
            if (a.N != b.N)
                throw new Exception("Матрицы нельзя перемножать.");
            return Multiplication(a, b);
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
