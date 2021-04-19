using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using DiscordMikrotikBot.Extensions;

namespace DiscordMikrotikBot.Web
{
    public static class WebCore
    {
        public static string ScrapeRouterOS()
        {
            var request =
                WebRequest.CreateHttp("https://mikrotik.com/download.rss");
            request.Method = "GET";
            request.Host = "mikrotik.com";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:64.0) Gecko/20100101 Firefox/64.0";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Headers[HttpRequestHeader.AcceptLanguage] = "en-us";
            request.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate, br";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            request.KeepAlive = true;
            request.Timeout = 11000;
            request.Headers["Upgrade-Insecure-Requests"] = "1";
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            try
            {
                return request.ReadResponse();
            }
            catch (Exception ex)
            {
                // Log to Logger class
                return null; // return null to report failure has happened
            }
        }
    }
}
