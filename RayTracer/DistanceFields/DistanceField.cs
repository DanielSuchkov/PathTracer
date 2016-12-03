using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Zhucai.LambdaParser;

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

        public static DistanceField LoadFromJson(string path) {
            try {
                return WalkJson(JToken.Parse(File.ReadAllText(path)));
            } catch (Exception err) {
                Console.WriteLine("Loading from {0}: {1}", path, err);
                return null;
            }
        }

        public static MaterialSettings ReadMaterial(JToken node) {
            var res = new MaterialSettings();
            var refl = node.Value<double?>("Reflectance");
            if (refl != null) {
                res.Reflectance = refl.Value;
            }

            var roug = node.Value<double?>("Roughness");
            if (roug != null) {
                res.Roughness = roug.Value;
            }

            var source = node.Value<bool?>("Source");
            if (source != null) {
                res.Source = source.Value;
            }

            var absorb = node.Value<double?>("Absorbance");
            if (absorb != null) {
                res.Absorbance = absorb.Value;
            }

            var colorFn = node.Value<string>("ColorFn");
            if (!String.IsNullOrEmpty(colorFn)) {
                res.GetColor = ExpressionParser.Compile<Func<Vec3, Vec3>>(colorFn, "System", "RayTracer");
            }
            return res;
        }

        public static DistanceField ReadSphere(JToken node) {
            var res = new Sphere(node.Value<double>("Radius"));
            return res;
        }

        public static Vec3 ReadVec3(JToken node) {
            var res = new Vec3();
            var x = node.Value<double?>("x");
            if (x != null) {
                res.X = x.Value;
            }
            var y = node.Value<double?>("y");
            if (y != null) {
                res.Y = y.Value;
            }
            var z = node.Value<double?>("z");
            if (z != null) {
                res.Z = z.Value;
            }

            return res;
        }

        public static DistanceField ReadUnion(JToken node) {
            var first = WalkJson(node.Value<JObject>("First"));
            var second = WalkJson(node.Value<JObject>("Second"));
            return new Union(first, second);            
        }

        public static DistanceField WalkJson(JToken node) {
            DistanceField res = null;
            switch (node.Value<string>("Type")) {
                case "Sphere":
                    res = ReadSphere(node);
                    break;
                case "Union":
                    res = ReadUnion(node);
                    break;
                case "Plane":
                    res = ReadPlane(node);
                    break;
                case "Repitition":
                    res = ReadRepetition(node);
                    break;
                case "Substraction":
                    res = ReadSubstraction(node);
                    break;
                case "Intersection":
                    res = ReadIntersection(node);
                    break;
                default:
                    throw new Exception("Can't find a Type field. It's terrible. Suffer");
            }
            var material = node.Value<JObject>("Material");
            if (material != null) {
                res = new MaterialChange(res, ReadMaterial(material));
            }

            var position = node.Value<JObject>("Position");
            if (position != null) {
                res = new Translation(res, ReadVec3(position));
            }
            return res;
        }

        private static DistanceField ReadIntersection(JToken node) {
            var first = WalkJson(node.Value<JObject>("First"));
            var second = WalkJson(node.Value<JObject>("Second"));
            return new Intersection(first, second);
        }

        private static DistanceField ReadSubstraction(JToken node) {
            var first = WalkJson(node.Value<JObject>("First"));
            var second = WalkJson(node.Value<JObject>("Second"));
            return new Subtraction(first, second);
        }

        private static DistanceField ReadRepetition(JToken node) {
            var field = WalkJson(node.Value<JObject>("Field"));
            var dist = ReadVec3(node.Value<JObject>("Distance"));
            return new Repitition(field, dist);
        }

        private static DistanceField ReadPlane(JToken node) {
            var offset = node.Value<double>("Offset");
            var normal = ReadVec3(node.Value<JObject>("Normal"));
            return new Plane(normal, offset);
        }
    }
}
