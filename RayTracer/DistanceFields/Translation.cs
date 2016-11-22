using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer.DistanceFields {
    public class Translation : DistanceField {
        public DistanceField Field { get; set; }
        public Vec3 Amount { get; set; }

        public Translation(DistanceField field, Vec3 amount) {
            Field = field;
            Amount = amount;
        }

        public override SampleResult Sample(Vec3 pos) {
            return Field.Sample(pos - Amount);
        }
    }
}
