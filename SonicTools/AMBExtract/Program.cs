using System;
using System.IO;
using DimpsSonicLib;

namespace AMBExtract
{
    class Program
    {
        public static string appDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        static void Main(string[] args)
        {
            Console.WriteLine("<> AMBExtract <>\n----------------\n");

            try
            {
                if (args.Length == 0)
                {
                    Console.WriteLine("Usage: Drag an AMB file to extract or drag a valid extraction directory to repack an AMB file.");
                    Console.WriteLine("Command-line Usage: AMBExtract <AMB File> OR AMBExtract <Path>");
                }

                // Unpack AMB file(s)
                else if (File.Exists(args[0]))
                {
                    // Change this later to process potential options like SkipIndexCreation, ChangeEndian, or UseCompression
                    if (args.Length > 1)
                    {
                        foreach (string file in args)
                        {

                            if (!File.GetAttributes(file).HasFlag(FileAttributes.Directory))
                            {
                                Log.Print("Unpacking \"" + Path.GetFileName(file) + "\"\n");
                                Functions.UnpackAMBFile(file);
                                Console.WriteLine("\n");
                            }
                            else
                                Log.PrintWarning("A directory was detected, ignoring.\n");
                        }
                    }
                    else
                    {
                        Log.Print("Unpacking \"" + Path.GetFileName(args[0]) + "\"");
                        Functions.UnpackAMBFile(args[0]);
                        Console.WriteLine();
                    }
                }

                // Pack AMB from Directory
                else if (File.GetAttributes(args[0]).HasFlag(FileAttributes.Directory))
                {
                    Log.Print("Directory \"" + args[0] + "\" was passed in.");
                    Functions.PackAMBFile(args[0]);
                }
                else
                    Log.PrintError("AMBExtract was unable to parse the given argument(s)");

            }
            catch (Exception ex)
            {
                Log.PrintError("An error has occured:\n" + ex.ToString());
            }
            Console.ReadLine();
        }
    }
}