using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DimpsSonicLib;
using DimpsSonicLib.IO;

namespace CmdTest
{
    public class BinaryTests
    {
        public static void DoThing()
        {
            // Test read on the name table dumped from an S4E2 Android OBB file.
            Stream stream = File.OpenRead(@"C:\Users\Kass\Desktop\OBBNAME.TBL");
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream);

            int numDirs = 119;
            int numFiles = 2317;

            string[] array = reader.ReadNullTerminatedStringArray(numDirs + numFiles);

            stream.Close();

            int dirID = 114;  // Should be "MSG/ANC"
            int fileID = 1094; // Should be "MSG_ANC_DESIGN03_01_IT.AMB"

            // Print evaluated file path embedded in nameTable, with ID correction to start from 0
            Console.WriteLine("Evaluated file path: {0}/{1}",
                array[dirID - 1], array[numDirs + (fileID - 1)]);
        }


    }
}
