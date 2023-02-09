using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class TestTransformations
    {
        [TestMethod]
        public void MultiplyByTranslationMatrix()
        {
            Matrix4 transform = Matrix4.Translation(5, -3, 2);
            Point p = new(-3, 4, 5);

            Assert.IsTrue(transform * p == new Point(2, 1, 7));
        }

        [TestMethod]
        public void MultiplyByInverseTranslationMatrix()
        {
            Matrix4 transform = Matrix4.Translation(5, -3, 2);
            Matrix4 inv = transform.Inverse();
            Point p = new(-3, 4, 5);

            Assert.IsTrue(inv * p == new Point(-8, 7, 3));
        }

        [TestMethod]
        public void TranslationDoesNotAffectVector()
        {
            Matrix4 transform = Matrix4.Translation(5, -3, 2);
            Vector v = new(-3, 4, 5);

            Assert.IsTrue(transform * v == v);
        }

        [TestMethod]
        public void ScalingMatrixAppliedToPoint()
        {
            Matrix4 transform = Matrix4.Scaling(2, 3, 4);
            Point p = new(-4, 6, 8);

            Assert.IsTrue(transform * p == new Point(-8, 18, 32));
        }

        [TestMethod]
        public void ScalingMatrixAppliedToVector()
        {
            Matrix4 transform = Matrix4.Scaling(2, 3, 4);
            Vector v = new(-4, 6, 8);

            Assert.IsTrue(transform * v == new Vector(-8, 18, 32));
        }

        [TestMethod]
        public void MultiplyByInverseScalingMatrix()
        {
            Matrix4 transform = Matrix4.Scaling(2, 3, 4);
            Matrix4 inv = transform.Inverse();
            Vector v = new(-4, 6, 8);

            Assert.IsTrue(inv * v == new Vector(-2, 2, 2));
        }

        [TestMethod]
        public void ReflectionIsNegativeScaling()
        {
            Matrix4 transform = Matrix4.Scaling(-1, 1, 1);
            Point p = new(2, 3, 4);

            Assert.IsTrue(transform * p == new Point(-2, 3, 4));
        }

        [TestMethod]
        public void RotatePointAroundXAxis()
        {
            Point p = new(0, 1, 0);
            Matrix4 half_quarter = Matrix4.RotationX(MathF.PI / 4);
            Matrix4 full_quarter = Matrix4.RotationX(MathF.PI / 2);

            Assert.IsTrue(half_quarter * p == new Point(0, MathF.Sqrt(2) / 2, MathF.Sqrt(2) / 2));
            Assert.IsTrue(full_quarter * p == new Point(0, 0, 1));
        }

        [TestMethod]
        public void InverseRotateIsOppositeDirection()
        {
            Point p = new(0, 1, 0);
            Matrix4 half_quarter = Matrix4.RotationX(MathF.PI / 4);
            Matrix4 inv = half_quarter.Inverse();

            Assert.IsTrue(inv * p == new Point(0, MathF.Sqrt(2) / 2, -MathF.Sqrt(2) / 2));
        }

        [TestMethod]
        public void RotatePointAroundYAxis()
        {
            Point p = new(0, 0, 1);
            Matrix4 half_quarter = Matrix4.RotationY(MathF.PI / 4);
            Matrix4 full_quarter = Matrix4.RotationY(MathF.PI / 2);

            Assert.IsTrue(half_quarter * p == new Point(MathF.Sqrt(2) / 2, 0, MathF.Sqrt(2) / 2));
            Assert.IsTrue(full_quarter * p == new Point(1, 0, 0));
        }

        [TestMethod]
        public void RotatePointAroundZAxis() {
            Point p = new(0, 1, 0);
            Matrix4 half_quarter = Matrix4.RotationZ(MathF.PI / 4);
            Matrix4 full_quarter = Matrix4.RotationZ(MathF.PI / 2);

            Assert.IsTrue(half_quarter * p == new Point(-MathF.Sqrt(2) / 2, MathF.Sqrt(2) / 2, 0));
            Assert.IsTrue(full_quarter * p == new Point(-1, 0, 0));
        }

        [TestMethod]
        public void ShearingTransformMovesXInProportionToY()
        {
            Matrix4 transform = Matrix4.Shearing(1, 0, 0, 0, 0, 0);
            Point p = new(2, 3, 4);

            Assert.IsTrue(transform * p == new Point(5, 3, 4));
        }

        [TestMethod]
        public void ShearingTransformMovesXInProportionToZ()
        {
            Matrix4 transform = Matrix4.Shearing(0, 1, 0, 0, 0, 0);
            Point p = new(2, 3, 4);

            Assert.IsTrue(transform * p == new Point(6, 3, 4));
        }

        [TestMethod]
        public void ShearingTransformMovesYInProportionToX()
        {
            Matrix4 transform = Matrix4.Shearing(0, 0, 1, 0, 0, 0);
            Point p = new(2, 3, 4);

            Assert.IsTrue(transform * p == new Point(2, 5, 4));
        }

        [TestMethod]
        public void ShearingTransformMovesYInProportionToZ()
        {
            Matrix4 transform = Matrix4.Shearing(0, 0, 0, 1, 0, 0);
            Point p = new(2, 3, 4);

            Assert.IsTrue(transform * p == new Point(2, 7, 4));
        }

        [TestMethod]
        public void ShearingTransformMovesZInProportionToX()
        {
            Matrix4 transform = Matrix4.Shearing(0, 0, 0, 0, 1, 0);
            Point p = new(2, 3, 4);

            Assert.IsTrue(transform * p == new Point(2, 3, 6));
        }

        [TestMethod]
        public void ShearingTransformMovesZInProportionToY()
        {
            Matrix4 transform = Matrix4.Shearing(0, 0, 0, 0, 0, 1);
            Point p = new(2, 3, 4);

            Assert.IsTrue(transform * p == new Point(2, 3, 7));
        }

        [TestMethod]
        public void ApplyTransformationsInSequence()
        {
            Point p = new(1, 0, 1);
            Matrix4 A = Matrix4.RotationX(MathF.PI / 2);
            Matrix4 B = Matrix4.Scaling(5, 5, 5);
            Matrix4 C = Matrix4.Translation(10, 5, 7);

            // Apply rotation first
            Point p2 = new Point(A * p);
            Assert.IsTrue(p2 == new Point(1, -1, 0));

            // then apply scaling
            Point p3 = new Point(B * p2);
            Assert.IsTrue(p3 == new Point(5, -5, 0));

            // then apply translation
            Point p4 = new Point(C * p3);
            Assert.IsTrue(p4 == new Point(15, 0, 7));
        }

        [TestMethod]
        public void ChainedTransformations()
        {
            Point p = new(1, 0, 1);
            Matrix4 A = Matrix4.RotationX(MathF.PI / 2);
            Matrix4 B = Matrix4.Scaling(5, 5, 5);
            Matrix4 C = Matrix4.Translation(10, 5, 7);

            Matrix4 T = C * B * A;
            Assert.IsTrue(T * p == new Point(15, 0, 7));
        }
    }
}
