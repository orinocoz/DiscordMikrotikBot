using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordMikrotikBot.Lists
{
    public class SettingsConfig
    {
        public string? LastSharedStableVersion { get; set; }
        public string? LastSharedDevVersion { get; set; }
        public string? LastSharedTestingVersion { get; set; }
        public string? LastSharedLongTermVersion { get; set; }
        public string BaseVersion { get; set; }
        public string StableWebhook { get; set; }
        public string TestingWebhook { get; set; }
        public string DevWebhook { get; set; }
        public string LongTermWebhook { get; set; }

        public List<string> PrcoessedVersions { get; set; } // Running memory table of versions mentioned on the server since this bot has started

    }
}
