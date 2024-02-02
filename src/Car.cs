using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaCarForms
{
    internal class Car
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float Speed { get; internal set; }
        public float Acceleration { get; set; } = 0.5f;
        public float MaxSpeed { get; set; } = 10f;
        public float MaxReverseSpeed { get; set; } = 3f;

        public float Friction { get; set; } = 0.2f;

        private float rotationEffect = 0.06f;

        public float Angle { get; internal set; } = 0f;

        public Controls? Controls { get; set; }
        public SKPoint[][]? Borders { get; set; }

        private readonly Sensor? sensor;

        private SKPoint[] points;

        public Car(float x, float y, float width, float height) { 
            this.X = x;
            this.Y = y;
            this.Height = height;
            this.Width = width;

            this.Controls = default(Controls);
            this.sensor = new Sensor(this);
        }

        private void createPolygon()
        {
            this.points = new SKPoint[4];
            float x = 0;
            float y = 0;

            var radious = Utils.Hypotenuse(this.Height, this.Width) / 2f;
            var alpha = MathF.Atan2(this.Width, this.Height);

            x = this.X + radious * MathF.Sin(alpha - this.Angle);
            y = this.Y - radious * MathF.Cos(alpha - this.Angle);
            this.points[0] = new SKPoint(x, y);

            x = this.X + radious * MathF.Sin(-alpha - this.Angle);
            y = this.Y - radious * MathF.Cos(-alpha - this.Angle);
            this.points[1] = new SKPoint(x, y);

            x = this.X + radious * MathF.Sin(MathF.PI + alpha - this.Angle);
            y = this.Y - radious * MathF.Cos(MathF.PI + alpha - this.Angle);
            this.points[2] = new SKPoint(x, y);

            x = this.X + radious * MathF.Sin(MathF.PI - alpha - this.Angle);
            y = this.Y - radious * MathF.Cos(MathF.PI - alpha - this.Angle);
            this.points[3] = new SKPoint(x, y);

        }

        public void Update()
        {
            move();
            createPolygon();
            sensor?.Update();
        }

        private void move()
        {
            if (Controls == null) return;

            if (Controls.Forward)
            {
                this.Speed += this.Acceleration;
            }
            if (Controls.Reverse)
            {
                this.Speed -= this.Acceleration;
            }


            if (this.Speed > 0)
            {
                this.Speed -= this.Friction;
            }
            else if (this.Speed < 0)
            {
                this.Speed += this.Friction;
            }

            if (this.Speed > this.MaxSpeed) this.Speed = this.MaxSpeed;
            if (this.Speed < -this.MaxReverseSpeed) this.Speed = -this.MaxReverseSpeed;

            if (Math.Abs(this.Speed) < this.Friction)
            {
                this.Speed = 0;
            }

            if (Speed != 0)
            {
                var flip = (this.Speed > 0) ? 1 : -1;

                if (Controls.Left)
                {
                    this.Angle += this.rotationEffect * flip;
                }
                if (Controls.Right)
                {
                    this.Angle -= this.rotationEffect * flip;
                }

                this.X -= (float)Math.Sin(Angle) * this.Speed;
                this.Y -= (float)Math.Cos(Angle) * this.Speed;
            }
        }

        public void Draw(SKCanvas canvas)
        {

            SKPaint paint = new SKPaint
            {
                Color = SKColors.DarkSlateBlue,
                Style = SKPaintStyle.StrokeAndFill,
                IsAntialias = true,
            };

            var path = new SKPath();
            path.MoveTo(points[0].X, points[0].Y);
            for (int i = 1; i <= points.Length; i++)
            {
                var index = i % points.Length;
                path.LineTo(points[index].X, points[index].Y);
            }
            canvas.DrawPath(path, paint);

            if (sensor != null) 
                sensor.Draw(canvas);
        }
    }
}
