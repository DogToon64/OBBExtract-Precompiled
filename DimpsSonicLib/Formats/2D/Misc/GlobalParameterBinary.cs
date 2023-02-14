using System;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Formats.Param
{
    public class HEADER 
    {
        public const string signature = "GPB";
        public uint version { get; set; } = 0;              // char version[4]; - File Version "1.1.0"
        public string param_dataType { get; set; } = "";    // char[32]; Denotes the dataset contained

        public uint dataPtr { get; set; } = 0;
        public uint structSize { get; set; } = 0;
        public uint numStructs { get; set; } = 0;
    }

    class GlobalParameter
    {

    }
}
