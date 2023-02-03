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
    }
}
