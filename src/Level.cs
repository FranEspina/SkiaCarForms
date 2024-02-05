using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SkiaCarForms.Network
{
    internal class Level
    {
        public float[] Inputs { get; private set; }

        public float[] Outputs { get; private set; }

        public float[] Biases { get; private set; }
        
        public float[][] Weights { get; private set; }

        public string[] LabelOutputs { get; private set; } 


        public Level(int inputCount, int outputCount)
        {
            this.Inputs = new float[inputCount];
            this.Outputs = new float[outputCount];
            this.Biases = new float[outputCount];
            this.LabelOutputs = new string[outputCount];
            this.Weights = new float[inputCount][];

            randomize(this);
        }

        public void SetLabelOutput(int index, string label)
        {
            this.LabelOutputs[index] = label;
        }

        private static void randomize(Level level)
        {
            for (int i = 0; i < level.Inputs.Length; i++)
            {
                level.Weights[i] = new float[level.Outputs.Length];

                for (int j = 0; j < level.Outputs.Length; j++)
                {
                    level.Weights[i][j] = (float)Random.Shared.NextDouble() * 2 - 1;
                }
            }

            for (int j = 0; j < level.Outputs.Length; j++)
            {
                level.Biases[j] = (float)Random.Shared.NextDouble() * 2 - 1;
            }
        }

        public float[] feedForward(float[] givenInputs)
        {
            if (givenInputs.Length != this.Inputs.Length)
            {
                throw new ArgumentException("Los datos de entrada y el nivel deben tener el mismo número de nodos.");
            }

            for (int i = 0; i < this.Inputs.Length; i++)
            {
                this.Inputs[i] = givenInputs[i];
            }

            for (int i = 0; i < this.Outputs.Length; i++)
            {
                float sum = 0;
                for (int j = 0; j < this.Inputs.Length; j++)
                {
                    sum += this.Inputs[j] * this.Weights[j][i];
                }

                if (sum > this.Biases[i])
                {
                    this.Outputs[i] = 1;
                }
                else
                {
                    this.Outputs[i] = 0;
                }
            }

            return this.Outputs;
        }



    }
}
