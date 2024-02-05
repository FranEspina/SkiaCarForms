using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaCarForms.Network
{


    internal class NeuronalNetwork
    {
        public Level[] Levels { get; private set; }

        public float[] Outputs
        {
            get
            {
                if (Levels == null || Levels.Length == 0) return [];
                return Levels[Levels.Length - 1].Outputs;
            }
        }

        public NeuronalNetwork(int[] neuronCounts)
        {
            this.Levels = new Level[neuronCounts.Length - 1];

            for (int i = 0; i < neuronCounts.Length - 1; i++) 
            { 
                this.Levels[i] = new Level(neuronCounts[i], neuronCounts[i+1]);
            }

        }

        public void feedForward(float[] givenValues)
        {
            var values = (float[]) givenValues.Clone();

            for (int i = 0; i < this.Levels.Length; i++)
            {
                values = this.Levels[i].feedForward(values);
            }
 
        }
    }
}
