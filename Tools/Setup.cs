using System;
using System.Collections.Generic;
using System.Text;
using DiscordMikrotikBot.Lists.Enums;
using DiscordMikrotikBot.Trackers;

namespace DiscordMikrotikBot.Tools
{
    public class ChannelConfig
    {
        public BranchChannel Channel { get; set; }
        public string webhook { get; set; }
    }

    public class Setup
    {
        private string stableWebhook;
        private string devWebhook;
        private string testingWebhook;
        private string longtermWebhook;
        private string mainWebhook;
        private string baseVersion;
        private List<ChannelConfig> Channels = new List<ChannelConfig>();



        public void DummyData()
        {
            RunningSettings.BaseVersion = "6.48";
            RunningSettings.StableWebhook = "https://discord.com/api/webhooks/833410285914161162/XKUFLToD5bM0R4S8ePgmkxo1vf5uRF9o7zyk_oItnvL1AMYO-ygmZuI052pCgP_BGXn6";
        }

        private bool LoadConfig()
        {
            var tmp = IO.GetConfig();
            if (tmp != null)
            {
                RunningSettings.BaseVersion = tmp.BaseVersion;
                RunningSettings.LastSharedDevVersion = tmp.LastSharedDevVersion;
                RunningSettings.LastSharedLongTermVersion = tmp.LastSharedLongTermVersion;
                RunningSettings.LastSharedStableVersion = tmp.LastSharedStableVersion;
                RunningSettings.LastSharedTestingVersion = tmp.LastSharedTestingVersion;
                RunningSettings.StableWebhook = tmp.StableWebhook;
                RunningSettings.DevWebhook = tmp.DevWebhook;
                RunningSettings.TestingWebhook = tmp.TestingWebhook;
                RunningSettings.LongTermWebhook = tmp.LongTermWebhook;

                return true;
            }
            else return false; // Config not found or readable
        }

        private bool IsAYes(string whaaaaaaaaaaaa)
        {
            if (whaaaaaaaaaaaa.ToLower().Contains("yes") || whaaaaaaaaaaaa.ToLower().Contains("ye") || whaaaaaaaaaaaa.ToLower().Contains("ys") ||
                whaaaaaaaaaaaa.ToLower().Contains("yeah") || whaaaaaaaaaaaa.ToLower().Contains("fuck off") || whaaaaaaaaaaaa.ToLower().Contains("y"))
            {
                return true;
            }
            else return false;
        }

        private void PromptBaseVersion()
        {
            Console.Write("Minimum version to report on: ");
            baseVersion = Console.ReadLine();
            while (true) // Loop until the webhook is pasted correctly by the user
            {
                Console.WriteLine("Is this correct?:   " + baseVersion);
                Console.Write("Answer: ");
                if (IsAYes(Console.ReadLine()))
                {
                    break;
                }
                else
                {
                    Console.Write("Minimum version to report on: ");
                    baseVersion = Console.ReadLine();
                }
            }
        }

        private void PromptForSettings()
        {
            PromptBaseVersion();
            ConsoleColor.Green();
            Console.Write("Do you want to send all notifications to one channel?: ");
            if (IsAYes(Console.ReadLine()))
            {
                Console.Write("Webhook URL: ");
                mainWebhook = Console.ReadLine();
                while (true) // Loop until the webhook is pasted correctly by the user
                {
                    Console.WriteLine("Is this correct?:   "+mainWebhook);
                    Console.Write("Answer: ");
                    if (IsAYes(Console.ReadLine()))
                    {
                        break;
                    }
                    else
                    {
                        Console.Write("Webhook URL: ");
                        mainWebhook = Console.ReadLine();
                    }
                }
                ConsoleColor.Reset();
            }
            else
            {
                ConsoleColor.Green();
                foreach (var i in Enum.GetValues(typeof(BranchChannel)))
                {
                    if (!i.ToString().ToLower().Contains("unknown"))
                    {
                        ChannelConfig item = new ChannelConfig();
                        Console.Write(i.ToString() + " Channel Webhook: ");
                        string tmp = Console.ReadLine();
                        Console.WriteLine("Is this correct?:   " + tmp);
                        Console.Write("Answer: ");
                        if (IsAYes(Console.ReadLine()))
                        {
                            item.Channel = (BranchChannel)i;
                            item.webhook = tmp;
                            Channels.Add(item);
                        }
                        else
                        {
                            Console.Write(i.ToString() + " Channel Webhook: ");
                            tmp = Console.ReadLine();
                        }
                    }
                }
                ConsoleColor.Reset();
            }
            PostPrompt();
        }

        private void PostPrompt()
        {
            HashDatAss ass = new HashDatAss();
            if (Channels.Count == 0 || Channels.Count == 1)
            {
                RunningSettings.DevWebhook = ass.GetEncrypted(mainWebhook);
                RunningSettings.TestingWebhook = ass.GetEncrypted(mainWebhook);
                RunningSettings.StableWebhook = ass.GetEncrypted(mainWebhook);
                RunningSettings.LongTermWebhook = ass.GetEncrypted(mainWebhook);
            }
            else
            {
                foreach (var i in Channels)
                {
                    if (i.Channel == BranchChannel.Development)
                    {
                        RunningSettings.DevWebhook = ass.GetEncrypted(i.webhook);
                    }
                    if (i.Channel == BranchChannel.Testing)
                    {
                        RunningSettings.TestingWebhook = ass.GetEncrypted(i.webhook);
                    }
                    if (i.Channel == BranchChannel.Stable)
                    {
                        RunningSettings.StableWebhook = ass.GetEncrypted(i.webhook);
                    }
                    if (i.Channel == BranchChannel.LongTerm)
                    {
                        RunningSettings.LongTermWebhook = ass.GetEncrypted(i.webhook);
                    }
                }
            }
            RunningSettings.BaseVersion = baseVersion;
        }

        public void Exec()
        {
            // Loads config, prompts for initial webhook setup, counts down 5 seconds from start up to tweak settings if wanted
            if (LoadConfig())
            {
                // Count down to load program, interrupt to change webhooks and base version info
            }
            else
            {
                // Prompt for initial setup
                PromptForSettings();
            }
        }
    }
}
