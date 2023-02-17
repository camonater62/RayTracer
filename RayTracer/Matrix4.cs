using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer
{
    public class Matrix4
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

        public static Matrix4 IDENTITY { get; } = new(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);

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
                    if (!float_eq(left[y, x], right[y, x]))
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
                    if (!float_eq(left[y, x], right[y, x]))
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

        public Matrix4 Transpose() {
            return new Matrix4(
                this[0, 0], this[1, 0], this[2, 0], this[3, 0],
                this[0, 1], this[1, 1], this[2, 1], this[3, 1],
                this[0, 2], this[1, 2], this[2, 2], this[3, 2],
                this[0, 3], this[1, 3], this[2, 3], this[3, 3]
            );
        }

        public Matrix3 SubMatrix(int row, int column) {
            Matrix3 M = new(0, 0, 0, 0, 0, 0, 0, 0, 0);
            for (int y = 0; y < 4; y++) {
                if (y == row) {
                    continue;
                }

                for (int x = 0; x < 4; x++) {
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
            for (int x = 0; x < 4; x++) {
                sum += this[0, x] * Cofactor(0, x);
            }
            return sum;
        }

        public bool Invertible() {
            return !float_eq(Determinant(), 0);
        }

        public Matrix4 Inverse() {
            float det = Determinant();

            if (float_eq(det, 0)) {
                throw new InvalidOperationException("This matrix is not invertible");
            }

            Matrix4 M = new(0, 0, 0, 0, 0, 0, 0,0 , 0, 0, 0, 0, 0, 0, 0, 0);

            for (int y = 0; y < 4; y++) {
                for (int x = 0; x < 4; x++) {
                    M[x, y] = Cofactor(y, x) / det;
                }
            }
            
            return M;
        }

        public static Matrix4 Translation(float x, float y, float z)
        {
            return new Matrix4(
                1, 0, 0, x,
                0, 1, 0, y,
                0, 0, 1, z,
                0, 0, 0, 1
            );
        }

        public static Matrix4 Scaling(float x, float y, float z)
        {
            return new Matrix4(
                x, 0, 0, 0,
                0, y, 0, 0,
                0, 0, z, 0,
                0, 0, 0, 1
            );
        }

        public static Matrix4 RotationX(float r)
        {
            return new Matrix4(
                1, 0, 0, 0,
                0, MathF.Cos(r), -MathF.Sin(r), 0,
                0, MathF.Sin(r), MathF.Cos(r), 0,
                0, 0, 0, 1
            );
        }

        public static Matrix4 RotationY(float r)
        {
            return new Matrix4(
                MathF.Cos(r), 0, MathF.Sin(r), 0,
                0, 1, 0, 0,
                -MathF.Sin(r), 0, MathF.Cos(r), 0,
                0, 0, 0, 1
            );
        }

        public static Matrix4 RotationZ(float r)
        {
            return new Matrix4(
                MathF.Cos(r), -MathF.Sin(r), 0, 0,
                MathF.Sin(r), MathF.Cos(r), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            );
        }

        public static Matrix4 Shearing(float xy, float xz, float yx, float yz, float zx, float zy)
        {
            return new Matrix4(
                1, xy, xz, 0,
                yx, 1, yz, 0,
                zx, zy, 1, 0,
                0, 0, 0, 1
            );
        }

        public override string ToString() {
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

        public override bool Equals(object? obj)
        {
            if (obj != null && obj is Matrix4) {
                return this == (Matrix4) obj;
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
