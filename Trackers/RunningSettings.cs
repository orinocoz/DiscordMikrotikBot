using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordMikrotikBot.Trackers
{
    public static class RunningSettings
    {
        public static string? LastSharedStableVersion { get; set; }
        public static string? LastSharedDevVersion { get; set; }
        public static string? LastSharedTestingVersion { get; set; }
        public static string? LastSharedLongTermVersion { get; set; }
        public static string BaseVersion { get; set; }
        public static string StableWebhook { get; set; }
        public static string TestingWebhook { get; set; }
        public static string DevWebhook { get; set; }
        public static string LongTermWebhook { get; set; }

        public static string HashToken = "ksdh9fuhnbsd;fs8ojifmosi8-literal-random-shit";
    }
}
