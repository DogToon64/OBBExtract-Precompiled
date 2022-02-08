using System;
using DimpsSonicLib.IO;
using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.Core;

namespace CmdTest
{
    public class zlibTest
    {
        public static void zlibFunc(string[] args)
        {

            var newDir = FileInterface.CreateDirectoryAtFileLocation(FileInterface.GetLocalFile("SON_MDL.AMB"), "Compressed");

            foreach (string file in args)
            {
                // Compress files
                Compress(file, newDir);
            }

            // Now decompress those exact same files
            Decompress(newDir);

        }

        public static void Compress(string file, string newDir)
        {
            Console.WriteLine("string file: {0}\nstring newDir: {1}", file, newDir);
            Console.WriteLine("Compressed {0} from {1} bytes down to {2} bytes.\n", file, 0 , -69);
        }

        public static void Decompress(string dir)
        {

            Console.WriteLine("Decompressed {0}", "Fuck All");
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
