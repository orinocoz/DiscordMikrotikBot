using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DiscordMikrotikBot.Lists;
using Newtonsoft.Json;

namespace DiscordMikrotikBot.Tools
{
    public static class IO
    {
        private static string configFile = "Config\\settings.json";
        public static bool MissingConfigFolder()
        {
            string tmp = "Config";
            return !Directory.Exists("Config");
        }

        public static bool SaveConfig(SettingsConfig config)
        {
            if (MissingConfigFolder()) Directory.CreateDirectory("Config");
            try
            {
                using (StreamWriter sw = File.CreateText(configFile))
                {
                    sw.WriteLine(JsonConvert.SerializeObject(config));
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log to Logger class
                return false;
            }
        }

        public static SettingsConfig GetConfig()
        {
            try
            {
                return JsonConvert.DeserializeObject<SettingsConfig>(File.ReadAllText(configFile));
            }
            catch (Exception ex)
            {
                // Log to Logger class
                return null; // Return null if Json fails so we can handle the failure during logic
            }
        }
    }
}
