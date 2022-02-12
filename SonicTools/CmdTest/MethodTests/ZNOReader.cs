using System;
using System.IO;
using DimpsSonicLib;
using DimpsSonicLib.Formats.SegaNN;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CmdTest
{
    public class ZNOReader
    {
        public static void ReadZNO(string file)
        {
            // ZNO reading tests
            Stream stream = File.OpenRead(file);

            NNFile a = new NNFile(file);


            Console.WriteLine("ZNO Data: \n");

            Console.WriteLine(a.Data.Info.dataSize);
        }
    }
}
