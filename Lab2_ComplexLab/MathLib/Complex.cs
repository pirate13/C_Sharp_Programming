using System;

namespace MathLib
{
    public sealed class Complex : IEquatable<Complex>
    {
        public double Re { get; }
        public double Im { get; }

        public Complex(double re, double im)
        {
            Re = re;
            Im = im;
        }

        public Complex(double re) : this(re, 0) { }

        public double Magnitude => Math.Sqrt(Re * Re + Im * Im);

        public override string ToString()
        {
            var sign = Im >= 0 ? "+" : "-";
            return $"{Re} {sign} {Math.Abs(Im)}i";
        }

        public static Complex operator +(Complex a, Complex b)
            => new Complex(a.Re + b.Re, a.Im + b.Im);

        public static Complex operator -(Complex a, Complex b)
            => new Complex(a.Re - b.Re, a.Im - b.Im);

        public static Complex operator *(Complex a, Complex b)
            => new Complex(
                a.Re * b.Re - a.Im * b.Im,
                a.Re * b.Im + a.Im * b.Re
            );

        public static Complex operator /(Complex a, Complex b)
        {
            var denom = b.Re * b.Re + b.Im * b.Im;
            if (denom == 0) throw new DivideByZeroException();
            return new Complex(
                (a.Re * b.Re + a.Im * b.Im) / denom,
                (a.Im * b.Re - a.Re * b.Im) / denom
            );
        }

        public static Complex operator -(Complex a)
            => new Complex(-a.Re, -a.Im);

        public static Complex operator ++(Complex a)
            => new Complex(a.Re + 1, a.Im);

        public static Complex operator --(Complex a)
            => new Complex(a.Re - 1, a.Im);

        public bool Equals(Complex? other)
            => other is not null && Re.Equals(other.Re) && Im.Equals(other.Im);

        public override bool Equals(object? obj)
            => obj is Complex c && Equals(c);

        public override int GetHashCode()
            => HashCode.Combine(Re, Im);

        public static bool operator ==(Complex? a, Complex? b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Complex? a, Complex? b) => !(a == b);

        public static bool operator true(Complex a) => a.Re != 0 || a.Im != 0;
        public static bool operator false(Complex a) => a.Re == 0 && a.Im == 0;

        public static implicit operator double(Complex a) => a.Magnitude;
        public static explicit operator Complex(double d) => new Complex(d, 0);
    }
}
