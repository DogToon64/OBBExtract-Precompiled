using System;
using DimpsSonicLib;

namespace DimpsArcExtract
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("<> Dimps Archive Extract <>\n---------------------------\n");

            if (args.Length == 0)
            {
                Console.WriteLine("Usage: Drag an archive (AMB or OBB) file to extract.");
                Console.WriteLine("* Please note that this application does not support any OBB from Episode 1,\n" +
                    "  as well as AMB files from the Windows Phone version of Episode 1.\n");
                Console.WriteLine("Command-line Usage: ");
            }
            else
            {
                int i = 0;
                foreach (string arg in args)
                {
                    try
                    {
                        Functions.DetectArcType(arg);    i++;
                        if (i != args.Length) 
                        { 
                            Console.WriteLine("Press enter to begin next file"); 
                            Console.ReadLine();
                        }
                    }
                    catch (Exception ex)
                    { Log.PrintError($"Exception message: {ex.Message}\n\nException: {ex}\n\n\n"); }
                }        
            }

            Console.WriteLine("\n\nPress enter to exit."); Console.ReadLine();
        }
    }
}
