using System;
using System.IO;
using DimpsSonicLib;
using DimpsSonicLib.IO;
using DimpsSonicLib.Archives;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AMBExtract
{
    class Functions
    {
        public static void UnpackAMBFile(string arg)
        {
            try
            {
                // Create folder to extract to
                string newDir = Utility.CreateFolderFromFileName(arg);
                Directory.CreateDirectory(newDir);

                // Load AMB file from argument
                Stream stream = File.OpenRead(arg);

                // Parsing
                BinderReader binder = new BinderReader(stream);
                binder.ReadBinder();
#if DEBUG
                // [TESTING] Print Header and Sub Header information
                Console.WriteLine("[HEADER]\nCompression Type: {0}\nIs Big Endian: {1}\n", binder.Header.compressionType, binder.Header.endianness);
                Console.WriteLine("[SUB HEADER]\nFile Count:    {0}\nData Pointer: {1}\n", binder.SubHeader.fileCount, binder.SubHeader.dataPointer);

                // [TESTING] Print info for each file in the AMB index list
                for (int i = 0; i < (int)binder.SubHeader.fileCount; i++)
                {
                    Console.WriteLine("[FILE {0}]\nName:         {1}\nPointer:      {2}\n" +
                        "Size:         {3}\nUnknown:      {4}\nUser 0:       {5}\nUser 1:       {6}\n",
                        i,
                        binder.Index[i].name,
                        binder.Index[i].filePointer,
                        binder.Index[i].fileSize,
                        binder.Index[i].unknown1,
                        binder.Index[i].USR0,
                        binder.Index[i].USR1);
                }

#else
                Console.WriteLine("Compression Type: {0}\nIs Big Endian:    {1}", binder.Header.compressionType, binder.Header.endianness);
                if (binder.SubHeader.fileCount != 0)
                    Console.WriteLine("File Count:       {0}\n", binder.SubHeader.fileCount);
#endif

                // Create index binary based on current file



                // Extract data
                List<byte[]> fileData = new List<byte[]>();

                for (int i = 0; i < (int)binder.SubHeader.fileCount; i++)
                {
                    binder.JumpTo(binder.Index[i].filePointer);
                    fileData.Add(binder.ReadBytes((int)binder.Index[i].fileSize));
                    string fileName = binder.Index[i].name != "" ? binder.Index[i].name : $"file_{i}.bin"; 

                    Console.WriteLine("Extracting \"{0}\"", fileName);

                    File.WriteAllBytes((newDir + "\\" + fileName), fileData[i]);
                }

            }
            catch (Exception ex)
            {
                Log.PrintError(ex.ToString());
            }
        }

        public static void PackAMBFile(string args)
        {
            try
            {
                throw new Exception("Binder repacking not yet implemented!\n");

                // Try get either index binary or read from original.

                // If above fails, generate a brand new file after user prompt (can be silenced)

                // Pack data from current list 



            }
            catch (Exception ex)
            {
                Log.PrintError(ex.Message.ToString());
            }
        }

        public static void ChangeEndian()
        {
            
        }
    }
}
