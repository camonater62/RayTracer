using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tuple = RayTracer.Tuple;

namespace Tests
{
    [TestClass]
    public class TestMatrix
    {
        private bool float_eq(float a, float b)
        {
            const float EPSILON = 0.0001f;
            return MathF.Abs(a - b) < EPSILON;
        }

        [TestMethod]
        public void ConstructMatrix()
        {
            Matrix4 M = new(1,     2,     3,     4,
                            5.5f,  6.5f,  7.5f,  8.5f,
                            9,     10,    11,    12,
                            13.5f, 14.5f, 15.5f, 16.5f);
            Assert.IsTrue(M[0, 0] == 1);
            Assert.IsTrue(M[0, 3] == 4);
            Assert.IsTrue(M[1, 0] == 5.5f);
            Assert.IsTrue(M[1, 2] == 7.5f);
            Assert.IsTrue(M[2, 2] == 11);
            Assert.IsTrue(M[3, 0] == 13.5f);
            Assert.IsTrue(M[3, 2] == 15.5f);
        }

        [TestMethod]
        public void ConstructMatrix2()
        {
            Matrix2 M = new(-3,  5, 
                             1, -2);
            Assert.IsTrue(M[0, 0] == -3);
            Assert.IsTrue(M[0, 1] == 5);
            Assert.IsTrue(M[1, 0] == 1);
            Assert.IsTrue(M[1, 1] == -2);
        }

        [TestMethod]
        public void ConstructMatrix3()
        {
            Matrix3 M = new(-3,  5,  0,
                             1, -2, -7,
                             0,  1,  1);
            Assert.IsTrue(M[0, 0] == -3);
            Assert.IsTrue(M[1, 1] == -2);
            Assert.IsTrue(M[2, 2] == 1);
        }

        [TestMethod]
        public void MatrixEquality()
        {
            Matrix4 A = new(1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 2);
            Matrix4 B = new(1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 2);
            Assert.IsTrue(A == B);
        }

        [TestMethod]
        public void MatrixInequality()
        {
            Matrix4 A = new(1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 2);
            Matrix4 B = new(2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 2, 1);
            Assert.IsTrue(A != B);
        }

        [TestMethod]
        public void MatrixMultiplication()
        {
            Matrix4 A = new(1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 2);
            Matrix4 B = new(-2, 1, 2, 3, 3, 2, 1, -1, 4, 3, 6, 5, 1, 2, 7, 8);
            Matrix4 prod = A * B;
            Matrix4 correct = new(20, 22, 50, 48, 44, 54, 114, 108, 40, 58, 110, 102, 16, 26, 46, 42);
            Assert.IsTrue(prod == correct);
        }

        [TestMethod]
        public void MatrixTupleMultiplication()
        {
            Matrix4 A = new(1, 2, 3, 4, 2, 4, 4, 2, 8, 6, 4, 1, 0, 0, 0, 1);
            Tuple b = new(1, 2, 3, 1);
            Assert.IsTrue(A * b == new Tuple(18, 24, 33, 1));
        }

        [TestMethod]
        public void IdentityMatrixMultiplication()
        {
            Matrix4 A = new(0, 1, 2, 4, 1, 2, 4, 8, 2, 4, 8, 16, 4, 8, 16, 32);
            Assert.IsTrue(A * Matrix4.IDENTITY == A);

            Tuple a = new(1, 2, 3, 4);
            Assert.IsTrue(Matrix4.IDENTITY * a == a);
        }

        [TestMethod]
        public void Transpose() {
            Matrix4 A = new(0, 9, 3, 0, 9, 8, 0, 8, 1, 8, 5, 3, 0, 0, 5, 8);
            Matrix4 B = new(0, 9, 1, 0, 9, 8, 8, 0, 3, 0, 5, 5, 0, 8, 3, 8);

            Assert.IsTrue(A.Transpose() == B);
            Assert.IsTrue(A == B.Transpose());
        }

        [TestMethod]
        public void Determinant2x2() {
            Matrix2 A = new(1, 5, -3, 2);
            Assert.IsTrue(float_eq(A.Determinant(), 17));
        }

        [TestMethod]
        public void Submatrix3x3() {
            Matrix3 A = new(1, 5, 0, -3, 2, 7, 0, 6, -3);
            Matrix2 B = new(-3, 2, 0, 6);

            Assert.IsTrue(A.SubMatrix(0, 2) == B);
        }

        [TestMethod]
        public void Submatrix4x4() {
            Matrix4 A = new(-6, 1, 1, 6, -8, 5, 8, 6, -1, 0, 8, 2, -7, 1, -1, 1);
            Matrix3 B = new(-6, 1, 6, -8, 8, 6, -7, -1, 1);

            Assert.IsTrue(A.SubMatrix(2, 1) == B);
        }

        [TestMethod]
        public void Minor3x3() {
            Matrix3 A = new(3, 5, 0, 2, -1, -7, 6, -1, 5);
            Matrix2 B = A.SubMatrix(1, 0);

            Assert.IsTrue(float_eq(B.Determinant(), 25));
            Assert.IsTrue(float_eq(A.Minor(1, 0), 25));
        }

