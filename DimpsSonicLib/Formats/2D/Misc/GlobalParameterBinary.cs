using System;
using System.Collections.Generic;
using System.Text;

namespace DimpsSonicLib.Formats.Param
{
    public class GPB_BASE
    {
        public const string Signature = "GPB";
        public uint Version { get; set; }       // char version[4]; - File Version "1.1.0"
        public string StructType { get; set; }  // char[32]; Denotes the dataset contained

        public uint DataPtr { get; set; }
        public uint StructSize { get; set; }
        public uint StructCount { get; set; }

        public GPB_BASE() {  }


        public virtual void Read(IO.ExtendedBinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(IO.ExtendedBinaryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
