using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HeliumBot.Configs;
using HeliumBot.Data.Config;
using HeliumBot.Implements;
using HeliumBot.Plugins.Group;
using HeliumBot.Utils;
using HeliumBot.Utils.Extensions;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin;

namespace HeliumBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var isDebugging = false;
            
            #region Initilize Bot

            Bot bot = null;

            BotConfig botConfig = null;
            GenshinConfig genshinConfig = null;

            Console.WriteLine("Use /help for command help");
            while (!isDebugging)
            {
                switch (Console.ReadLine())
                {
                    case "/exit":
                        if (bot != null) await bot.Terminate();
                        return;
                    case "/bot config write":
                        Configurator<BotConfig>.WriteConfig(botConfig);
                        Logger.Log("Bot config wroted");
                        break;
                    case "/bot config read":
                        botConfig = Configurator<BotConfig>.ReadConfig();
                        Logger.Log("Tried to read bot config");
                        break;
                    case "/bot config generate":
                        Configurator<BotConfig>.GenerateConfig();
                        Logger.Log("Empty bot config generated");
                        break;
                    case "/genshin config write":
                        Configurator<GenshinConfig>.WriteConfig(genshinConfig);
                        Logger.Log("Bot config wroted");
                        break;
                    case "/genshin config read":
                        genshinConfig = Configurator<GenshinConfig>.ReadConfig();
                        Logger.Log("Tried to read config");
                        break;
                    case "/genshin config generate":
                        Configurator<GenshinConfig>.GenerateConfig();
                        Logger.Log("Empty config generated");
                        break;
                    case "/bot launch":
                        if (botConfig != null)
                            bot = new Bot
                            {
                                SessionHost = botConfig.SessionHost,
                                SessionPort = botConfig.SessionPort,
                                SessionKey = botConfig.SessionKey,
                                BotQQ = botConfig.BotQQ,
                                Plugins = new IPlugin[]
                                {
                                    new HelloWorldPlugin(),
                                    new GenshinPlugin()
                                }
                            };

                        if (bot != null) await bot.Launch();
                        break;
                    case "/bot terminate":
                        await bot!.Terminate();
                        break;
                    default:
                        Console.WriteLine("Usage:");
                        Console.WriteLine("/help : view commands help");
                        Console.WriteLine("/exit : exit plugin and dispose resources");
                        Console.WriteLine("/bot launch : launch bot by bot config");
                        Console.WriteLine("/bot terminate : stop bot and dispose resources but not exit plugin");
                        Console.WriteLine("/<module> config generate : generate a empty config json");
                        Console.WriteLine("/<module> config read : read config json");
                        Console.WriteLine("/<module> config write : write config json");
                        break;
                }
            }

            #endregion
            //
            // var gc = Configurator<GenshinConfig>.ReadConfig();
            // var genshin = new GenshinQuery
            // {
            //     AppVersion = gc.AppVersion,
            //     Salt = gc.Salt,
            //     Cookie = gc.Cookie,
            //     Uid = "152372349"
            // };
            //
            // var gi = await genshin.getSpiralAbyss();
            //
            // File.WriteAllText(@"D:\a\aaaa.json", gi);
            // Console.WriteLine(gi);
        }
    }
}