using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Matrix2
    {
        private static bool float_eq(float a, float b)
        {
            const float EPSILON = 0.0001f;
            return MathF.Abs(a - b) < EPSILON;
        }

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

        public static bool operator ==(Matrix2 left, Matrix2 right)
        {
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    if (!float_eq(left[y, x], right[y, x]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator !=(Matrix2 left, Matrix2 right)
        {
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    if (!float_eq(left[y, x], right[y, x]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public float Determinant() {
            return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
        }

        public override string ToString() {
            string s = "";

            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 2; col++)
                {
                    s += this[row, col] + " ";
                }
                s += "\n";
            }

            return s;
        }

        public override bool Equals(object? obj)
        {
            if (obj != null && obj is Matrix2) {
                return this == (Matrix2) obj;
            }
            return false;
        }

        public override int GetHashCode()
        {
            // The easy way
            return ToString().GetHashCode();
        }
    }
}
