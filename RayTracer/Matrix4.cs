using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Matrix4
    {
        private float[,] Values { get; set; }

        public float this[int x, int y]
        {
            get { return Values[x, y]; }
            set { Values[x, y] = value; }
        }

        public Matrix4(float a1, float a2, float a3, float a4,
                       float b1, float b2, float b3, float b4,
                       float c1, float c2, float c3, float c4,
                       float d1, float d2, float d3, float d4)
        {
            Values = new float[4, 4] {
                { a1, a2, a3, a4, },
                { b1, b2, b3, b4, },
                { c1, c2, c3, c4, },
                { d1, d2, d3, d4, },
            };
        }

        public static bool operator ==(Matrix4 left, Matrix4 right)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (left[y, x] != right[y, x])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator !=(Matrix4 left, Matrix4 right)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    if (left[y, x] != right[y, x])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static Matrix4 operator *(Matrix4 A, Matrix4 B)
        {
            Matrix4 M = new(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    M[row, col] = A[row, 0] * B[0, col] +
                                  A[row, 1] * B[1, col] +
                                  A[row, 2] * B[2, col] +
                                  A[row, 3] * B[3, col];
                }
            }

            return M;
        }

        public string ToString() {
            string s = "";

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    s += this[row, col] + " ";
                }
                s += "\n";
            }

            return s;
        }
    }
}
