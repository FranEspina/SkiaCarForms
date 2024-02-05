using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaCarForms.Network
{


    internal class NeuronalNetwork
    {
        private Level[] levels;
        public NeuronalNetwork(int[] neuronCounts)
        {
            this.levels = new Level[neuronCounts.Length - 1];

            for (int i = 0; i < neuronCounts.Length - 1; i++) 
            { 
                this.levels[i] = new Level(neuronCounts[i], neuronCounts[i+1]);
            }

        }

        public static float[] feedForward(float[] givenValues, NeuronalNetwork network)
        {
            var values = (float[]) givenValues.Clone();

            for (int i = 0; i < network.levels.Length; i++)
            {
                values = Level.feedForward(values, network.levels[i]);
            }

            return values;
        }
    }
}
