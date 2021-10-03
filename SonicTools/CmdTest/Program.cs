using System;
using System.IO;

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
                    
                    
                }
                catch (Exception ex) { Console.WriteLine("Exception message: {0}\n\nException: {1}", ex.Message, ex); }

                Console.WriteLine("\n\nPress enter to exit."); Console.ReadLine();
            }
        }    
    }
}
