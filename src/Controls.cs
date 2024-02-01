using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaCarForms
{
    internal class Controls
    {
        public bool Forward {get; internal set;}
        public bool Left { get; internal set; }
        public bool Right { get; internal set; }
        public bool Reverse { get; internal set; }

        public Controls()
        {
            this.Forward = false;
            this.Left = false;
            this.Right = false;
            this.Reverse = false;
        }

        public void EventHandlerKeyDown(object? sender, KeyEventArgs e)
        {
            switch(e.KeyCode) { 
                case Keys.A:
                    this.Left = true;
                    break;
                case Keys.D:
                    this.Right = true;   
                    break;  
                case Keys.W:
                    this.Forward = true;
                    break;
                case Keys.S:
                    this.Reverse = true;
                    break;  
            }
        }

        public void EventHandlerKeyUp(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    this.Left = false;
                    break;
                case Keys.D:
                    this.Right = false;
                    break;
                case Keys.W:
                    this.Forward = false;
                    break;
                case Keys.S:
                    this.Reverse = false;
                    break;
            }
        }
    }
}
