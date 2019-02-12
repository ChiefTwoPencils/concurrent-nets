using System;

namespace Parallelization
{
    class Complex
    {
        public Complex(float real, float imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public float Imaginary { get; }
        public float Real { get; }

        public float Magnitude
            => (float) Math.Sqrt(Math.Pow(Real, 2.0) + Math.Pow(Imaginary, 2.0));

        public static Complex operator + (Complex l, Complex r)
            => new Complex(l.Real + r.Real, l.Imaginary + r.Imaginary);

        public static Complex operator * (Complex l, Complex r)
            => new Complex(l.Real * r.Real - l.Imaginary * r.Imaginary,
                l.Real * r.Imaginary + l.Imaginary * r.Real);
    }
}
