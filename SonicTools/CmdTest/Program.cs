using System;
using System.IO;
using DimpsSonicLib.IO;
using System.Threading;

namespace CmdTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments given.");
            }
            else
            {
                try
                {
                    // Test read on the name table dumped from an S4E2 Android OBB file.
                    Stream stream = File.OpenRead(@"C:\Users\Kass\Desktop\OBBNAME.TBL");
                    ExtendedBinaryReader reader = new ExtendedBinaryReader(stream);

                    int numDirs = 119;
                    int numFiles = 2317;

                    string[] array = reader.ReadNullTerminatedStringArray(numDirs + numFiles);

                    stream.Close();

                    int dirID  = 114;  // Should be "MSG/ANC"
                    int fileID = 1094; // Should be "MSG_ANC_DESIGN03_01_IT.AMB"

                    // Print evaluated file path embedded in nameTable, with ID correction to start from 0
                    Console.WriteLine("Evaluated file path: {0}/{1}",
                        array[dirID - 1], array[numDirs + (fileID - 1)] );
                    

                }
                catch (Exception ex) { Console.WriteLine("Exception message: {0}\n\nException: {1}", ex.Message, ex); }

                Console.WriteLine("\n\nPress enter to exit."); Console.ReadLine();
                //Console.WriteLine("\n\nExiting in 2 seconds."); Thread.Sleep(2000);
            }
        }    
    }
}
