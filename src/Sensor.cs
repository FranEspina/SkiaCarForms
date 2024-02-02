using SkiaSharp;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkiaCarForms
{
    internal class Sensor
    {
        private readonly Car car;
        private readonly int rayCount = 5;
        private readonly float rayLength = 120f;
        private readonly float raySpread = (float)  Math.PI / 2;
        private SKPoint[][] rays = [];
        private IntersectionPoint?[] readings;
        private SKPoint[][]? borders
        {
            get
            {
                return car?.Borders;
            }
        }

        public Sensor(Car car) {
            this.car = car;
            this.readings = [];
        }

        public void Update()
        {

            castRays();
            setReadings();
        } 

        private void setReadings()
        {
            this.readings = new IntersectionPoint[this.rayCount];
            for (int i = 0; i < this.readings.Length; i++)
            {
                setReadings(this.rays[i], i);
            }
        }

        private void setReadings(SKPoint[] ray, int rayIndex)
        {
            this.readings[rayIndex] = default(IntersectionPoint);

            var touches = new List<IntersectionPoint>();
            this.borders?.ToList().ForEach(border => {

                var touche = Utils.GetIntesection(
                    ray[0], ray[1], 
                    border[0], border[1]);

                if (touche != null) touches.Add(touche);
            });

            if (touches.Count > 0 )
            {
                var nearIntersection = touches.MinBy(x => x.Offset);
                this.readings[rayIndex] = nearIntersection;
            }
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

            for (int i = 0;i < this.rays.Length; i++)
            {
                paint.Color = SKColors.Yellow;
                var ray = this.rays[i];
                var intersection = this.readings[i];
                if (intersection == null)
                {
                    canvas.DrawLine(ray[0], ray[1], paint);
                }
                else
                {
                    var point = new SKPoint(intersection.X, intersection.Y);
                    canvas.DrawLine(ray[0], point, paint);
                    paint.Color = SKColors.Black;
                    canvas.DrawLine(point, ray[1], paint);
                }
            }

            this.rays.ToList().ForEach(
                ray => {
                });

        }

    }
}
