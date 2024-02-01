using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaCarForms
{
    internal class Road
    {
        private float x;
        private float width;
        private int laneCount;
        private float left;
        private float right;
        private float top;
        private float bottom;
        private readonly float INFINITY = 100000;
        private readonly int laneStrokeWidth = 5;
        private bool unavez = false;

        public SKPoint[][] Borders { get; private set; }

        public Road(float x, float width, int laneCount = 4) 
        {
            this.x = x ;
            this.width = width;
            this.laneCount = laneCount;

            this.left = x - width / 2 ;
            this.right = x + width / 2 ;
            this.top = INFINITY;
            this.bottom = - INFINITY;

            var topLeft = new SKPoint(this.left, this.top);
            var topRight = new SKPoint(this.right, this.top);
            var bottomLeft = new SKPoint(this.left, this.bottom);
            var bottomRight = new SKPoint(this.right, this.bottom);

            this.Borders = [[topLeft, bottomLeft], 
                            [topRight, bottomRight]];


        }

        public float GetLaneCenter(int indexLane)
        {
            if (indexLane < 1)
            {
                return GetLaneCenter(1);
            }
            else if (indexLane > laneCount)
            {
                return GetLaneCenter(laneCount);
            }
            else
            {
                return Utils.Lerp(this.left, this.right, indexLane / ((float)laneCount)) - (this.width / laneCount / 2) ;
            }
        }

        public void Draw(SKCanvas canvas)
        {
            SKPaint paint = new SKPaint
            {
                Color = SKColors.White,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = laneStrokeWidth,
                PathEffect = SKPathEffect.CreateDash([20, 20], 0)
        };

            for (int i = 1; i < laneCount; i++)
            {                
                var x = Utils.Lerp(this.left, this.right, i / ((float) laneCount));

                canvas.DrawLine(x, this.top,
                                x, this.bottom, 
                                paint);
            }

            paint.PathEffect = null;

            this.Borders.ToList().ForEach(b => canvas.DrawLine(b[0].X, b[0].Y, b[1].X, b[1].Y, paint));
            

        }
    }
}
