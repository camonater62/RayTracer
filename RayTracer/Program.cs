namespace RayTracer
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
            return new Projectile { position = position, velocity= velocity };
        }
        static void Main(string[] args)
        {
            Projectile p = new Projectile();
            p.position = new Point(0, 1, 0);
            p.velocity = new Vector(1, 1, 0).Normalize();
            Environment e = new Environment();
            e.gravity = new Vector(0, -0.1f, 0);
            e.wind = new Vector(-0.01f, 0, 0);

            while (p.position.Y > 0) { 
                p = tick(e, p);
                Console.WriteLine(p.position);
            }
        }
    }
}