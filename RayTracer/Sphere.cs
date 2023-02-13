using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Sphere
    {
        public Sphere()
        {

        }

        public float[] Intersect(Ray ray)
        {
            Vector sphere_to_ray = ray.Origin - new Point(0, 0, 0);
            float a = Vector.Dot(ray.Direction, ray.Direction);
            float b = 2 * Vector.Dot(ray.Direction, sphere_to_ray);
            float c = Vector.Dot(sphere_to_ray, sphere_to_ray) - 1;

            float discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return Array.Empty<float>();
            }

            float t1 = (-b - MathF.Sqrt(discriminant)) / (2 * a);
            float t2 = (-b + MathF.Sqrt(discriminant)) / (2 * a);

            return new float[]{ t1, t2 };
        }
    }
}
