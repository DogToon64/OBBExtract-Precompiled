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
                // Create extraction directory
                string newDir = Utility.CreateFolderFromFileName(arg);
                Directory.CreateDirectory(newDir);

                // Load AMB file
                Stream stream = File.OpenRead(arg);

                // Parse the AMB
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
                        binder.FileSys.Count != 0 && binder.FileSys[i] != "" ? binder.FileSys[i] : $"NAME IS NULL! (ID:{i})",
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
                for (int i = 0; i < (int)binder.SubHeader.fileCount; i++)
                {
                    bool error = false;
                    string fileName = binder.FileSys.Count != 0 && binder.FileSys[i] != "" ? binder.FileSys[i] : $"file_{i}.bin";

                    Console.WriteLine("Extracting \"{0}\"", fileName);

                    // Check the file data for a ZIP! signature
                    var fileCheck = new ExtendedBinaryReader(new MemoryStream(binder.FileData[i]));
                    fileCheck.JumpTo(0);

                    // Decompress the file if ZIP! signature is detected
                    if (fileCheck.ReadSignature() == "ZIP!")
                    {
                        byte[] fileOut = null;
                        Log.PrintWarning(" > File appears to be compressed, attempting to decompress it...");
                        fileCheck.JumpTo(8);

                        // Attempt to figure out what kind of ZIP! header we're dealing with.
                        if (fileCheck.ReadUInt32() != 0)
                        {
                            try     // ZIP! type is uint32
                            {
                                byte[] fileIn = fileCheck.ReadBytes(binder.FileData[i].Length);
                                fileOut = Compression.DecompressZlibChunk(fileIn);
                            }
                            catch
                            { error = true; }
                        }
                        else
                        {
                            fileCheck.JumpTo(20);
                            try     // ZIP! type is uint64
                            {
                                byte[] fileIn = fileCheck.ReadBytes(binder.FileData[i].Length);
                                fileOut = Compression.DecompressZlibChunk(fileIn);
                            }
                            catch
                            { error = true; }
                        }

                        // Write the result file. Save raw file data if ZIP! section fails
                        if (!error)
                        {
                            Console.SetCursorPosition(67, (Console.CursorTop - 1));
                            Log.PrintInfo("Done");
                            File.WriteAllBytes((newDir + "\\" + fileName), fileOut);
                        }
                        else
                        {
                            Log.PrintError("Failed decompression attempt! Saving original data instead.");
                            File.WriteAllBytes((newDir + "\\" + fileName), binder.FileData[i]);
                        }                     
                    }
                    else
                    {
                        File.WriteAllBytes((newDir + "\\" + fileName), binder.FileData[i]);
                    }            
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
