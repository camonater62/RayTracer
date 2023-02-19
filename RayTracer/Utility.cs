﻿namespace RayTracer
{
    public static class Utility
    {
        public static bool float_eq(float a, float b)
        {
            const float EPSILON = 0.0001f;
            return MathF.Abs(a - b) < EPSILON;
        }
    }
}
