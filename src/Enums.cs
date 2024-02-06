using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaCarForms
{
    public class Enums { 
        public enum CarTypeEnum
        {
            PlayerControled,
            Traffic,
            IAControled, 
        }

        public enum SimulationModeEnum
        {
            IADriveMode, 
            PlayerDriveMode
        }
    }
}
