using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordMikrotikBot.Lists.Enums
{
    public enum BranchChannel
    {
        Stable = 0,
        Development = 1,
        LongTerm = 2,
        Testing = 3,
        Unknown = 4 // Issue during parse, log to Logger class
    }
}
