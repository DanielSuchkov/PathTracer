using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RayTracer.DistanceFields {
    public abstract class DistanceField {
        public abstract SampleResult Sample(Vec3 pos);

        public static Union operator +(DistanceField first, DistanceField second) {
            return new Union(first, second);
        }

        public static Subtraction operator -(DistanceField first, DistanceField second) {
            return new Subtraction(first, second);
        }

        public static Intersection operator *(DistanceField first, DistanceField second) {
            return new Intersection(first, second);
        }

        public static Translation operator +(DistanceField field, Vec3 translation) {
            return new Translation(field, translation);
        }

        public static Translation operator +(Vec3 translation, DistanceField field) {
            return field + translation;
        }

        public static MaterialChange operator *(DistanceField field, MaterialSettings settings) {
            return new MaterialChange(field, settings);
        }

        public static MaterialChange operator *(MaterialSettings settings, DistanceField field) {
            return field * settings;
        }

        public static Repitition operator *(DistanceField field, Vec3 distance) {
            return new Repitition(field, distance);
        }

        public static Repitition operator *(Vec3 distance, DistanceField field) {
            return field * distance;
        }
    }
}
