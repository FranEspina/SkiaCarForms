using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiaCarForms.Serialization
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using System.Runtime.CompilerServices;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using SkiaCarForms.Network;

    internal static class NeuronalNetworkSerializer
    {

        private static readonly string filename = "./bestCarBrain.json";

        internal static void DeleteFile()
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
        }

        internal static void SaveFile(NeuronalNetwork network)
        {
            string jsonString = JsonConvert.SerializeObject(network);
            File.WriteAllText(filename, jsonString);
        }

        internal static string GetContent(NeuronalNetwork network)
        {
            return JsonConvert.SerializeObject(network);
        }

        internal static string LoadContentFile()
        {
            if (File.Exists(filename))
            {
                return File.ReadAllText(filename);
            }
            return string.Empty;

        }

        internal static NeuronalNetwork LoadFile()
        {
            if (File.Exists(filename))
            {
                var jsonString = File.ReadAllText(filename);
                return JsonConvert.DeserializeObject<NeuronalNetwork>(jsonString);
            }
            return null;
        }

        internal static NeuronalNetwork Load(string jsonString)
        {
            return JsonConvert.DeserializeObject<NeuronalNetwork>(jsonString);
        }
    }

}
