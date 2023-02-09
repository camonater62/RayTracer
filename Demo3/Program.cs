using RayTracer;
using Tuple = RayTracer.Tuple;

namespace Demo3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Canvas c = new(50, 50);

            int count = 12;

            float step = 2 * MathF.PI / count;

            Color color = new(1, 1, 1);

            for (int i = 0; i < count; i++)
            {
                float angle = i * step;
                Matrix4 T = Matrix4.Translation(25, 25, 0) * Matrix4.RotationZ(angle) * Matrix4.Translation(0, 20, 0);
                Tuple p = T * new Point(0, 1, 0);
                c.Write((int)p.X, (int)p.Y, color);
            }

            File.WriteAllText("out.ppm", c.ToPPM());
        }
    }
}