using System;
using System.IO;
using DimpsSonicLib;
using DimpsSonicLib.Archives;
using DimpsSonicLib.Formats.SegaNN;
using System.Collections.Generic;
using System.Text;

namespace DimpsArcExtract
{
    class Functions
    {
        public static void DetectArcType(string arg)
        {
            string ext = Path.GetExtension(arg).ToLower();

            if (ext == ".amb")
            {
                Log.PrintInfo($"Unpacking \"{Path.GetFileName(arg)}\", please wait...");
                UnpackAMBFile(arg);
            }
            else if (ext == ".obb")
            {
                Log.PrintInfo($"Unpacking \"{Path.GetFileName(arg)}\", please wait...");
                UnpackOBBFile(arg);
            }
            else throw new Exception("Passed file was possibly not an AMB or an OBB archive.");
        }

        public static void UnpackAMBFile(string arg)
        {
            // Load the file into a stream
            Console.WriteLine($"Opening {Path.GetFileName(arg)}");
            Stream stream = File.OpenRead(arg);

            // Set up the object, passing in the file's stream
            NNFile nnFile = new NNFile(stream);

            // Parse the given file's stream
            nnFile.ReadNNFile();

            Console.WriteLine("Finished.");
        }

        public static void UnpackOBBFile(string arg)
        {
            var Type = AndroidOBB.DetermineOBBType(arg);

            if (Type != AndroidOBB.OBBType.NotOBB)
            {
                AndroidOBB.ExtractOBBFile(arg, Type);
                Log.PrintInfo("\nComplete!");
            }
            else
                Log.PrintError("File passed is not an OBB file.");
        }
    }
}
