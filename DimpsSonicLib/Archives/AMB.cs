using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DimpsLib.IO;

namespace DimpsLib.Archives
{
    public class MemoryBinder
    {
        public static bool CheckSignature(Stream input)
        {
            var reader = new ExtendedBinaryReader(input, false);

            if (reader.ReadSignature(4) == "#AMB")
                return true;
            else
                return false;
        }

        public static void ReadHeader(Stream input)
        {
            var reader = new ExtendedBinaryReader(input, false);
            Logger.PrintError("Not Implemented\n");
        }

    }

}
