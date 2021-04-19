using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ConsoleColor = DiscordMikrotikBot.Tools.ConsoleColor;

namespace DiscordMikrotikBot.Extensions
{
    class Reader
    {
        private static Thread inputThread;
        private static AutoResetEvent getInput, gotInput;
        private static string input;

        static Reader()
        {
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);
            inputThread = new Thread(reader);
            inputThread.IsBackground = true;
            inputThread.Start();
        }

        private static void reader()
        {
            while (true)
            {
                getInput.WaitOne();
                input = Console.ReadLine();
                gotInput.Set();
            }
        }

        public static string ReadLine(int timeOutMillisecs)
        {
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success)
                return input;
            else
            {
                ConsoleColor.Cyan();
                Console.WriteLine("");
                Console.WriteLine("Config change timeout expired......Starting bot with previously saved settings!");
                ConsoleColor.Reset();
                return null;
            }
        }
    }
}
