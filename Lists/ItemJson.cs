using System;
using System.Collections.Generic;
using System.Text;
using DiscordMikrotikBot.Lists.Enums;

namespace DiscordMikrotikBot.Lists
{
    public class ItemJson
    {
        public string Version { get; set; }
        public BranchChannel Branch { get; set; }
        public string Changelog { get; set; }
    }
}
