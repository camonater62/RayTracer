using Tuple = RayTracer.Tuple;

namespace Tests
{
    [TestClass]
    public class TestTuple
    {
        private bool float_eq(float a, float b)
        {
            const float EPSILON = 0.0001f;
            return MathF.Abs(a - b) < EPSILON;
        }

        [TestMethod]
        public void TupleIsPoint()
        {
            Tuple a = new(4.3f, -4.2f, 3.1f, 1.0f);
            Assert.IsTrue(float_eq(a.X, 4.3f));
            Assert.IsTrue(float_eq(a.Y, -4.2f));
            Assert.IsTrue(float_eq(a.Z, 3.1f));
            Assert.IsTrue(float_eq(a.W, 1.0f));
            Assert.IsTrue(a.IsPoint());
            Assert.IsFalse(a.IsVector());
        }

        [TestMethod]
        public void TupleIsVector()
        {
            Tuple a = new(4.3f, -4.2f, 3.1f, 0.0f);
            Assert.IsTrue(float_eq(a.X, 4.3f));
            Assert.IsTrue(float_eq(a.Y, -4.2f));
            Assert.IsTrue(float_eq(a.Z, 3.1f));
            Assert.IsTrue(float_eq(a.W, 0.0f));
            Assert.IsFalse(a.IsPoint());
            Assert.IsTrue(a.IsVector());
        }

        [TestMethod]
        public void PointConstructor()
        {
            Point p = new(4, -4, 3);
            Assert.IsTrue(p == new Tuple(4, -4, 3, 1));
        }

        [TestMethod]
        public void VectorConstructor()
        {
            Vector v = new(4, -4, 3);
            Assert.IsTrue(v == new Tuple(4, -4, 3, 0));
        }

        [TestMethod]
        public void AddTuples()
        {
            Tuple a1 = new(3, -2, 5, 1);
            Tuple a2 = new(-2, 3, 1, 0);
            Assert.IsTrue(a1 + a2 == new Tuple(1, 1, 6, 1));
        }

        [TestMethod]
        public void SubractPoints()
        {
            Point p1 = new(3, 2, 1);
            Point p2 = new(5, 6, 7);
            Assert.IsTrue(p1 - p2 == new Vector(-2, -4, -6));
        }

        [TestMethod]
        public void SubtractVectorFromPoint()
        {
            Point p = new(3, 2, 1);
            Vector v = new(5, 6, 7);
            Assert.IsTrue(p - v == new Point(-2, -4, -6));
        }

        [TestMethod]
        public void SubtractVectors()
        {
            Vector v1 = new(3, 2, 1);
            Vector v2 = new(5, 6, 7);
            Assert.IsTrue(v1 - v2 == new Vector(-2, -4, -6));
        }

        [TestMethod]
        public void SubtractVectorFromZero()
        {
            Vector zero = new(0, 0, 0);
            Vector v = new(1, -2, 3);
            Assert.IsTrue(zero - v == new Vector(-1, 2, -3));
        }

        [TestMethod]
        public void NegateTuple()
        {
            Tuple a = new(1, -2, 3, -4);
            Assert.IsTrue(-a == new Tuple(-1, 2, -3, 4));
        }

        [TestMethod]
        public void MultiplyTupleByScalar()
        {
            Tuple a = new(1, -2, 3, -4);
            Assert.IsTrue(a * 3.5f == new Tuple(3.5f, -7, 10.5f, -14));
        }

        [TestMethod]
        public void DivideTupleByScalar()
        {
            Tuple a = new(1, -2, 3, -4);
            Assert.IsTrue(a / 2 == new Tuple(0.5f, -1, 1.5f, -2));
        }

        [TestMethod]
        public void VectorMagnitude1()
        {
            Vector v = new(1, 0, 0);
            Assert.IsTrue(float_eq(v.Mag(), 1));
        }

        [TestMethod]
        public void VectorMagnitude2()
        {
            Vector v = new(0, 1, 0);
            Assert.IsTrue(float_eq(v.Mag(), 1));
        }

        [TestMethod]
        public void VectorMagnitude3()
        {
            Vector v = new(0, 0, 1);
            Assert.IsTrue(float_eq(v.Mag(), 1));
        }

        [TestMethod]
        public void VectorMagnitude4()
        {
            Vector v = new(1, 2, 3);
            Assert.IsTrue(float_eq(v.Mag(), MathF.Sqrt(14)));
        }

        [TestMethod]
        public void VectorMagnitude5()
        {
            Vector v = new(-1, -2, -3);
            Assert.IsTrue(float_eq(v.Mag(), MathF.Sqrt(14)));
        }

        [TestMethod]
        public void TupleNormalize1()
        {
            Vector v = new(4, 0, 0);
            Assert.IsTrue(v.Normalize() == new Vector(1, 0, 0));
        }

        [TestMethod]
        public void TupleNormalize2()
        {
            Vector v = new(1, 2, 3);
            Assert.IsTrue(v.Normalize() == new Vector(0.26726f, 0.53452f, 0.80178f));
        }

        [TestMethod]
        public void TupleNormalize3()
        {
            Vector v = new(1, 2, 3);
            Vector norm = new(v.Normalize());
            Assert.IsTrue(float_eq(norm.Mag(), 1));
        }

        [TestMethod]
        public void DotProduct()
        {
            Vector a = new(1, 2, 3);
            Vector b = new(2, 3, 4);
            Assert.IsTrue(float_eq(a.Dot(b), 20));
        }

        [TestMethod]
        public void CrossProduct()
        {
            Vector a = new(1, 2, 3);
            Vector b = new(2, 3, 4);
            Assert.IsTrue(a.Cross(b) == new Vector(-1, 2, -1));
            Assert.IsTrue(b.Cross(a) == new Vector(1, -2, 1));
        }

        [TestMethod]
        public void Color()
        {
            Color c = new(-0.5f, 0.4f, 1.7f);
            Assert.IsTrue(float_eq(c.R, -0.5f));
            Assert.IsTrue(float_eq(c.G, 0.4f));
            Assert.IsTrue(float_eq(c.B, 1.7f));
        }

        [TestMethod]
        public void AddColors()
        {
            Color c1 = new(0.9f, 0.6f, 0.75f);
            Color c2 = new(0.7f, 0.1f, 0.25f);
            Assert.IsTrue(c1 + c2 == new Color(1.6f, 0.7f, 1.0f));
        }

        [TestMethod]
        public void SubColors()
        {
            Color c1 = new(0.9f, 0.6f, 0.75f);
            Color c2 = new(0.7f, 0.1f, 0.25f);
            Assert.IsTrue(c1 - c2 == new Color(0.2f, 0.5f, 0.5f));
        }

        [TestMethod]
        public void MultiplyColorByScalar()
        {
            Color c = new(0.2f, 0.3f, 0.4f);
            Assert.IsTrue(c * 2 == new Color(0.4f, 0.6f, 0.8f));
        }

        [TestMethod]
        public void MultiplyColors()
        {
            Color c1 = new(1, 0.2f, 0.4f);
            Color c2 = new(0.9f, 1, 0.1f);
            Assert.IsTrue(c1 * c2 == new Color(0.9f, 0.2f, 0.04f));
        }
    }
}
