using System;
using System.Globalization;
using System.Linq;

namespace VectorLib
{
    public sealed class VectorN : IEquatable<VectorN>
    {
        public int Dimension { get; }
        public double[] Coords { get; }

        public VectorN(int dimension)
        {
            if (dimension <= 0)
                throw new ArgumentException("Dimension must be positive.", nameof(dimension));

            Dimension = dimension;
            Coords = new double[dimension];
        }

        public VectorN(double[] coords)
        {
            if (coords is null)
                throw new ArgumentNullException(nameof(coords));
            if (coords.Length == 0)
                throw new ArgumentException("Vector must have at least one coordinate.", nameof(coords));

            Dimension = coords.Length;
            Coords = coords.ToArray();  
        }

        public double this[int i]
        {
            get => Coords[i];
            set => Coords[i] = value;
        }

        public VectorN Add(VectorN other)
        {
            EnsureSameDim(other);
            var res = new double[Dimension];
            for (int i = 0; i < Dimension; i++)
                res[i] = Coords[i] + other!.Coords[i];
            return new VectorN(res);
        }

        public VectorN Subtract(VectorN other)
        {
            EnsureSameDim(other);
            var res = new double[Dimension];
            for (int i = 0; i < Dimension; i++)
                res[i] = Coords[i] - other!.Coords[i];
            return new VectorN(res);
        }

        public VectorN Multiply(double k)
        {
            var res = new double[Dimension];
            for (int i = 0; i < Dimension; i++)
                res[i] = Coords[i] * k;
            return new VectorN(res);
        }

        public double Magnitude()
        {
            double sum = 0;
            for (int i = 0; i < Dimension; i++)
                sum += Coords[i] * Coords[i];
            return Math.Sqrt(sum);
        }

        public double Dot(VectorN other)
        {
            EnsureSameDim(other);
            double sum = 0;
            for (int i = 0; i < Dimension; i++)
                sum += Coords[i] * other!.Coords[i];
            return sum;
        }

        public override string ToString()
            => $"({string.Join(", ", Coords.Select(c => c.ToString(CultureInfo.InvariantCulture)))})";

        public bool Equals(VectorN? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (Dimension != other.Dimension) return false;

            for (int i = 0; i < Dimension; i++)
                if (!Coords[i].Equals(other.Coords[i]))
                    return false;

            return true;
        }

        public override bool Equals(object? obj) => obj is VectorN v && Equals(v);

        public override int GetHashCode()
        {
            var h = new HashCode();
            h.Add(Dimension);
            for (int i = 0; i < Dimension; i++) h.Add(Coords[i]);
            return h.ToHashCode();
        }

        private void EnsureSameDim(VectorN? other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other));

            if (Dimension != other.Dimension)
                throw new ArgumentException("Vectors must have the same dimension.", nameof(other));
        }
    }
}
