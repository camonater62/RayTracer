using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class TestRays
    {
        private static bool float_eq(float a, float b)
        {
            const float EPSILON = 0.0001f;
            return MathF.Abs(a - b) < EPSILON;
        }

        [TestMethod]
        public void CreateAndQueryRay()
        {
            Point origin = new(1, 2, 3);
            Vector direction = new(4, 5, 6);

            Ray r = new Ray(origin, direction);
            Assert.IsTrue(r.Origin == origin);
            Assert.IsTrue(r.Direction == direction);
        }

        [TestMethod]
        public void ComputePointFromDistance()
        {
            Ray r = new(new Point(2, 3, 4), new Vector(1, 0, 0));

            Assert.IsTrue(r.Position(0) == new Point(2, 3, 4));
            Assert.IsTrue(r.Position(1) == new Point(3, 3, 4));
            Assert.IsTrue(r.Position(-1) == new Point(1, 3, 4));
            Assert.IsTrue(r.Position(2.5f) == new Point(4.5f, 3, 4));
        }

        [TestMethod]
        public void RayIntersectsSphereAt2Points()
        {
            Ray r = new(new Point(0, 0, -5), new Vector(0, 0, 1));
            Sphere s = new();

            float[] xs = s.Intersect(r);

            Assert.IsTrue(xs.Length == 2);
            Assert.IsTrue(float_eq(xs[0], 4.0f));
            Assert.IsTrue(float_eq(xs[1], 6.0f));
        }

        [TestMethod]
        public void RayInterectsSphereAtTangent()
        {
            Ray r = new(new Point(0, 1, -5), new Vector(0, 0, 1));
            Sphere s = new();

            float[] xs = s.Intersect(r);

            Assert.IsTrue(xs.Length == 2);
            Console.WriteLine(xs[0] + " " + xs[1]);
            Assert.IsTrue(float_eq(xs[0], 5.0f));
            Assert.IsTrue(float_eq(xs[1], 5.0f));
        }

        [TestMethod]
        public void RayMissesSphere()
        {
            Ray r = new(new Point(0, 2, -5), new Vector(0, 0, 1));
            Sphere s = new();

            float[] xs = s.Intersect(r);

            Assert.IsTrue(xs.Length == 0);
        }

        [TestMethod]
        public void RayOriginatesInsideASphere()
        {
            Ray r = new(new Point(0, 0, 0), new Vector(0, 0, 1));
            Sphere s = new();

            float[] xs = s.Intersect(r);

            Assert.IsTrue(xs.Length == 2);
            Assert.IsTrue(float_eq(xs[0], -1.0f));
            Assert.IsTrue(float_eq(xs[1], 1.0f));
        }

        [TestMethod]
        public void SphereIsBehindRay()
        {
            Ray r = new(new Point(0, 0, 5), new Vector(0, 0, 1));
            Sphere s = new();

            float[] xs = s.Intersect(r);

            Assert.IsTrue(xs.Length == 2);
            Assert.IsTrue(float_eq(xs[0], -6.0f));
            Assert.IsTrue(float_eq(xs[1], -4.0f));
        }
    }
}
