using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace DiscordMikrotikBot.Extensions
{
    public static class HttpWebRequestExtension
    {
        public static string ReadResponse(this HttpWebRequest request)
        {
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
