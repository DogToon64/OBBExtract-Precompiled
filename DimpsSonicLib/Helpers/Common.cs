using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimpsSonicLib
{
    public class Common
    {

    }

    /// <summary>
    /// <b> WORK IN PROGRESS, DO NOT USE!!! </b>
    /// </summary>
    public class Logger
    {
        public static void Print(string text, bool pad = false)
        {
            if (pad)
                Console.WriteLine(text + "\n");
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
