using System;
using DimpsSonicLib.IO;
using DimpsSonicLib;
using System.IO;

namespace CmdTest
{
    public class zlibTest
    {
        public static void zlibFunc(string[] args)
        {
            var newDir = Utility.CreateDirectoryAtFileLocation(Utility.GetLocalFile("SON_MDL.AMB"), "Compressed");

            foreach (string file in args)
            {
                //Console.WriteLine("string file: {0}\nstring newDir: {1}\n\n", file, newDir);
                Console.WriteLine("\nCOMPRESSION TESTS\n=================\n");
                // Compress files
                Compress(file, newDir);

                Console.WriteLine();

                // Now decompress those exact same files
                Decompress(file, newDir);
            }
        }

        public static void Compress(string file, string newDir)
        {
            Stream stream = File.OpenRead(file);
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream);
            MemoryStream result = new MemoryStream();

            byte[] head = reader.ReadBytes(15);
            byte[] flag = { 2 };
            byte[] data = reader.ReadBytes((int)(reader.BaseStream.Length - 15));
            byte[] deflated = Compression.CompressZlibChunk(data);


            byte[] origSizeInt = BitConverter.GetBytes((int)reader.BaseStream.Length);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(origSizeInt);

            result.Write(head, 0, head.Length);
            result.Write(flag, 0, 1);
            result.Write(origSizeInt, 0, origSizeInt.Length);
            result.Write(deflated, 0, deflated.Length);

            string name = Path.GetFileName(file);
            File.WriteAllBytes((newDir + "\\" + name), result.ToArray());

            Console.WriteLine("Compressed \"{0}\"\n {1} bytes -> {2} bytes\n", file, stream.Length , result.Length);
        }

        public static void Decompress(string file, string dir)
        {
            Stream stream = File.OpenRead((dir + "\\" + file));
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream);
            MemoryStream result = new MemoryStream();

            byte[] head = reader.ReadBytes(15);
            byte[] flag = { 0 };
            reader.JumpAhead(5);
            byte[] data = reader.ReadBytes((int)(reader.BaseStream.Length - 20));
            byte[] inflated = Compression.DecompressZlibChunk(data);


            byte[] origSizeInt = BitConverter.GetBytes((int)reader.BaseStream.Length);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(origSizeInt);

            result.Write(head, 0, head.Length);
            result.Write(inflated, 0, inflated.Length);

            string name = Path.GetFileNameWithoutExtension(file) + "_DECOMPRESS" + Path.GetExtension(file);
            File.WriteAllBytes((dir + "\\" + name), result.ToArray());

            Console.WriteLine("Decompressed \"{0}\"\n {1} bytes -> {2} bytes\n", file, stream.Length, result.Length);
        }
    }
}
