using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using DiscordMikrotikBot.Extensions;
using DiscordMikrotikBot.Lists;
using Newtonsoft.Json;

namespace DiscordMikrotikBot.Web
{
    public class DiscordWeb
    {
        public bool SendWebhook(string raw,string webhook,string url)
        {
            // Instead of just sending string we will have the function requiring a ItemJson so we can send full details in correct format for POST webhook
            // https://gist.github.com/Birdie0/78ee79402a4301b1faf412ab5f1cdcf9

            // Basic simple webhook JSON template
            WebhookBasicJSON tmp = new WebhookBasicJSON();
            tmp.avatar_url = "https://991tech.org/cdn-img/winbox.png";
            tmp.content = raw;
            tmp.username = "Mikrotik Changelog Bot";


            var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(tmp));
            var request =
                WebRequest.CreateHttp(webhook);
            request.Method = "POST";
            request.UserAgent = "DiscordMikrotikChangelogBot/1.0";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            request.KeepAlive = true;
            request.Timeout = 11000;
            request.Headers["Upgrade-Insecure-Requests"] = "1";
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length); // Assigning POST data
                }
            }
            catch (Exception ex)
            {
                return false; // Log to Logger class because this has been a failure
            }
            string htmlResponse;
            try
            {
                htmlResponse = request.ReadResponse();

            }
            catch
            {
                return false; // Log to Logger class because this has been a failure
            }

            return false; // Log to Logger class because this has been a failure
        }
    }
}
