using System;
using System.IO;
using System.Reflection;
using System.Threading;
using DimpsSonicLib;

namespace AMBExtract
{
    class Program
    {
        public static string appDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        static void Main(string[] args)
        {
#if STAGING
            Console.WriteLine("<> AMBExtract <>\n----------------");
            Log.PrintWarning(" >> THIS IS A STAGING BUILD, THERE *WILL* BE BUGS! <<\n");
#else
            Console.WriteLine("<> AMBExtract <>\n----------------\n");
#endif

            try
            {
                if (args.Length == 0)
                {
                    //Console.WriteLine("Usage: Drag an AMB file to extract or drag a valid extraction directory to repack an AMB file.");
                    //Console.WriteLine("Command-line Usage: AMBExtract <AMB File> OR AMBExtract <Path>");
                    Console.WriteLine("Usage: Drag one or more AMB files to extract contents. **Repacking AMBs is not supported at this time.");
                    Console.WriteLine("Command-line Usage: AMBExtract <AMB File>");

                    Console.WriteLine("\nExiting in 5 seconds...");
                    Thread.Sleep(5000);
                    Environment.Exit(0);
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
                                Log.PrintInfo("Reading \"" + Path.GetFileName(file) + "\"\n");
                                Functions.UnpackAMBFile(file);
                                Console.WriteLine("\n");
                            }
                            else
                                Log.PrintWarning("A directory was detected, ignoring.\n");
                        }
                    }
                    else
                    {
                        Log.PrintInfo("Reading \"" + Path.GetFileName(args[0]) + "\"");
                        Functions.UnpackAMBFile(args[0]);
                        Console.WriteLine();
                    }
                }

                // Pack AMB from Directory
                else if (File.GetAttributes(args[0]).HasFlag(FileAttributes.Directory))
                {
                    Log.PrintInfo("Directory \"" + args[0] + "\"\n");
                    Functions.PackAMBFile(args[0]);
                }

                else
                {
                    throw new Exception("AMBExtract was unable to parse the given argument(s)");
                }

                Log.PrintInfo("Complete!");
#if DEBUG
                Console.ReadLine();
#elif STAGING
                Console.ReadLine();
#endif
            }
            catch (Exception ex)
            {
                Log.PrintError("An error has occured:\n" + ex.ToString());
                Log.Print("\n\nPress Enter to exit.");
                Console.ReadLine();
            }
        }
    }
}