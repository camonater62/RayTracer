﻿namespace RayTracer
{
    public class Tuple
    {
        protected static bool float_eq(float a, float b)
        {
            const float EPSILON = 0.0001f;
            return MathF.Abs(a - b) < EPSILON;
        }

        private float x, y, z, w;

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
        public float Z { get => z; set => z = value; }
        public float W { get => w; set => w = value; }

        public Tuple(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public bool IsPoint()
        {
            return float_eq(w, 1.0f);
        }

        public bool IsVector()
        {
            return float_eq(w, 0.0f);
        }

        public static bool operator ==(Tuple a, Tuple b)
        {
            return float_eq(a.X, b.X) && float_eq(a.Y, b.Y) && float_eq(a.Z, b.Z) && float_eq(a.W, b.W);
        }

        public static bool operator !=(Tuple a, Tuple b)
        {
            return !float_eq(a.X, b.X) || !float_eq(a.Y, b.Y) || !float_eq(a.Z, b.Z) || !float_eq(a.W, b.W);
        }

        public static Tuple operator -(Tuple a)
        {
            return new Tuple(-a.X, -a.Y, -a.Z, -a.W);
        }

        public static Tuple operator +(Tuple a, Tuple b)
        {
            return new Tuple(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        public static Tuple operator -(Tuple a, Tuple b)
        {
            return new Tuple(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        public static Tuple operator *(Tuple a, float f)
        {
            return new Tuple(a.X * f, a.Y * f, a.Z* f, a.W * f);
        }

        public static Tuple operator /(Tuple a, float f)
        {
            if (f == 0.0f)
            {
                throw new DivideByZeroException();
            }
            return new Tuple(a.X / f, a.Y / f, a.Z/ f, a.W / f);
        }

        public static float Magnitude(Tuple a)
        {
            return a.Mag();
        }

        public float Mag()
        {
            return MathF.Sqrt(X * X + Y * Y + Z * Z + W * W);
        }

        public static Tuple Normalize(Tuple a)
        {
            return a.Normalize();
        }

        public Tuple Normalize()
        {
            float mag = this.Mag();
            return new Tuple(X / mag, Y / mag, Z / mag, W / mag);
        }

        public static float Dot(Tuple a, Tuple b) {
            return a.Dot(b);
        }

        public float Dot(Tuple a)
        {
            return x*a.x + y*a.y + z*a.z + w*a.w;
        }

        
        
    }

    public class Point : Tuple
    {
        public Point(float x, float y, float z) : base(x, y, z, 1.0f)
        {

        }

        public float W { get; private set; }

        public Point(Tuple a) : base(a.X, a.Y, a.Z, a.W)
        {
            if (!float_eq(a.W, 1.0f))
            {
                throw new ArgumentException("Provided tuple is not a valid point, w != 1");
            }
        }
    }

    public class Vector : Tuple
    {
        public Vector(float x, float y, float z) : base(x, y, z, 0.0f)
        {

        }

        public float W { get; private set; }
        
        public Vector(Tuple a) : base(a.X, a.Y, a.Z, a.W)
        {
            if (!float_eq(a.W, 0.0f))
            {
                throw new ArgumentException("Provided tuple is not a valid vector, w != 0");
            }
        }

        public static Vector Cross(Vector a, Vector b)
        {
            return a.Cross(b);
        }

        public Vector Cross(Vector a)
        {
            float cx = Y * a.Z - Z * a.Y;
            float cy = Z * a.X - X * a.Z;
            float cz = X * a.Y - Y * a.X;
            return new Vector(cx, cy, cz);
        }
    }

    public class Color : Tuple
    {
        public float R { get => X; set => X = value; }
        public float G { get => Y; set => Y = value; }
        public float B { get => Z; set => Z = value; }

        public Color(float r, float g, float b) : base(r, g, b, 0)        
        {
       
        }

        public static Color operator *(Color a, Color b)
        {
            return new Color(a.R * b.R, a.G * b.G, a.B * b.B);
        }
    }
}