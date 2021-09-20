using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.Core;

namespace DeflateTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No files passed.");
            }
            else
            {
                try
                {
                    string dir = (Path.GetFullPath(args[0]).Replace(Path.GetFileName(args[0]), ""));
                    Directory.CreateDirectory(dir + "Compressed");
                    var newDir = (dir + "Compressed");

                    foreach (string file in args)
                    {
                        // Compress files
                        Compress(file, newDir);
                    }

                    // Now decompress those exact same files
                    Decompress(newDir);

                    Console.WriteLine("done");
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                Console.ReadLine();
            }
        }



        public static void Compress(string file, string newDir)
        {
            if (true)
            {
                
                Console.WriteLine("Compressed {0} from {1} down to {2} bytes.", Path.GetFileName(file));
            }
        }

        public static void Decompress(string dir)
        {

            Console.WriteLine("Decompressed {0}");
            //using (FileStream originalFileStream = input.OpenRead())
            //{ 
            //    using (var memoryStream = new MemoryStream())
            //    {
            //        StreamUtils.Copy(originalFileStream, memoryStream, new byte[2048]);

            //        byte[] result = null;
            //        using (var outStream = new MemoryStream())
            //        {
            //            using (InflaterInputStream inf = new InflaterInputStream(memoryStream))
            //            {
            //                // Causes an Unexpected EOF Error despite files actually being valid?
            //                StreamUtils.Copy(inf, outStream, new byte[2048]); 
            //            }
            //            result = outStream.ToArray();

            //            File.WriteAllBytes((dir + "\\" + input.Name + ".dec"), result);
            //        }
            //    }
            //}
        }
    }
}
