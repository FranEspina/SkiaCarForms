using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaCarForms
{
    internal class CanvasLock
    {
        public static object Lock = new object();
    }
}
