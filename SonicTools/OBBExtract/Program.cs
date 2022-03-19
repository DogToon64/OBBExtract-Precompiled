using System;
using System.IO;
using DimpsSonicLib;
using DimpsSonicLib.Archives;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBBExtract
{
    class Program
    {
        static void Main(string[] args)
        {
            bool flag = args.Length > 0;
            

            Console.WriteLine("<> OBBExtract <>\n----------------\n");

            try
            {
                if (!flag)
                {
                    Console.WriteLine("Usage: Drag an OBB file to extract its contents.");
                    Console.WriteLine("Please note that this application does NOT support any OBB from Episode 1\n");
                    Console.WriteLine("Press enter to Exit.");
                    Console.ReadLine();
                }

                // File Checking
                else if (flag)
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();

                    foreach (var arg in args)
                    {
                        if (File.Exists(arg))
                        {
                            var Type = AndroidOBB.DetermineOBBType(arg);

                            if (Type != AndroidOBB.OBBType.NotOBB)
                            {
                                Log.PrintInfo("Unpacking \"" + Path.GetFileName(arg) + "\", please wait...");

                                AndroidOBB.ExtractOBBFile(arg, Type);

                                Log.PrintInfo("\nDone\n");
                            }
                            else
                                Log.PrintError("File passed is not an OBB file.");
                        }
                        else
                            Log.PrintError("The given argument was not a file.");
                    }

                    Log.PrintInfo("\nComplete!");
                    watch.Stop();
                    TimeSpan ts = watch.Elapsed;
                    Console.WriteLine("Time elapsed: {0}{01:0}.{2:0} Seconds.", ts.Minutes != 0 ? (ts.Minutes + " Minute(s), "): "", ts.Seconds, ts.Milliseconds / 10);
                }
                else
                    Log.PrintError("OBBExtract was unable to parse the given argument(s)");
            }
            catch (Exception ex)
            {
                Log.PrintError("An error has occured:\n\n");
                Log.PrintError(ex.ToString());
                Log.Print("\nPress enter to Exit.");
                Console.ReadLine();
            }
            Log.Print("\nPress enter to Exit.");
            Console.ReadLine();
        }
    }
}
