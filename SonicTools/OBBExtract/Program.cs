using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
                    Console.WriteLine("Please note that this application does not yet support LPKv2 or any OBB from Episode 1\n");
                    Console.WriteLine("Press enter to Exit.");
                    Console.ReadLine();
                }

                // File Checking
                else if (flag)
                {
                    var arg = args[0];
                    if (File.Exists(arg))
                    {
                        var Type = Functions.DetermineOBBType(arg);

                        if (Type != Functions.OBBType.NotOBB)
                        {
                            Logger.Print("Unpacking \"" + Path.GetFileName(arg) + "\", Please wait...\n");
                            Functions.ExtractOBBFile(arg, Type);
                            Logger.PrintInfo("\nComplete!");
                        }
                        else
                            Logger.PrintError("File passed is not an OBB file.");

                    }

                    else
                        Logger.PrintError("The given argument was not a file.");
                }

                else
                    Logger.PrintError("OBBExtract was unable to parse the given argument(s)");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error has occured:\n\n" + ex.Message);
                Console.WriteLine("\nPress enter to Exit.");
                Console.ReadLine();
            }
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}
