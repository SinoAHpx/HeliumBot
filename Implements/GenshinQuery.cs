using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeliumBot.Data;
using HeliumBot.Utils.Extensions;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace HeliumBot.Implements
{
    public class GenshinQuery
    {
        public string AppVersion { get; set; }
        public string Uid { get; set; }

        private string UserAgent;

        //引自 https://github.com/Azure99/GenshinPlayerQuery/blob/565421d6a791c7ff01f10fdeab93b4384b7f0268/src/Core/GenshinAPI.cs#L13
        private string Salt = "14bmu1mz0yuljprsfgpvjh3ju2ni468r";

        //TODO: 把Cookie的读取写进配置文件里
        private string Cookie =
            "aliyungf_tc=d1e8757fc7487bf628d68bd7fe9d74df6bacc49e50c1adcdf1e94399729aec0e; _MHYUUID=39447d6f-3c33-4725-a39e-079e2684a668; _ga_ZBNHQCY81B=GS1.1.1619417587.1.0.1619417587.0; _ga=GA1.2.1830700132.1619417550; _gid=GA1.2.1931531820.1619774946; ltoken=0BZfgU4nwjUGMsy8HVkz0ITEgnQLUG5MoSaA5T3L; ltuid=162906166; account_id=162906166; login_ticket=FDmpxxLpKG9iBwN3VlFcRn5Gpa4Ni88S5QNVORfF; cookie_token=4W73JXBDgxOUSq6cMXERws4wnIKcOV2UlrsVchBq; _gat=1";

        public GenshinQuery(string appVersion = null, string uid = null)
        {
            AppVersion = appVersion;
            Uid = uid;
            UserAgent =
                $"Mozilla/5.0 (Linux; Android 11; Redmi Note 8 Build/RQ2A.210405.005; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/90.0.4430.91 Mobile Safari/537.36 miHoYoBBS/{AppVersion}";
        }

        public GenshinQuery()
        {
            UserAgent =
                $"Mozilla/5.0 (Linux; Android 11; Redmi Note 8 Build/RQ2A.210405.005; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/90.0.4430.91 Mobile Safari/537.36 miHoYoBBS/{AppVersion}";
        }

        public async Task<GenshinIndex> QueryGenshinIndex()
        {
            var url = $"https://api-takumi.mihoyo.com/game_record/genshin/api/index?server=cn_gf01&role_id={Uid}";
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

        private RestClient BuildRequest(string url)
        {
            var rc = new RestClient {BaseUrl = new Uri(url)};

            rc.AddDefaultHeaders(new Dictionary<string, string>
            {
                {"User-Agent", UserAgent},
                {"DS", GetDs()},
                {"x-rpc-client_type", "5"},
                {"x-rpc-app_version", AppVersion},
                {"Origin", "https://webstatic.mihoyo.com"},
                {"Accept", "application/json, text/plain, */*"},
                {"X-Requesed-With", "com.mihoyo.hyperion"},
                {"Sec-Fetch-Site", "same-site"},
                {"Sec-Fetch-Mode", "cors"},
                {"Sec-Fetch-Dest", "empty"},
                {"Referer", "https://webstatic.mihoyo.com/"},
                {"Accept-Encoding", "gzip, deflate"},
                {"Accept-Language", "en-GB,en;q=0.9,zh-CN;q=0.8,zh;q=0.7,en-US;q=0.6"},
                {"Cookie", Cookie}
            });
            
            return rc;
        }
        
        private string GetDs()
        {
            var version = AppVersion.GetMd5();
            var time = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            var rs = GetRandomString(6);
            var re = $"salt={Salt}&t={time}&r={rs}".GetMd5();
            
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