        [TestMethod]
        public void Cofactor3x3() {
            Matrix3 A = new(3, 5, 0, 2, -1, -7, 6, -1, 5);

            Assert.IsTrue(float_eq(A.Minor(0, 0), -12));
            Assert.IsTrue(float_eq(A.Cofactor(0, 0), -12));
            Assert.IsTrue(float_eq(A.Minor(1, 0), 25));
            Assert.IsTrue(float_eq(A.Cofactor(1, 0), -25));
        }

        [TestMethod]
        public void Determinant3x3() {
            Matrix3 A = new(1, 2, 6, -5, 8, -4, 2, 6, 4);

            Assert.IsTrue(float_eq(A.Cofactor(0, 0), 56));
            Assert.IsTrue(float_eq(A.Cofactor(0, 1), 12));
            Assert.IsTrue(float_eq(A.Cofactor(0, 2), -46));
            Assert.IsTrue(float_eq(A.Determinant(), -196));
        }

        [TestMethod]
        public void Determinant4x4() {
            Matrix4 A = new(-2, -8, 3, 5, 
                            -3, 1, 7, 3,
                             1, 2, -9, 6, 
                             -6, 7, 7, -9);

            Assert.IsTrue(float_eq(A.Cofactor(0, 0), 690));
            Assert.IsTrue(float_eq(A.Cofactor(0, 1), 447));
            Assert.IsTrue(float_eq(A.Cofactor(0, 2), 210));
            Assert.IsTrue(float_eq(A.Cofactor(0, 3), 51));
            Assert.IsTrue(float_eq(A.Determinant(), -4071));
        }

        [TestMethod]
        public void CheckInvertibility() {
            Matrix4 A = new(6, 4, 4, 4, 
                            5, 5, 7, 6, 
                            4, -9, 3, -7, 
                            9, 1, 7, -6);
            Assert.IsTrue(float_eq(A.Determinant(), -2120));
            Assert.IsTrue(A.Invertible());
        }

        [TestMethod]
        public void CheckNoninvertiblity() {
            Matrix4 A = new(-4, 2, -2, -3, 9, 6, 2, 6, 0, -5, 1, -5, 0, 0, 0, 0);
            Assert.IsTrue(float_eq(A.Determinant(), 0));
            Assert.IsFalse(A.Invertible());
        }

        [TestMethod]
        public void Inverse4x4() {
            Matrix4 A = new(-5, 2, 6, -8,
                            1, -5, 1, 8,
                            7, 7, -6, -7,
                            1, -3, 7, 4);
            Matrix4 B = A.Inverse();

            Assert.IsTrue(float_eq(A.Determinant(), 532));
            Assert.IsTrue(float_eq(A.Cofactor(2, 3), -160));
            Assert.IsTrue(float_eq(B[3, 2], -160.0f/532.0f));
            Assert.IsTrue(float_eq(A.Cofactor(3, 2), 105));
            Assert.IsTrue(float_eq(B[2, 3], 105f/532f));
            Assert.IsTrue(B == new Matrix4(0.21805f, 0.45113f, 0.24060f, -0.04511f,
                                           -0.80827f, -1.45677f, -0.44361f, 0.52068f,
                                           -0.07895f, -0.22368f, -0.05263f, 0.19737f,
                                           -0.52256f, -0.81391f, -0.30075f, 0.30639f));
        }

        [TestMethod]
        public void Inverse4x4_2() {
            Matrix4 A = new(8, -5, 9, 2,
                            7, 5, 6, 1,
                            -6, 0, 9, 6,
                            -3, 0, -9, -4);
            Matrix4 N = new(-0.15385f, -0.15385f, -0.28205f, -0.53846f,
                            -0.07692f, 0.12308f, 0.02564f, 0.03077f,
                            0.35897f, 0.35897f, 0.43590f, 0.92308f,
                            -0.69231f, -0.69231f, -0.76923f, -1.92308f);
            Assert.IsTrue(N == A.Inverse());
        }

        [TestMethod]
        public void Inverse4x4_3() {
            Matrix4 A = new(9, 3, 0, 9,
                            -5, -2, -6, -3,
                            -4, 9, 6, 4,
                            -7, 6, 6, 2);
            Matrix4 N = new(-0.04074f, -0.07778f, 0.14444f, -0.22222f,
                            -0.07778f, 0.03333f, 0.36667f, -0.33333f,
                            -0.02901f, -0.14630f, -0.10926f, 0.12963f,
                            0.17778f, 0.06667f, -0.26667f, 0.33333f);
            Assert.IsTrue(N == A.Inverse());
        }

        [TestMethod]
        public void MultiplyByInverse() {
            Matrix4 A = new(3, -9, 7, 3,
                            3, -8, 2, -9,
                            -4, 4, 4, 1,
                            -6, 5, -1, 1);
            Matrix4 B = new(8, 2, 2, 2,
                            3, -1, 7, 0,
                            7, 0, 5, 4,
                            6, -2, 0, 5);
            Matrix4 C = A * B;

            Assert.IsTrue(A == C * B.Inverse());
        }
    }
}
