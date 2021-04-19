using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordMikrotikBot.Tools
{
    public static class ConsoleColor
    {
        public static void Cyan()
        {
            Console.ForegroundColor = System.ConsoleColor.Cyan;
        }
        public static void Reset()
        {
            Console.ForegroundColor = System.ConsoleColor.White;
        }
        public static void Red()
        {
            Console.ForegroundColor = System.ConsoleColor.Red;
        }
        public static void Green()
        {
            Console.ForegroundColor = System.ConsoleColor.Green;
        }
    }
}
