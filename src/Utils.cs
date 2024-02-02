using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaCarForms
{
    internal class Utils
    {
        /// <summary>
        /// Interpolación lineal entre dos puntos
        /// </summary>
        /// <param name="x">valor origen</param>
        /// <param name="y">valor detino</param>
        /// <param name="t">porcentaje entre 0 y 1</param>
        /// <returns>El valor entre x e y que deja el t% de la distancia entre x e y, a la derecha de x</returns>
        public static float Lerp(float x, float y, float t)
        {
            return x + (y - x) * t; 
        }

        /// <summary>
        /// Functión que devuelve la intersección entre dos segmentos [A, B] y [C, D]
        /// </summary>
        /// <param name="A">Punto inicio del primer segmento</param>
        /// <param name="B">Punto fin del primer segmento</param>
        /// <param name="C">Punto inicio del segundo segmento</param>
        /// <param name="D">Punto fin del segundo segmento</param>
        /// <returns>Si los segmentos se corta devuelve las coordenadas del punto y la distancia de la intersección. Nulo en otro caso.</returns>
        public static IntersectionPoint? GetIntesection(SKPoint A, SKPoint B, SKPoint C, SKPoint D )
        {
            var tTop= (D.X - C.X) * (A.Y - C.Y) - (D.Y - C.Y) * (A.X - C.X);
            var uTop= (C.Y - A.Y) * (A.X - B.X) - (C.X - A.X) * (A.Y - B.Y);
            var bottom= (D.Y - C.Y) * (B.X - A.X) - (D.X - C.X) * (B.Y - A.Y);

            if (bottom != 0)
            {
                var t = tTop / bottom;
                var u = uTop / bottom;
                if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
                {
                    return new IntersectionPoint(
                            Lerp(A.X, B.X, t),
                            Lerp(A.Y, B.Y, t),
                            t

                        );
                }
            }

            return null;
        }

        public static float Hypotenuse(float leg, float otherLeg)
        {
            return MathF.Sqrt(leg * leg + otherLeg * otherLeg);
        }
    }
}
