namespace Tests
{
    [TestClass]
    public class TestCanvas
    {
        [TestMethod]
        public void ConstructCanvas()
        {
            Canvas c = new(10, 20);
            Assert.IsTrue(c.Width == 10);
            Assert.IsTrue(c.Height == 20);

            Color black = new(0, 0, 0);
            foreach (Color p in c.Pixels)
            {
                Assert.IsTrue(p == black);
            }
        }

        [TestMethod]
        public void WritePixels()
        {
            Canvas c = new(10, 20);
            Color red = new(1, 0, 0);
            c.Write(2, 3, red);
            Assert.IsTrue(c.At(2, 3) == red);
        }

        [TestMethod]
        public void ConstructPPMHeader()
        {
            Canvas c = new(5, 3);
            string ppm = c.ToPPM();

            StringAssert.StartsWith(ppm, "P3\n5 3\n255");
        }

        [TestMethod]
        public void ConstructPPMPixelData()
        {
            Canvas c = new(5, 3);
            Color c1 = new(1.5f, 0, 0);
            Color c2 = new(0, 0.5f, 0);
            Color c3 = new(-0.5f, 0, 1);
            c.Write(0, 0, c1);
            c.Write(2, 1, c2);
            c.Write(4, 2, c3);
            string ppm = c.ToPPM();

            string goal = "P3\n" + "5 3\n" + "255\n" + "255 0 0 0 0 0 0 0 0 0 0 0 0 0 0\n" + "0 0 0 0 0 0 0 128 0 0 0 0 0 0 0\n" + "0 0 0 0 0 0 0 0 0 0 0 0 0 0 255\n";

            Assert.IsTrue(ppm == goal);
        }

        [TestMethod]
        public void SplitLongLinesInPPMFiles()
        {
            Canvas c = new(10, 2);
            Color c1 = new(1, 0.8f, 0.6f);
            for (int y = 0; y < c.Height; y++)
            {
                for (int x = 0; x < c.Width; x++)
                {
                    c.Write(x, y, c1);
                }
            }

            string goal = "P3\n10 2\n255\n255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n153 255 204 153 255 204 153 255 204 153 255 204 153\n255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204\n153 255 204 153 255 204 153 255 204 153 255 204 153\n";

            Assert.IsTrue(c.ToPPM() == goal);
        }

        [TestMethod]
        public void PPMEndsWithNewline()
        {
            Canvas c = new(5, 3);
            string ppm = c.ToPPM();
            Assert.IsTrue(ppm.EndsWith("\n"));
        }
    }
}
