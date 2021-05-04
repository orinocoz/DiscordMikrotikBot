using System;
using DiscordMikrotikBot.Extensions;
using DiscordMikrotikBot.Logic;
using DiscordMikrotikBot.Tools;
using DiscordMikrotikBot.Trackers;
using DiscordMikrotikBot.Web;
using ConsoleColor = DiscordMikrotikBot.Tools.ConsoleColor;

namespace DiscordMikrotikBot
{
    class Program
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                      https://github.com/Crash0v3r1de/DiscordMikrotikBot                       ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        /// If any code changes have been done I only request that you reference my github in your source ///
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        private static int loopedCount = 0;
        static void Main(string[] args)
        {

            //if(args != null & args[0] != "debug")
            //Console.Write("Testing complete, output is done, quit console window?: ");
            //var data = Console.ReadLine();
            //if(ConsoleLogic.VerifyQuit(data)) Environment.Exit(0);
            //else
            //{
            //    // Handle future options for testing
            //}



            Intro();
            new Setup().Exec();
            Console.WriteLine("Config loaded.....Parsing Changelog");

            while (true)
            {
                Rss feed = XMLMaster.ScrapeMikrotik(WebCore.ScrapeRouterOS());
                Console.WriteLine("Changelog parsed.....checking if new version needs to be announced");
                new VersionLogic().CheckChangelog(feed);
                Console.WriteLine("Checking of new versions complete..... Going to sleep in 15 seconds if no input received");
                Console.Write("(press enter): ");
                var tmp = Reader.ReadLine(15*1000);
                // Handle input as a text menu at some point - null means no input
                if (!String.IsNullOrWhiteSpace(tmp))
                {
                    ConsoleColor.Red();
                    Console.WriteLine("Config change while running is currently not a function yet");
                    Console.WriteLine("Please edit the config file direct in the \"Config\" folder where this binary sits...(restart required)");
                    ConsoleColor.Reset();
                }

                ConsoleColor.Reset();
                Timers.Sleep(25,true);
                Looped();
            }

        }

        private static void Intro()
        {
            ConsoleColor.Cyan();
            Console.WriteLine("=================================================================================");
            Console.WriteLine("===          Discord Mikrotik RouterOS Version Bot - Webhook only             ===");
            Console.WriteLine("=================================================================================");
            ConsoleColor.Reset();
        }

        private static void Looped()
        {
            string times = "times";
            if (loopedCount == 0) times = "time";
            else times = "times";
            Console.Clear();
            loopedCount++;
            Intro();
            ConsoleColor.Green();
            Console.WriteLine($"Checked {loopedCount} {times}...");
            ConsoleColor.Reset();
        }
    }
}
