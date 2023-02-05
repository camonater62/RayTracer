using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Matrix3
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

        public static bool operator ==(Matrix3 left, Matrix3 right)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (!float_eq(left[y, x], right[y, x]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator !=(Matrix3 left, Matrix3 right)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (!float_eq(left[y, x], right[y, x]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Matrix2 SubMatrix(int row, int column) {
            Matrix2 M = new(0, 0, 0, 0);
            for (int y = 0; y < 3; y++) {
                if (y == row) {
                    continue;
                }

                for (int x = 0; x < 3; x++) {
                    if (x == column) {
                        continue;
                    }

                    int ty = y;
                    int tx = x;

                    if (y > row) {
                        ty--;
                    }
                    if (x > column) {
                        tx--;
                    }

                    M[ty, tx] = this[y, x]; 
                }
            }
            return M;
        }

        public float Minor(int row, int column) {
            return SubMatrix(row, column).Determinant();
        }

        public float Cofactor(int row, int column) {
            float minor = Minor(row, column);
            if ((row + column) % 2 == 1) {
                minor *= -1;
            }
            return minor;
        }

        public float Determinant() {
            float sum = 0;
            for (int x = 0; x < 3; x++) {
                sum += this[0, x] * Cofactor(0, x);
            }
            return sum;
        }

        public string ToString() {
            string s = "";

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    s += this[row, col] + " ";
                }
                s += "\n";
            }

            return s;
        }
    }
}
