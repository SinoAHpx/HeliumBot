using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HeliumBot.Data.Config;
using HeliumBot.Data.Genshin;
using Newtonsoft.Json;
using RestSharp;

namespace HeliumBot.Utils.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// in: {111,222,333,444} param=1 out:{222,333,444}
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static string[] CopyStringArray(this string[] ex, int startIndex)
        {
            var arr = new string[ex.Length - startIndex];
            
            Array.Copy(ex, startIndex, arr, 0, ex.Length - startIndex);

            return arr;
        }
        
        public static bool IsEmptyOrNull(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static bool IsNumber(this string source)
        {
            try
            {
                Convert.ToInt32(source);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetMd5(this string str)
        {
            var md5 = MD5.Create();
            var buffer = Encoding.Default.GetBytes(str);
            var bufferMd5 = md5.ComputeHash(buffer);
            var sb = new StringBuilder();

            foreach (var t in bufferMd5)
                sb.Append(t.ToString("x2"));

            return sb.ToString();
        }

        public static long ToLong(this string ex)
        {
            return Convert.ToInt64(ex);
        }

        public static DateTime TimestampToDateTime(this string ex)
        {
            return DateTimeOffset.FromUnixTimeSeconds(ex.ToLong()).DateTime;
        }

        public static string UrlToAvatarName(this string icon)
        {
            var raw = Path.GetFileName(icon);
            var path = @$".\Helium\{nameof(GenshinDictionary)}.json";
            if (File.Exists(path))
            {
                var dictionary = JsonConvert.DeserializeObject<GenshinDictionary>(File.ReadAllText(path));

                return dictionary.Characters.Any(x => raw.Contains(x.EnglishName))
                    ? dictionary.Characters.First(x => raw.Contains(x.EnglishName)).Name
                    : "旅行者";
            }

            throw new IOException("Please generate genshin dictionray first");
        }

        public static bool IsCommand(this string ex)
        {
            return ex.StartsWith("/");
        }

        public static async Task<string> Request(this string ex, Method method = Method.GET)
        {
            var rest = new RestClient
            {
                BaseUrl = new Uri(ex)
            };

            return (await rest.ExecuteAsync(new RestRequest(method))).Content;
        }

        public static GenshinServer GetGenshinServerByUid(this string ex)
        {
            return ex.First() switch
            {
                '9' => GenshinServer.TwHkMo,
                '8' => GenshinServer.Asia,
                '7' => GenshinServer.Europe,
                '6' => GenshinServer.America,
                '5' => GenshinServer.Pilipili,
                '1' => GenshinServer.Official,
                _ => throw new ArgumentException("No such server!")
            };
        }

        public static bool IsChineseServer(this string ex)
        {
            var re = ex.GetGenshinServerByUid();
            return re == GenshinServer.Official || re == GenshinServer.Pilipili;
        }

        public static string ToServerId(this GenshinServer ex)
        {
            return ex switch
            {
                GenshinServer.Official => "cn_gf01",
                GenshinServer.Pilipili => "cn_qd01",
                GenshinServer.Asia => "os_asia",
                GenshinServer.Europe => "os_euro",
                GenshinServer.America => "os_usa",
                GenshinServer.TwHkMo => "os_cht",
                _ => throw new ArgumentOutOfRangeException(nameof(ex), ex, null)
            };
        }

        public static string GetGenshinQueryUrl(this string uid, GenshinQueryType type = GenshinQueryType.Index, int abyssSchedule = 1)
        {
            var server = uid.GetGenshinServerByUid();
            var pre = uid.IsChineseServer() ? "api-takumi.mihoyo.com" : "api-os-takumi.mihoyo.com";

            return type switch
            {
                GenshinQueryType.Index => $"https://{pre}/game_record/genshin/api/index?server={server.ToServerId()}&role_id={uid}",
                GenshinQueryType.Abyss => $"https://{pre}/game_record/genshin/api/spiralAbyss?schedule_type={abyssSchedule}&server={server.ToServerId()}&role_id={uid}",
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };;
        }
    }
}