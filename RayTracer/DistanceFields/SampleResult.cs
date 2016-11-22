using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RayTracer.DistanceFields {
    public class SampleResult {
        public double Distance { get; set; }
        public Vec3 Normal { get; set; }
        public Vec3 Color { get; set; }
        public double Reflectance { get; set; }
        public double Roughness { get; set; }
        public double Absorbance { get; set; }
        public bool Source { get; set; }

        public SampleResult() {
            Color = Vec3.One;
            Reflectance = 0.5;
            Roughness = 1;
            Absorbance = 0.05;
            Source = false;
        }

        public static SampleResult Min(SampleResult first, SampleResult second) {
            if (first.Distance < second.Distance) {
                return first;
            } else {
                return second;
            }
        }

        public static SampleResult Max(SampleResult first, SampleResult second) {
            if (first.Distance > second.Distance) {
                return first;
            } else {
                return second;
            }
        }
    }
}
