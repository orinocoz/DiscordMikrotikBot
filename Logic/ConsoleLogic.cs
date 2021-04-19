using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace DiscordMikrotikBot.Logic
{
    public static class ConsoleLogic
    {
        public static bool VerifyQuit(string data)
        {
            if (data.Contains("yes") || data.Contains("y") || data.Contains("ys") || data.Contains("ye") ||
                data.Contains("yeah") || data.Contains("yes please"))
            {
                return true;
            }
            else return false;
        }
    }
}
