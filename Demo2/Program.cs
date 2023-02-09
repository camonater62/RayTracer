using RayTracer;

namespace Demo2
{
    internal class Program
    {
        struct Projectile
        {
            public Point position;
            public Vector velocity;
        }
        struct Environment
        {
            public Vector gravity;
            public Vector wind;
        }
        static Projectile tick(Environment env, Projectile proj)
        {
            Point position = proj.position + proj.velocity;
            Vector velocity = proj.velocity + env.gravity + env.wind;
            return new Projectile { position = position, velocity = velocity };
        }

        public static void Main(string[] args)
        {
            Projectile p = new();
            p.position = new(0, 1, 0);
            p.velocity = new Vector(1, 1.8f, 0).Normalize() * 11.25f;

            Environment e = new();
            e.gravity = new(0, -0.1f, 0);
            e.wind = new(-0.01f, 0, 0);

            Canvas c = new(900, 550);

            while (p.position.Y > 0)
            {
                p = tick(e, p);
                c.Write((int) p.position.X, c.Height - (int)p.position.Y, new Color(1, 0.647f, 0));
                Console.WriteLine(p.position);
            }

            File.WriteAllText("out.ppm", c.ToPPM());
        }
    }
}