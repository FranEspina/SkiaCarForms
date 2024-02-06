using OpenTK.Graphics.OpenGL;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SkiaCarForms
{
    internal class Dashboard
    {
        public Car? Car { set; internal get; }
        public List<Car> Traffics { get; set; }
        public List<Car> Cars { get; set; }


        private float x;
        private float y;

        public bool MustDraw { get; set; } = true;

        public Dashboard(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void Draw(SKCanvas canvas)
        {
            if (Car == null) return;
            if (!MustDraw) return;

            float textSize = 14;
            string text = "";

            var textPos = $"({getFloatToString(Car.X)}, {getFloatToString(Car.Y)})";
            float currentHeight = this.y + textSize;
            
            text = $"Posición: {textPos}";
            drawText(canvas, this.x, currentHeight, textPos, textSize);
            currentHeight = currentHeight + textSize;


            text = $"Velocidad: {getFloatToString(Car.Speed)}";
            drawText(canvas, this.x, currentHeight, text, textSize);
            currentHeight = currentHeight + textSize;


            text = $"Ángulo: {getFloatToString(Car.Angle * 180f / MathF.PI)} º";
            drawText(canvas, this.x, currentHeight, text, textSize);
            currentHeight = currentHeight + textSize;


            if (Cars != null)
            {
                var running = Cars.Where(c => !c.Damaged).Count();
                var pctg = (float) running / (float) Cars.Count * 100f;

                text = $"Coches: {running}/{Cars.Count} ({getFloatToString(pctg)} %)";
                drawText(canvas, this.x, currentHeight, text, textSize);
                currentHeight = currentHeight + textSize;

            }

            if (Traffics!= null)
            {
                var porDelante = Traffics.Where(t => t.Y < Car.Y).Count();

                text = $"Tráfico: {porDelante}/{Traffics.Count}";
                drawText(canvas, this.x, currentHeight, text, textSize);
                currentHeight = currentHeight + textSize;
            }
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

        private string getFloatToString(float number)
        {
            return Math.Round(number, 2).ToString("F2",
                  CultureInfo.CreateSpecificCulture("es-ES"));
        }
    }
}
