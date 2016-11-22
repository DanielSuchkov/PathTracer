﻿using RayTracer.DistanceFields;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.Concurrent;

namespace RayTracer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        WriteableBitmap bitmap;
        DisplayMethod displayMethod;
        Thread backgroundThread;

        public static Vec3 Up;
        public static Vec3 Forward;
        public static Vec3 Right;
        public static double FocalLength = 1;


        public MainWindow() {
            Forward = (Model.Target - Model.Eye).Normalize();
            Right = Forward.Cross(Model.WorldUp).Normalize();
            Up = Right.Cross(Forward).Normalize();

            InitializeComponent();
            UpdateSize();
            CompositionTarget.Rendering += CompositionTarget_Rendering;

            this.Closed += Stop;

            backgroundThread = new Thread(CalculateRays);
            backgroundThread.Start();
        }

        private void Stop(object sender, EventArgs e) {
            backgroundThread.Abort();
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e) {
            if (bitmap.PixelWidth != (int)Content.ActualWidth || bitmap.PixelHeight != (int)Content.ActualHeight) {
                UpdateSize();
            }

            var start = DateTime.Now;
            while ((DateTime.Now - start).Milliseconds < 16) {
                displayMethod.DrawPiece(bitmap);
            }
        }

        private void CalculateRays() {
            while (true) {
                Parallel.For(0, 1000, (_) => {
                    var x = ThreadSafeRandom.NextDouble() - 0.5;
                    var y = ThreadSafeRandom.NextDouble() - 0.5;

                    var pixel = Model.Eye + x * Right + y * Up;
                    var source = Model.Eye - Forward * FocalLength;
                    var ray = new Ray(source, pixel - source);

                    var result = ray.March(Model.Field, 0.01, 200.0, Model.Fog);
                    displayMethod.AddPoint(new ColoredPoint(result.Color, x, -y));
                });
            }
        }

        private void UpdateSize() {
            bitmap = BitmapFactory.New((int)Content.ActualWidth, (int)Content.ActualHeight);
            ImageContainer.Source = bitmap;
            var pixelSize = 1.0 / Math.Max(bitmap.PixelWidth, bitmap.PixelHeight);
            if (displayMethod == null) {
                displayMethod = new AveragedPixels();
            } else {
                displayMethod.Reset(pixelSize);
            }
        }
    }
}
