using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMikrotikBot.Trackers
{
    public static class Timers
    {
        public static void Sleep(int mins,bool debug = false)
        {
            if (debug)
            {
                for (int a = mins; a >= 0; a--)
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write("Next execution ... {0} ", a);
                    System.Threading.Thread.Sleep(1000);
                }
            }
            else
            {
                for (int a = mins; a >= 0; a--)
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write("Next execution ... {0} Mins", a);
                    System.Threading.Thread.Sleep(160000);
                }
            }

        }
            
    }
}

