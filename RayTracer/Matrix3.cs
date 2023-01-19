using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Matrix3
    {
        private float[,] Values { get; set; }

        public float this[int x, int y]
        {
            get { return Values[x, y]; }
            set { Values[x, y] = value; }
        }

        public Matrix3(float a1, float a2, float a3, 
                       float b1, float b2, float b3, 
                       float c1, float c2, float c3)
        {
            Values = new float[3, 3] {
                { a1, a2, a3 },
                { b1, b2, b3 },
                { c1, c2, c3 },
            };
        }
    }
}
