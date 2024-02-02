using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaCarForms
{
    public class IntersectionPoint
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Offset { get; set; }    

        public IntersectionPoint(float x, float y, float offset)
        {
            this.X = x;
            this.Y = y;
            this.Offset = offset;
        }

    }
}
