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
    }
}
