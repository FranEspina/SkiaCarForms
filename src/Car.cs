using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SkiaCarForms.Enums;

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

        public SKPoint[] ShapePolygon { get; internal set; }

        private bool damaged = false;

        public List<Car> Traffics { get; set; }

        public CarTypeEnum Type { get; internal set; }

        public Car(float x, float y, float width, float height, CarTypeEnum type) { 
            this.X = x;
            this.Y = y;
            this.Height = height;
            this.Width = width;
            this.Controls = default(Controls);
            this.ShapePolygon = [];
            this.Type = type;
            this.Traffics = [];

            if (type == CarTypeEnum.Traffic)
                InitializeTraffic();

            if (type == CarTypeEnum.Controled)
                this.sensor = new Sensor(this);

        }

        private void InitializeTraffic()
        {
            if (Type != CarTypeEnum.Traffic) return;

            MaxSpeed = MaxSpeed * 0f;
            Controls = new Controls();
            Controls.Forward = true;

        }

        private void createPolygon()
        {
            this.ShapePolygon = new SKPoint[4];
            float x = 0;
            float y = 0;

            var radious = Utils.Hypotenuse(this.Height, this.Width) / 2f;
            var alpha = MathF.Atan2(this.Width, this.Height);

            x = this.X + radious * MathF.Sin(alpha - this.Angle);
            y = this.Y - radious * MathF.Cos(alpha - this.Angle);
            this.ShapePolygon[0] = new SKPoint(x, y);

            x = this.X + radious * MathF.Sin(-alpha - this.Angle);
            y = this.Y - radious * MathF.Cos(-alpha - this.Angle);
            this.ShapePolygon[1] = new SKPoint(x, y);

            x = this.X + radious * MathF.Sin(MathF.PI + alpha - this.Angle);
            y = this.Y - radious * MathF.Cos(MathF.PI + alpha - this.Angle);
            this.ShapePolygon[2] = new SKPoint(x, y);

            x = this.X + radious * MathF.Sin(MathF.PI - alpha - this.Angle);
            y = this.Y - radious * MathF.Cos(MathF.PI - alpha - this.Angle);
            this.ShapePolygon[3] = new SKPoint(x, y);

        }

        public void Update()
        {
            if (!this.damaged)
            {
                move();
                createPolygon();
                this.damaged = assessDamage();
                sensor?.Update();
            }
        }

        private bool assessDamage()
        {
            if (this.Borders == null) return false;

            foreach (var border in this.Borders)
            {
                if (Utils.IsPolyIntersect(this.ShapePolygon, border))
                    return true;
            }

            if (this.Traffics != null)
            {
                foreach(var traffic in this.Traffics)
                {
                    if (Utils.IsPolyIntersect(this.ShapePolygon, traffic.ShapePolygon))
                        return true;
                }
            }

            return false;
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
            drawShape(canvas);
            sensor?.Draw(canvas);
        }

        private void drawShape(SKCanvas canvas)
        {
            var color = (Type == CarTypeEnum.Traffic) ? SKColors.DarkRed : SKColors.DarkBlue;
            var colorApplied = (this.damaged) ? SKColors.Transparent : color;

            canvas.Save();
            canvas.Translate(this.X, this.Y);
            canvas.RotateRadians(-this.Angle);

            var tintedBm = Utils.GetTintedImage("Car.png", (int) this.Width, (int) this.Height, colorApplied);

            using (var paint = new SKPaint())
            {
                paint.IsAntialias = true;
                canvas.DrawBitmap(tintedBm, -this.Width / 2, -this.Height / 2, paint);
            }

            canvas.Restore();
        }
    }
}
