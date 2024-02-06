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
        
        public int RayCount {get; private set;}

        private readonly float rayLength = 120f;
        private readonly float raySpread = (float)  Math.PI / 2;
        private SKPoint[][] rays = [];
        public IntersectionPoint?[] Readings;
        private SKPoint[][]? borders
        {
            get
            {
                return car?.Borders;
            }
        }

        private List<Car>? traffics
        {
            get
            {
                return car.Traffics;
            }
        }

        public Sensor(Car car, int rayCount) {
            this.car = car;
            this.Readings = [];
            this.RayCount = rayCount;
        }

        public void Update()
        {

            castRays();
            setReadings();
        } 

        private void setReadings()
        {
            this.Readings = new IntersectionPoint[this.RayCount];
            for (int i = 0; i < this.Readings.Length; i++)
            {
                setReadings(this.rays[i], i);
            }
        }

        private void setReadings(SKPoint[] ray, int rayIndex)
        {
            this.Readings[rayIndex] = default(IntersectionPoint);

            var touches = new List<IntersectionPoint>();
            this.borders?.ToList().ForEach(border => {

                var touche = Utils.GetIntesection(
                    ray[0], ray[1], 
                    border[0], border[1]);

                if (touche != null) touches.Add(touche);
            });

            this.traffics?.ForEach(traffic =>
            {
                var touche = Utils.PolyIntersect(ray, traffic.ShapePolygon);
                if (touche != null) touches.Add(touche);
            });

            if (touches.Count > 0 )
            {
                var nearIntersection = touches.MinBy(x => x.Offset);
                this.Readings[rayIndex] = nearIntersection;
            }
        }

        private void castRays()
        {
            this.rays = new SKPoint[RayCount][];
            var start = new SKPoint(car.X, car.Y);

            for (int i = 0; i < RayCount; i++)
            {
                var step = (this.RayCount == 1) ? 0.5f : (float)i / (RayCount - 1);
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
                var intersection = this.Readings[i];

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
        }
    }
}
