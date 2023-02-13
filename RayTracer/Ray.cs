using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Ray
    {
        public Point Origin { get; set; }
        public Vector Direction { get; set; }
        
        public Ray(Point origin, Vector direction) { 
            Origin = origin; 
            Direction = direction;
        }

        public Point Position(float t)
        {
            return Origin + Direction * t;
        }
    }
}
