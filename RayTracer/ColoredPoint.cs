﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RayTracer {
    public class ColoredPoint {
        public Vec3 Color { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public ColoredPoint(Vec3 color, double x, double y) {
            Color = color;
            X = x;
            Y = y;
        }
    }
}
