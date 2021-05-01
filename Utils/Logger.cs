using System;

namespace HeliumBot.Utils
{
    public static class Logger
    {
        public static void Log(string text)
        {
            Console.WriteLine($"[{DateTime.Now:s}] {text}");
        }

        public static void Log(params string[] text)
        {
            Console.WriteLine($"[{DateTime.Now:s}] {string.Join(" ", text)}");
        }
    }
}