using OpenTK.Graphics.OpenGL;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaCarForms
{
    internal class Dashboard
    {
        public Car? Car { set; internal get; }

        private float x;
        private float y;    

        public Dashboard(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void Draw(SKCanvas canvas)
        {
            if (Car == null) return;

            float textSize = 25;

            var textPos = $"({Car.X}, {Car.Y})";

            drawText(canvas, this.x, this.y + 1 * textSize, textPos, textSize);
            drawText(canvas, this.x, this.y + 2 * textSize, Car.Speed.ToString(), textSize);
            drawText(canvas, this.x, this.y + 3 * textSize, Car.Angle.ToString(), textSize);
        }

        private void drawText(SKCanvas canvas, float x, float y, string text, float textSize)
        {
            SKPaint paint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = textSize,
                IsAntialias = true,
                Style = SKPaintStyle.Fill
            };

            canvas.DrawText(text, x, y, paint);
        }
    }
}
