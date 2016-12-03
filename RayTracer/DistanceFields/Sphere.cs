using System;
using Newtonsoft.Json;

namespace RayTracer.DistanceFields {
    public class Sphere : DistanceField {
        public double Radius { get; set; }

        public Sphere(double radius) {
            Radius = radius;
        }

        public override SampleResult Sample(Vec3 pos) {
            return new SampleResult { Distance = pos.Length() - Radius, Normal = pos.Normalize() };
        }
    }
}
