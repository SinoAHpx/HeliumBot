using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using HeliumBot.Data.Config;
using HeliumBot.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HeliumBot.Configs
{
    public static class BotConfigurator
    {
        private static readonly string _configPath = @".\HeliumBotConfig.json";

        public static BotConfig ReadConfig()
        {
            var text = File.ReadAllText(_configPath);
            Logger.Log("BotConfig json:", text);

            try
            {
                var re = JObject.Parse(text).ToObject<BotConfig>();
                Logger.Log("Read successful");
                return re;
            }
            catch (Exception e)
            {
                Logger.Log("Read failed. Reason:", e.Message);
                return null;
            }
        }

        public static void WriteConfig(BotConfig botConfig)
        {
            var json = JsonConvert.SerializeObject(botConfig, Formatting.Indented);
            Logger.Log("BotConfig json generated");
            Logger.Log(json);

            File.WriteAllText(_configPath, json);
            Logger.Log("BotConfig json wroted");
        }

        public static void GenerateConfig()
        {
            var json = JsonConvert.SerializeObject(new BotConfig(), Formatting.Indented);
            Logger.Log("BotConfig json generated");
            Logger.Log(json);

            File.WriteAllText(_configPath, json);
            Logger.Log("BotConfig json wroted");
        }
    }
}