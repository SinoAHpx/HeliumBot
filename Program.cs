using System;
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
            BotConfig botConfig = null;
            Bot bot = null;

            Console.WriteLine("Use /help for command help");
            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "/exit":
                        if (bot != null) await bot.Terminate();
                        return;
                    case "/config generate":
                        BotConfigurator.GenerateConfig();
                        Logger.Log("Empty bot config generated");
                        break;
                    case "/config read":
                        botConfig = BotConfigurator.ReadConfig();
                        Logger.Log("Tried to read bot config");
                        break;
                    case "/config write":
                        BotConfigurator.WriteConfig(botConfig);
                        Logger.Log("Bot config wroted");
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
                        Console.WriteLine("/launch : launch bot by bot config");
                        Console.WriteLine("terminate : stop bot and dispose resources but not exit plugin");
                        Console.WriteLine("/config generated : generate a empty bot config json");
                        Console.WriteLine("/config read : read bot config json");
                        Console.WriteLine("/config write : write bot config json");
                        break;
                }
            }
        }
    }
}