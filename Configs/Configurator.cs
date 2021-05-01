using System;
using System.IO;
using HeliumBot.Data.Config;
using HeliumBot.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HeliumBot.Configs
{
    public static class Configurator<T> where T : new()
    {
        private static readonly string _configPath = $@".\Helium\{typeof(T).Name}.json";
        
        static Configurator()
        {
            if (!Directory.Exists(@".\Helium"))
            {
                Directory.CreateDirectory(@".\Helium");
            }
        }

        public static T ReadConfig()
        {
            var text = File.ReadAllText(_configPath);
            Logger.Log("BotConfig json:", text);

            try
            {
                var re = JObject.Parse(text).ToObject<T>();
                Logger.Log("Read successful");

                return re;
            }
            catch (Exception e)
            {
                Logger.Log("Read failed. Reason:", e.Message);

                return new T();
            }
        }

        public static void WriteConfig(T t)
        {
            var json = JsonConvert.SerializeObject(t, Formatting.Indented);
            Logger.Log("BotConfig json generated");
            Logger.Log(json);

            File.WriteAllText(_configPath, json);
            Logger.Log("BotConfig json wroted");
        }
        
        public static void GenerateConfig()
        {
            var json = JsonConvert.SerializeObject(new T(), Formatting.Indented);
            Logger.Log("BotConfig json generated");
            Logger.Log(json);

            File.WriteAllText(_configPath, json);
            Logger.Log("BotConfig json wroted");
        }
    }
}