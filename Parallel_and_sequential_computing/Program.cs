using System.Diagnostics;
namespace Parallel_and_sequential_computing
{
    class MainProgramm
    {
        static void Main()
        {
            Console.WriteLine("Введите размеры квадратных матриц: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Stopwatch stopwatch = new Stopwatch();

            SequentialMatrix a1 = new SequentialMatrix(n);
            SequentialMatrix b1 = new SequentialMatrix(n);
            //Console.WriteLine(a1);
            //Console.WriteLine(b1);
            stopwatch.Start();
            SequentialMatrix sm = new(a1 + b1);
            stopwatch.Stop();
            Console.WriteLine("Sequential sum time: {0} ms \n", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            ParallelMatrix a2 = new ParallelMatrix(n);
            ParallelMatrix b2 = new ParallelMatrix(n);
            //Console.WriteLine(a2);
            //Console.WriteLine(b2);
            stopwatch.Start();
            ParallelMatrix pm = new(a2 + b2);
            stopwatch.Stop();
            Console.WriteLine("Parallel sum time: {0} ms \n", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            stopwatch.Start();
            sm = 11.3 * b1;
            stopwatch.Stop();
            Console.WriteLine("Sequential k-product time: {0} ms \n", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            stopwatch.Start();
            pm = 11.3 * b2;
            stopwatch.Stop();
            Console.WriteLine("Parallel k-product time: {0} ms \n", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            stopwatch.Start();
            sm = a1 * b1;
            stopwatch.Stop();
            Console.WriteLine("Sequential product time: {0} ms \n", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

            stopwatch.Start();
            pm = a2 * b2;
            stopwatch.Stop();
            Console.WriteLine("Parallel product time: {0} ms \n", stopwatch.ElapsedMilliseconds);
            stopwatch.Reset();

        }
    }
}