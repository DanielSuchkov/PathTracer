using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer {
    public struct Vec3 {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vec3(double x, double y, double z) {
            X = x;
            Y = y;
            Z = z;
        }

        public Vec3 Normalize() {
            var length = Length();
            return new Vec3(X / length, Y / length, Z / length);
        }

        public double Length() {
            return Distance(this, Zero);
        }

        public double Dot(Vec3 other) {
            return X * other.X + Y * other.Y + Z * other.Z;
        }

        public Vec3 Cross(Vec3 other) {
            return new Vec3(Y * other.Z - Z * other.Y, Z * other.X - X * other.Z, X * other.Y - Y * other.X);
        }

        public static Vec3 operator +(Vec3 a, Vec3 b) {
            return new Vec3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vec3 operator -(Vec3 a, Vec3 b) {
            return new Vec3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vec3 operator -(Vec3 v) {
            return -1 * v;
        }

        public static Vec3 operator *(Vec3 v, double a) {
            return new Vec3(v.X * a, v.Y * a, v.Z * a);
        }

        public static Vec3 operator *(double a, Vec3 v) {
            return v * a;
        }

        public static Vec3 operator /(Vec3 v, double a) {
            return new Vec3(v.X / a, v.Y / a, v.Z / a);
        }

        public static Vec3 Zero = new Vec3(0, 0, 0);
        public static Vec3 One = new Vec3(1, 1, 1);

        public static double Distance(Vec3 a, Vec3 b) {
            return Math.Sqrt(DistanceSquared(a, b));
        }

        public static double DistanceSquared(Vec3 a, Vec3 b) {
            var dx = a.X - b.X;
            var dy = a.Y - b.Y;
            var dz = a.Z - b.Z;
            return dx * dx + dy * dy + dz * dz;
        }

        public static Vec3 Random() {
            var vector = new Vec3(ThreadSafeRandom.NextDouble() * 2 - 1, ThreadSafeRandom.NextDouble() * 2 - 1, ThreadSafeRandom.NextDouble() * 2 - 1);
            if (vector.Length() > 1) {
                return Random();
            }
            return vector;
        }
    }
}
