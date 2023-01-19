using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Matrix2
    {
        private float[,] Values { get; set; }

        public float this[int x, int y]
        {
            get { return Values[x, y]; }
            set { Values[x, y] = value; }
        }

        public Matrix2(float a1, float a2,
                       float b1, float b2)
        {
            Values = new float[2, 2] {
                { a1, a2 },
                { b1, b2 },
            };
        }
    }
}
