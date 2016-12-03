using System;

namespace RayTracer.DistanceFields {
    public class MaterialSettings {
        public Func<Vec3, Vec3> GetColor { get; set; }
        public double Reflectance { get; set; }
        public double Roughness { get; set; }
        public bool Source { get; set; }
        public double Absorbance { get; set; }

        public MaterialSettings() {
            GetColor = _ => Vec3.One;
            Reflectance = 0;
            Roughness = 1;
            Absorbance = 0.1;
            Source = false;
        }
    }
}
