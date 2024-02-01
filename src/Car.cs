using SkiaSharp;
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
        public float Acceleration { get; set; } = 0.3f;
        public float MaxSpeed { get; set; } = 3f;
        public float MaxReverseSpeed { get; set; } = 1.5f;

        public float Friction { get; set; } = 0.1f;




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
            if (this.Speed < - this.MaxReverseSpeed) this.Speed = - this.MaxReverseSpeed;

            if ( Math.Abs(this.Speed) < this.Friction)
            {
                this.Speed = 0; 
            }

            this.Y -= this.Speed;
        }

        public void Draw(SKCanvas canvas)
        {
            SKPaint paint = new SKPaint
            {
                Color = SKColors.Black,
                Style = SKPaintStyle.Fill
            };

            // Define las dimensiones del rectángulo
            var rect = new SKRect(
                this.X - this.Width / 2, 
                this.Y - this.Height / 2,
                this.X + this.Width / 2,
                this.Y + this.Height / 2);

            // Dibuja el rectángulo en el canvas
            canvas.DrawRect(rect, paint);
        }
    }
}
