using System;
using System.Linq;
using HeliumBot.Utils.Extensions;

namespace HeliumBot.Implement
{
    public class GenshinQuery
    {
        public string AppVersion { get; set; }
        
        
        
        public string GetDS()
        {
            var version = AppVersion.GetMd5();
            var time = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            var rs = GetRandomString(6);
            var re = $"salt={version}&t={time}&r={rs}".GetMd5();

            return $"{time},{rs},{re}";
        }
        
        public string GetRandomString(int length)
        {
            var random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}