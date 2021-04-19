using System;
using DiscordMikrotikBot.Extensions;
using DiscordMikrotikBot.Lists;
using DiscordMikrotikBot.Logic;
using DiscordMikrotikBot.Tools;
using DiscordMikrotikBot.Trackers;
using DiscordMikrotikBot.Web;
using ConsoleColor = DiscordMikrotikBot.Tools.ConsoleColor;

namespace DiscordMikrotikBot
{
    class Program
    {
        static void Main(string[] args)
        {
            ////////////////////////////////////////////////////////////////////////
            ///        https://github.com/Crash0v3r1de/DiscordMikrotikBot        ///
            ////////////////////////////////////////////////////////////////////////
            //if(args != null & args[0] != "debug")
            Intro();
            new Setup().Exec();
            Console.WriteLine("Config loaded.....Parsing Changelog");

            while (true)
            {
                Rss feed = XMLMaster.ScrapeMikrotik(WebCore.ScrapeRouterOS());
                Console.WriteLine("Changelog parsed.....checking if new version needs to be announced");
                new VersionLogic().CheckChangelog(feed);
                Console.WriteLine("Checking of new versions complete.....Going to sleep in 15 seconds if no input received");
                var tmp = Reader.ReadLine(15*1000);
                // Handle input as a text menu at some point - null means no input
                ConsoleColor.Reset();
                Timers.Sleep(25,true);
            }


            //Console.Write("Testing complete, output is done, quit console window?: ");
            //var data = Console.ReadLine();
            //if(ConsoleLogic.VerifyQuit(data)) Environment.Exit(0);
            //else
            //{
            //    // Handle future options for testing
            //}
            ConsoleColor.Reset();
        }

        private static void Intro()
        {
            ConsoleColor.Cyan();
            Console.WriteLine("=================================================================================");
            Console.WriteLine("===          Discord Mikrotik RouterOS Version Bot - Webhook only             ===");
            Console.WriteLine("=================================================================================");
            ConsoleColor.Reset();
        }
    }
}
