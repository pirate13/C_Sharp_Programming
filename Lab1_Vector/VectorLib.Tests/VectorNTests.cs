using System;
using Xunit;
using VectorLib;

namespace VectorLib.Tests
{
    public class VectorNTests
    {
        [Fact]
        public void Ctor_WithDimension_CreatesZeroVector()
        {
            var v = new VectorN(3);
            Assert.Equal(3, v.Dimension);
            Assert.Equal(new[] { 0.0, 0.0, 0.0 }, v.Coords);
        }

        [Fact]
        public void Ctor_DimensionNonPositive_Throws()
        {
            Assert.Throws<ArgumentException>(() => new VectorN(0));
            Assert.Throws<ArgumentException>(() => new VectorN(-2));
        }

        [Fact]
        public void Ctor_WithArray_CopiesInput()
        {
            var arr = new[] { 1.0, 2.0, 3.0 };
            var v = new VectorN(arr);
            arr[0] = 99.0;
            Assert.Equal(1.0, v[0]);
        }

        [Fact]
        public void Ctor_NullArray_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new VectorN((double[])null!));
        }

        [Fact]
        public void Ctor_EmptyArray_Throws()
        {
            Assert.Throws<ArgumentException>(() => new VectorN(Array.Empty<double>()));
        }

        [Fact]
        public void Indexer_GetSet_Works()
        {
            var v = new VectorN(2);
            v[0] = 5;
            v[1] = -1;
            Assert.Equal(5, v[0]);
            Assert.Equal(-1, v[1]);
        }

        [Fact]
        public void Add_SameDimension_ReturnsCorrect()
        {
            var a = new VectorN(new[] { 1.0, 2.0 });
            var b = new VectorN(new[] { 3.0, 4.0 });
            Assert.Equal(new VectorN(new[] { 4.0, 6.0 }), a.Add(b));
        }

        [Fact]
        public void Add_DifferentDimension_Throws()
        {
            var a = new VectorN(new[] { 1.0, 2.0 });
            var b = new VectorN(new[] { 1.0, 2.0, 3.0 });
            Assert.Throws<ArgumentException>(() => a.Add(b));
        }

        [Fact]
        public void Subtract_SameDimension_ReturnsCorrect()
        {
            var a = new VectorN(new[] { 5.0, 1.0 });
            var b = new VectorN(new[] { 2.0, 3.0 });
            Assert.Equal(new VectorN(new[] { 3.0, -2.0 }), a.Subtract(b));
        }

        [Fact]
        public void Multiply_ByZero_ReturnsZeroVector()
        {
            var a = new VectorN(new[] { 1.0, -2.0, 3.0 });
            Assert.Equal(new VectorN(new[] { 0.0, 0.0, 0.0 }), a.Multiply(0));
        }

        [Fact]
        public void Multiply_ByNegative_Works()
        {
            var a = new VectorN(new[] { 1.0, -2.0 });
            Assert.Equal(new VectorN(new[] { -2.0, 4.0 }), a.Multiply(-2));
        }

        [Fact]
        public void Magnitude_ZeroVector_IsZero()
        {
            var a = new VectorN(4);
            Assert.Equal(0.0, a.Magnitude(), 10);
        }

        [Fact]
        public void Magnitude_KnownVector_3_4_Is5()
        {
            var a = new VectorN(new[] { 3.0, 4.0 });
            Assert.Equal(5.0, a.Magnitude(), 10);
        }

        [Fact]
        public void Dot_SameDimension_ReturnsCorrect()
        {
            var a = new VectorN(new[] { 1.0, 2.0, 3.0 });
            var b = new VectorN(new[] { 4.0, 5.0, 6.0 });
            Assert.Equal(32.0, a.Dot(b), 10);
        }

        [Fact]
        public void Dot_OrthogonalVectors_IsZero()
        {
            var a = new VectorN(new[] { 1.0, 0.0 });
            var b = new VectorN(new[] { 0.0, 1.0 });
            Assert.Equal(0.0, a.Dot(b), 10);
        }

        [Fact]
        public void Dot_DifferentDimension_Throws()
        {
            var a = new VectorN(new[] { 1.0, 2.0 });
            var b = new VectorN(new[] { 1.0, 2.0, 3.0 });
            Assert.Throws<ArgumentException>(() => a.Dot(b));
        }

        [Fact]
        public void Equals_SameValues_IsTrue()
        {
            var a = new VectorN(new[] { 1.0, 2.0 });
            var b = new VectorN(new[] { 1.0, 2.0 });
            Assert.True(a.Equals(b));
            Assert.Equal(a, b);
        }

        [Fact]
        public void Equals_DifferentValues_IsFalse()
        {
            var a = new VectorN(new[] { 1.0, 2.0 });
            var b = new VectorN(new[] { 1.0, 2.1 });
            Assert.NotEqual(a, b);
        }
    }
}
