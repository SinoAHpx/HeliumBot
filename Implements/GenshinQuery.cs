using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HeliumBot.Data.Config;
using HeliumBot.Data.Genshin;
using HeliumBot.Utils.Extensions;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace HeliumBot.Implements
{
    public class GenshinQuery
    {
        public string Uid { get; set; }

        private string UserAgent;

        //引自 https://github.com/Azure99/GenshinPlayerQuery/blob/565421d6a791c7ff01f10fdeab93b4384b7f0268/src/Core/GenshinAPI.cs#L13
        public GenshinConfig Config { get; set; }

        public GenshinQuery(string uid = null, GenshinConfig config = null)
        {
            UserAgent =
                $"Mozilla/5.0 (Linux; Android 11; Redmi Note 8 Build/RQ2A.210405.005; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/90.0.4430.91 Mobile Safari/537.36 miHoYoBBS/{Config.AppVersion}";
            Uid = uid;
            Config = config;
        }

        public GenshinQuery()
        {
            UserAgent =
                $"Mozilla/5.0 (Linux; Android 11; Redmi Note 8 Build/RQ2A.210405.005; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/90.0.4430.91 Mobile Safari/537.36 miHoYoBBS/{Config.AppVersion}";
        }

        public async Task<GenshinIndex> QueryGenshinIndex()
        {
            var url = Uid.GetGenshinQueryUrl();

            var client = BuildRequest(url);

            var responseJson = (await client.ExecuteAsync(new RestRequest(Method.GET))).Content;

            try
            {
                var genshinIndex = JObject.Parse(responseJson)["data"].ToObject<GenshinIndex>();

                return genshinIndex;
            }
            catch
            {
                return null;
            }
        }

        public async Task<GenshinAbyss> QueryGenshinAbyss(int schedule = 1)
        {
            var url = Uid.GetGenshinQueryUrl(GenshinQueryType.Abyss, schedule);
            var client = BuildRequest(url);

            var responseJson = (await client.ExecuteAsync(new RestRequest(Method.GET))).Content;
            
            try
            {
                var genshinIndex = JObject.Parse(responseJson)["data"].ToObject<GenshinAbyss>();

                return genshinIndex;
            }
            catch
            {
                return null;
            }
        }

        private RestClient BuildRequest(string url)
        {
            var rc = new RestClient {BaseUrl = new Uri(url)};

            rc.AddDefaultHeaders(new Dictionary<string, string>
            {
                {"User-Agent", UserAgent},
                {"DS", GetDs()},
                {"x-rpc-client_type", "5"},
                {"x-rpc-app_version", Config.AppVersion},
                {"Origin", "https://webstatic.mihoyo.com"},
                {"Accept", "application/json, text/plain, */*"},
                {"X-Requesed-With", "com.mihoyo.hyperion"},
                {"Sec-Fetch-Site", "same-site"},
                {"Sec-Fetch-Mode", "cors"},
                {"Sec-Fetch-Dest", "empty"},
                {"Referer", "https://webstatic.mihoyo.com/"},
                {"Accept-Encoding", "gzip, deflate"},
                {"Accept-Language", "en-GB,en;q=0.9,zh-CN;q=0.8,zh;q=0.7,en-US;q=0.6"},
                {"Cookie", Uid.IsChineseServer() ? Config.Cookie : Config.OsCookie}
            });
            
            return rc;
        }
        
        private string GetDs()
        {
            var version = Config.AppVersion.GetMd5();
            var time = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            var rs = GetRandomString(6);
            var re = $"salt={Config.Salt}&t={time}&r={rs}".GetMd5();
            
            return $"{time},{rs},{re}";
        }
        
        private string GetRandomString(int length)
        {
            var random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}