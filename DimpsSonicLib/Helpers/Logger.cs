using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimpsSonicLib
{
    /// <summary>
    /// <b> WORK IN PROGRESS, DO NOT USE!!! </b>
    /// </summary>
    public static class Logger
    {
        public static void Print(string text, bool overwrite = false)
        {
            if (overwrite)
                Console.Write(text + "\r");
            else
                Console.WriteLine(text);
        }

        public static void PrintWarning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void PrintInfo(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void PrintError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
