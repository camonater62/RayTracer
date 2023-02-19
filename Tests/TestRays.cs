namespace Tests
{
    [TestClass]
    public class TestRays
    {

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
    }
}
