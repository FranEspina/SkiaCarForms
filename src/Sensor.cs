using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SkiaCarForms
{
    internal class Sensor
    {
        private readonly Car car;
        private readonly int rayCount = 4;
        private readonly float rayLength = 100f;
        private readonly float raySpread = (float)  Math.PI / 4;
        private SKPoint[][] rays = [];

        public Sensor(Car car) {
            this.car = car;
        }

        public void Update()
        {
            castRays();
        } 

        private void castRays()
        {
            this.rays = new SKPoint[rayCount][];
            var start = new SKPoint(car.X, car.Y);

            for (int i = 0; i < rayCount; i++)
            {
                var step = (this.rayCount == 1) ? 0.5f : (float)i / (rayCount - 1);
                var rayAngle = Utils.Lerp(
                    raySpread / 2,
                    -raySpread / 2,
                    step
                    );

                var end = new SKPoint(car.X - (float)Math.Sin(rayAngle + car.Angle) * rayLength,
                                      car.Y - (float)Math.Cos(rayAngle + car.Angle) * rayLength);

                this.rays[i] = [start, end];

            }
        }

        public void Draw(SKCanvas canvas)
        {
            var paint = new SKPaint
            {
                Color = SKColors.Yellow,
                IsAntialias = true,
                StrokeWidth = 2,
                Style = SKPaintStyle.Stroke
            };

            this.rays.ToList().ForEach(
                ray => canvas.DrawLine(ray[0], ray[1], paint));

        }

    }
}
