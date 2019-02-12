using System;

namespace Parallelization
{
    class Mandelbrot
    {
        private const int Rows = 2000;
        private const int Columns = 2000;
        private static readonly Complex Center = new Complex(-0.75f, 0.0f);

        private const float Width = 2.5f;
        private const float Height = 2.5f;

        private static float ComputeRow(int col)
            => Center.Real - Width / 2.0f + col * Width / Columns;

        private static float ComputeColumn(int row)
            => Center.Imaginary - Height / 2.0f + row * Height / Rows;

        public Func<Complex, int, bool> IsMandelbrot = (complex, iterations) =>
        {
            var z = new Complex(0.0f, 0.0f);
            int acc = 0;
            while (acc < iterations && z.Magnitude < 2.0)
            {
                z = z * z + complex;
                acc += 1;
            }
            return acc == iterations;
        };
    }
}