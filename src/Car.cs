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

        public Car(float x, float y, float width, float height) { 
            this.X = x;
            this.Y = y;
            this.Height = height;
            this.Width = width;

            this.Controls = default(Controls);
        }

        public void Update()
        {
            move();
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
                Style = SKPaintStyle.Fill, 
                IsAntialias = true,
            };

            canvas.Save();
            canvas.Translate(this.X, this.Y);
            canvas.RotateRadians(-this.Angle);
            
            var rect = new SKRect(
                - this.Width / 2, 
                - this.Height / 2,
                this.Width / 2,
                this.Height / 2);


            // Dibuja el rectángulo en el canvas
            canvas.DrawRect(rect, paint);

            paint.Color = SKColors.White;
            var windowCar = new SKRect(
                            rect.Left + 5f,
                            rect.Top + 5f,
                            rect.Right - 5f,
                            rect.Top + rect.Height * 0.3f);

            canvas.DrawRect(windowCar, paint);

            canvas.Restore();
        }
    }
}
