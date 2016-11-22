using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RayTracer.DistanceFields {
    public class Plane : DistanceField {
        public Vec3 Normal { get; set; }
        public double Offset { get; set; }

        public Plane(Vec3 normal, double offset) {
            Normal = normal.Normalize();
            Offset = offset;
        }

        public override SampleResult Sample(Vec3 pos) {
            return new SampleResult { Distance = pos.Dot(Normal) - Offset, Normal = Normal };
        }
    }
}
