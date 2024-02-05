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
        public float[] Inputs { get; internal set; }

        public float[] Outputs { get; internal set; }

        public float[] Biases { get; internal set; }
        public float[][] Weights { get; internal set; }


        public Level(int inputCount, int outputCount)
        {
            this.Inputs = new float[inputCount];
            this.Outputs = new float[outputCount];
            this.Biases = new float[outputCount];

            this.Weights = new float[inputCount][];

            for (int i = 0; i < inputCount; i++)
            {
                this.Weights[i] = new float[outputCount];

                for (int j = 0; j < outputCount; j++)
                {
                    this.Weights[i][j] = (float) Random.Shared.NextDouble() * 2 - 1;
                }
            }
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

        public static float[] feedForward(float[] givenInputs, Level level)
        {
            if (givenInputs.Length != level.Inputs.Length)
            {
                throw new ArgumentException("Los datos de entrada y el nivel deben tener el mismo número de nodos.");
            }

            for (int i = 0; i < level.Inputs.Length; i++)
            {
                level.Inputs[i] = givenInputs[i];
            }

            for (int i = 0; i < level.Outputs.Length; i++)
            {
                float sum = 0;
                for (int j = 0; j < level.Inputs.Length; j++)
                {
                    sum += level.Inputs[j] * level.Weights[j][i];
                }

                if (sum > level.Biases[i])
                {
                    level.Outputs[i] = 1;
                }
                else
                {
                    level.Outputs[i] = 0;
                }
            }

            return level.Outputs;

        }



    }
}
