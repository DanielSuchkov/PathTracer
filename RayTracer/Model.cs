using RayTracer.DistanceFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer {
    public static class Model {
        public static Vec3 WorldUp = new Vec3(0, 1, 0);
        public static Vec3 Target = new Vec3(0, 0, 0);
        public static Vec3 Eye = new Vec3(0, 2, -4);

        public static Vec3 Fog = Vec3.Zero;

        public static MaterialSettings CheckerBoard = new MaterialSettings {
            GetColor = (Vec3 pos) => {
                var sum = (int)Math.Floor(pos.X) + (int)Math.Floor(pos.Z);
                var white = (sum / 2) * 2 == sum;
                if (white) {
                    return Vec3.One;
                } else {
                    return Vec3.Zero;
                }
            }
        };

        public static DistanceField Field =
            (new Sphere(1) * new MaterialSettings { Source = true, GetColor = _ => new Vec3(4, 4, 4) } + new Vec3(-5, 5, -5)) +
            (new Sphere(0.5) * new MaterialSettings { Roughness = 0, Reflectance = 1 } + new Vec3(1.5, 0, 0)) +
            (new Sphere(0.5) * new MaterialSettings { Roughness = 0, Reflectance = 0.5 } + new Vec3(0, 0, 0)) +
            (new Sphere(0.5) * new MaterialSettings { Roughness = 0, Reflectance = 0 } + new Vec3(-1.5, 0, 0)) +
            (new Sphere(0.25) * new MaterialSettings { Roughness = 1, Reflectance = 0, GetColor = _ => new Vec3(1, 0, 0) } + new Vec3(-1, -0.25, -0.75)) +
            (new Sphere(0.25) * new MaterialSettings { Roughness = 1, Reflectance = 0, GetColor = _ => new Vec3(0, 1, 0) } + new Vec3(0, -0.25, -0.75)) +
            (new Sphere(0.25) * new MaterialSettings { Roughness = 1, Reflectance = 0, GetColor = _ => new Vec3(0, 0, 1) } + new Vec3(1, -0.25, -0.75)) +
            (new Plane(new Vec3(0, 0, -1), -2)) +
            (new Plane(WorldUp, -0.5));

    }
}
