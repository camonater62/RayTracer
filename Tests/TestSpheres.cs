namespace Tests
{
    [TestClass]
    public class TestSpheres
    {
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
