using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostEngine.Engine.Utils
{
    public class Log
    {
        public static void Normal(string msg) 
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[Message]: {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Info(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"[Info]: {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Warning(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARNING]: {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR]: {msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
