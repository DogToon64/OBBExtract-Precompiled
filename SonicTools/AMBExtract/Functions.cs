using System;
using System.IO;
using DimpsLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBExtract
{
    class Functions : Program
    {
        public static void UnpackAMBFile(string args)
        {
            using (Stream stream = File.OpenRead(args))
            {
                if (MemoryBinder.CheckSignature(stream))
                {
                    MemoryBinder.ReadHeader(stream);
                }
                else
                {
                    Logger.PrintError("Signature failed to verify.\n");
                }
            }
        }

        public static void PackAMBFile(string args)
        {
            throw new NotImplementedException();
        }
    }
}
