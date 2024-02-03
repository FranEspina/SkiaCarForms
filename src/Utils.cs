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

        public static bool IsPolyIntersect(SKPoint[] poly1, SKPoint[] poly2)
        {
            return (PolyIntersect(poly1, poly2) != null);
        }

        public static IntersectionPoint? PolyIntersect(SKPoint[] poly1, SKPoint[] poly2)
        {
            for (int i = 0; i < poly1.Length; i++)
            {
                var A = poly1[i % poly1.Length];
                var B = poly1[(i + 1) % poly1.Length];

                for (int j = 0; j < poly2.Length; j++)
                {
                    var C = poly2[j % poly2.Length];
                    var D = poly2[(j + 1) % poly2.Length];
                    
                    var intersection = GetIntesection(A, B, C, D); 
                    if (intersection != null)
                        return intersection;
                }
            }
            return default(IntersectionPoint);
        }

        public static SKBitmap GetTintedImage(string source, float width, float height, SKColor color)
        {
            var bm = SKBitmap.Decode(source)
                .Resize(new SKSizeI((int) width, (int) height), SKFilterQuality.Low);

            return Utils.GetTintedImage(bm, color);
        }

        public static SKBitmap GetTintedImage(SKBitmap bm, SKColor color)
        {
            // Create a new bitmap to hold the tinted image
            var tintedBm = new SKBitmap(bm.Width, bm.Height, bm.ColorType, bm.AlphaType);

            // Iterate over each pixel in the original bitmap
            for (int y = 0; y < bm.Height; y++)
            {
                for (int x = 0; x < bm.Width; x++)
                {
                    // Get the original color
                    var origColor = bm.GetPixel(x, y);

                    // If the pixel is not transparent, tint it
                    if (origColor.Alpha != 0)
                    {
                        var r = (origColor.Red + color.Red) / 2;
                        var g = (origColor.Green + color.Green) / 2;
                        var b = (origColor.Blue + color.Blue) / 2;
                        var a = origColor.Alpha;

                        tintedBm.SetPixel(x, y, new SKColor((byte)r, (byte)g, (byte)b, a));
                    }
                    else
                    {
                        // Otherwise, keep the original color
                        tintedBm.SetPixel(x, y, origColor);
                    }
                }
            }
            return tintedBm;
        }
    }
}
