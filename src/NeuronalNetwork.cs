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
        public NeuronalNetwork(int[] neuronCounts)
        {
            this.Levels = new Level[neuronCounts.Length - 1];

            for (int i = 0; i < neuronCounts.Length - 1; i++) 
            { 
                this.Levels[i] = new Level(neuronCounts[i], neuronCounts[i+1]);
            }

        }

        public static float[] feedForward(float[] givenValues, NeuronalNetwork network)
        {
            var values = (float[]) givenValues.Clone();

            for (int i = 0; i < network.Levels.Length; i++)
            {
                values = Level.feedForward(values, network.Levels[i]);
            }

            return values;
        }
    }
}
