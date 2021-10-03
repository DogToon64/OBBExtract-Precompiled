using System;
using System.IO;
using DimpsSonicLib;
using DimpsSonicLib.IO;

namespace CmdTest
{
    public class DSLType
    {
        public static void ReadLTS(string file)
        {
            // Vector/Color reading tests
            Stream stream = File.OpenRead(file);
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream, true);

            Console.WriteLine("LTS Data: \n");
            Console.WriteLine($"Flag: {reader.ReadUInt32()}");
            Console.WriteLine($"Main Color: RGB{reader.ReadRGB()}\n");

            for (int i = 1; i <= 8; i++)
            {
                Logger.PrintInfo ($"Light Num  : {i}");
                Console.WriteLine($"Unknown    : {reader.ReadUInt32()}");
                reader.JumpAhead(4);
                Console.WriteLine($"Color      : RGBA{reader.ReadRGBA()}");
                Console.WriteLine($"Intensity  : {reader.ReadSingle()}");
                Console.WriteLine($"Rotation(?): XYZ{reader.ReadVector3()}\n");
                reader.JumpAhead(32);
            }
        }
    }
}
