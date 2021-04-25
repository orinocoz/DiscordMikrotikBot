using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using DiscordMikrotikBot.Lists;
using DiscordMikrotikBot.Lists.Enums;
using DiscordMikrotikBot.Tools;
using DiscordMikrotikBot.Trackers;
using DiscordMikrotikBot.Web;

namespace DiscordMikrotikBot.Logic
{
    public class VersionLogic
    {
        private Version baseVer = new Version(); // Minimum version number to notify set by config
        private Version lastSent = new Version(); // Last version reported by bot
        private Version currentSent = new Version(); // version the function is checking
        // The above is just a reference from when I was coding this class


        private Version stableBaseVer = new Version();
        private Version stableCurrentVer = new Version();
        private Version stableLastSent = new Version();
        private Version TestingBaseVer = new Version();
        private Version TestingCurrentVer = new Version();
        private Version TestingLastSent = new Version();
        private Version LongTermBaseVer = new Version();
        private Version LongTermCurrentVer = new Version();
        private Version LongTermLastSent = new Version();
        private Version DevBaseVer = new Version();
        private Version DevCurrentVer = new Version();
        private Version DevLastSent = new Version();


        private BranchChannel ParseChannel(string data)
        {
            if (data.Contains("Stable"))
            {
                return BranchChannel.Stable;
            }
            if (data.Contains("Testing"))
            {
                return BranchChannel.Testing;
            }
            if (data.Contains("Development"))
            {
                return BranchChannel.Development;
            }
            if (data.Contains("Long-term"))
            {
                return BranchChannel.LongTerm;
            }
            return BranchChannel.Unknown; // Log to Logger class - failure to parse channel correctly
        }
        private string ParseVersion(string raw)
        {
            return Regex.Match(raw, "h3>(.*?)changelog").Groups[1].Value.Trim();
        }
        private SettingsConfig ParseConfig()
        {
            SettingsConfig tmp = new SettingsConfig();
            tmp.BaseVersion = RunningSettings.BaseVersion;
            tmp.LastSharedDevVersion = RunningSettings.LastSharedDevVersion;
            tmp.LastSharedLongTermVersion = RunningSettings.LastSharedLongTermVersion;
            tmp.LastSharedStableVersion = RunningSettings.LastSharedStableVersion;
            tmp.LastSharedTestingVersion = RunningSettings.LastSharedTestingVersion;
            tmp.DevWebhook = RunningSettings.DevWebhook;
            tmp.LongTermWebhook = RunningSettings.LongTermWebhook;
            tmp.StableWebhook = RunningSettings.StableWebhook;
            tmp.TestingWebhook = RunningSettings.TestingWebhook;

            return tmp;
        }
        private bool StableNotificationNeeded(string version)
        {
            // formatting and parsing
            if (!RunningSettings.BaseVersion.Contains("rc") & !RunningSettings.BaseVersion.Contains("beta")) stableBaseVer = Version.Parse(RunningSettings.BaseVersion.Replace("rc", ".0."));
            if (RunningSettings.BaseVersion.Contains("rc")) stableBaseVer = Version.Parse(RunningSettings.BaseVersion.Replace("rc", ".0."));
            if (RunningSettings.BaseVersion.Contains("beta")) stableBaseVer = Version.Parse(RunningSettings.BaseVersion.Replace("beta", "."));
            if (!version.Contains("rc") & !version.Contains("beta")) stableCurrentVer = Version.Parse(version);
            if (version.Contains("rc")) stableCurrentVer = Version.Parse(version.Replace("rc", ".0."));
            if (version.Contains("beta")) stableCurrentVer = Version.Parse(version.Replace("beta", "."));

            // Only fires off if the program has never sent a notification before
            if (RunningSettings.LastSharedStableVersion != null)
            {
                stableLastSent = Version.Parse(RunningSettings.LastSharedStableVersion);
            }



            if (stableBaseVer < stableCurrentVer & stableLastSent < stableCurrentVer) // Last sent version number is lower so new number is higher aka newest/newer version not sent
            {
                RunningSettings.LastSharedStableVersion = stableCurrentVer.ToString();
                return true;
            }
            else return false;
        }
        private bool TestingNotificationNeeded(string version)
        {
            // formatting and parsing
            if (!RunningSettings.BaseVersion.Contains("rc") & !RunningSettings.BaseVersion.Contains("beta")) TestingBaseVer = Version.Parse(RunningSettings.BaseVersion.Replace("rc", ".0."));
            if (RunningSettings.BaseVersion.Contains("rc")) TestingBaseVer = Version.Parse(RunningSettings.BaseVersion.Replace("rc", ".0."));
            if (RunningSettings.BaseVersion.Contains("beta")) TestingBaseVer = Version.Parse(RunningSettings.BaseVersion.Replace("beta", "."));
            if (!version.Contains("rc") & !version.Contains("beta")) TestingCurrentVer = Version.Parse(version);
            if (version.Contains("rc")) TestingCurrentVer = Version.Parse(version.Replace("rc", ".0."));
            if (version.Contains("beta")) TestingCurrentVer = Version.Parse(version.Replace("beta", "."));

            // Only fires off if the program has never sent a notification before
            if (RunningSettings.LastSharedTestingVersion != null)
            {
                TestingLastSent = Version.Parse(RunningSettings.LastSharedTestingVersion);
            }



            if (TestingBaseVer < TestingCurrentVer & TestingLastSent < TestingCurrentVer) // Last sent version number is lower so new number is higher aka newest/newer version not sent
            {
                RunningSettings.LastSharedTestingVersion = TestingCurrentVer.ToString();
                return true;
            }
            else return false;
        }
        private bool LongTermNotificationNeeded(string version)
        {
            // formatting and parsing
            if (!RunningSettings.BaseVersion.Contains("rc") & !RunningSettings.BaseVersion.Contains("beta")) LongTermBaseVer = Version.Parse(RunningSettings.BaseVersion.Replace("rc", ".0."));
            if (RunningSettings.BaseVersion.Contains("rc")) LongTermBaseVer = Version.Parse(RunningSettings.BaseVersion.Replace("rc", ".0."));
            if (RunningSettings.BaseVersion.Contains("beta")) LongTermBaseVer = Version.Parse(RunningSettings.BaseVersion.Replace("beta", "."));
            if (!version.Contains("rc") & !version.Contains("beta")) LongTermCurrentVer = Version.Parse(version);
            if (version.Contains("rc")) LongTermCurrentVer = Version.Parse(version.Replace("rc", ".0."));
            if (version.Contains("beta")) LongTermCurrentVer = Version.Parse(version.Replace("beta", "."));

            // Only fires off if the program has never sent a notification before
            if (RunningSettings.LastSharedLongTermVersion != null)
            {
                LongTermLastSent = Version.Parse(RunningSettings.LastSharedLongTermVersion);
            }



            if (LongTermBaseVer < LongTermCurrentVer & LongTermLastSent < LongTermCurrentVer) // Last sent version number is lower so new number is higher aka newest/newer version not sent
            {
                RunningSettings.LastSharedLongTermVersion = LongTermCurrentVer.ToString();
                return true;
            }
            else return false;
        }
        private bool DevNotificationNeeded(string version)
        {
            // formatting and parsing
            if (!RunningSettings.BaseVersion.Contains("rc") & !RunningSettings.BaseVersion.Contains("beta")) DevBaseVer = Version.Parse(RunningSettings.BaseVersion.Replace("rc", ".0."));
            if (RunningSettings.BaseVersion.Contains("rc")) DevBaseVer = Version.Parse(RunningSettings.BaseVersion.Replace("rc", ".0."));
            if (RunningSettings.BaseVersion.Contains("beta")) DevBaseVer = Version.Parse(RunningSettings.BaseVersion.Replace("beta", "."));
            if (!version.Contains("rc") & !version.Contains("beta")) DevCurrentVer = Version.Parse(version);
            if (version.Contains("rc")) DevCurrentVer = Version.Parse(version.Replace("rc", ".0."));
            if (version.Contains("beta")) DevCurrentVer = Version.Parse(version.Replace("beta", "."));

            // Only fires off if the program has never sent a notification before
            if (RunningSettings.LastSharedDevVersion != null)
            {
                DevLastSent = Version.Parse(RunningSettings.LastSharedDevVersion);
            }



            if (DevBaseVer < DevCurrentVer & DevLastSent < DevCurrentVer) // Last sent version number is lower so new number is higher aka newest/newer version not sent
            {
                RunningSettings.LastSharedDevVersion = DevCurrentVer.ToString();
                return true;
            }
            else return false;
        }
        private bool NotificationNeeded(string version,BranchChannel channel)
        {

            if (channel == BranchChannel.Stable) return StableNotificationNeeded(version);
            if (channel == BranchChannel.Testing) return TestingNotificationNeeded(version);
            if (channel == BranchChannel.LongTerm) return LongTermNotificationNeeded(version);
            if (channel == BranchChannel.Development) return DevNotificationNeeded(version);

            return false;
        }
        private string Decrypt(BranchChannel channel)
        {
            HashDatAss ass = new HashDatAss();

            if (channel == BranchChannel.Development)
            {
                return ass.GetDecrypted(RunningSettings.DevWebhook);
            }
            if (channel == BranchChannel.Stable)
            {
                return ass.GetDecrypted(RunningSettings.StableWebhook);
            }
            if (channel == BranchChannel.Testing)
            {
                return ass.GetDecrypted(RunningSettings.TestingWebhook);
            }
            if (channel == BranchChannel.LongTerm)
            {
                return ass.GetDecrypted(RunningSettings.LongTermWebhook);
            }

            return null; // Log to Logger class for failure
        }
        private List<ItemJson> ParseXML(Rss rss)
        {
            List<ItemJson> tmp = new List<ItemJson>();
            foreach (var i in rss.Channel.Item)
            {
                var branch = ParseChannel(i.Category);
                if (branch != BranchChannel.Unknown)
                {
                    ItemJson tmpItem = new ItemJson();
                    tmpItem.Branch = branch;
                    tmpItem.Version = ParseVersion(i.Description); // If null or error log in Logger class
                    tmpItem.Changelog = i.Description;
                    tmpItem.URL = i.Link2;
                    tmp.Add(tmpItem);
                }
                else
                {
                    // Log to Logger class and handle accordingly
                }
            }

            return tmp; // If null or error log in Logger class
        }
        /// We can add switches for handling of different options down the road from this
        private string FormatWebhook(string branch, string version, string url)
        {
            return $"{branch} | {version} | [New version published - click here to read about it.]({url})";
        }
        /// This will eventually be the logic for changing settings - still needs to be coded and implemented
        private void ChangeSettingsPrompt()
        {

        }


        public void CheckChangelog(Rss feed)
        {
            foreach (var i in ParseXML(feed))
            {
                if (NotificationNeeded(i.Version,i.Branch))
                {
                    // Send notification and save the running config data
                    new DiscordWeb().SendWebhook(FormatWebhook(i.Branch.ToString(),i.Version,i.URL), Decrypt(i.Branch),i.URL);
                    IO.SaveConfig(ParseConfig());
                }
            }
        }

    }
}
