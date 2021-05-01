using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeliumBot.Utils;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin;

namespace HeliumBot.Configs
{
    public class Bot
    {
        public string SessionHost { get; set; }
        public int SessionPort { get; set; }
        public string SessionKey { get; set; }
        public long BotQQ { get; set; }
        public IEnumerable<IPlugin> Plugins { get; set; }

        private MiraiHttpSession _session;
        public async Task Launch()
        {
            Logger.Log("Session host:", SessionHost);
            Logger.Log("Session port:", SessionPort);
            Logger.Log("Session key:", SessionKey);
            var sessionOptions = new MiraiHttpSessionOptions(SessionHost, SessionPort, SessionKey);
            
            Logger.Log("Mirai session created");
            _session = new MiraiHttpSession();
            
            foreach (var plugin in Plugins)
            {
                _session.AddPlugin(plugin);
                Logger.Log("Plugin:", plugin.GetType().Name, "added");
            }

            try
            {
                await _session.ConnectAsync(sessionOptions, BotQQ);

                Logger.Log("Successful connected", BotQQ, "to Mirai session");
                Logger.Log("Bot launched!");
            }
            catch (Exception e)
            {
                Logger.Log("Connect to", BotQQ, "failed");
                Logger.Log("Reason:", e.Message);
            }
        }

        public async Task Terminate()
        {
            Logger.Log("Mirai session status:", _session.Connected);
            await _session.DisposeAsync();
            Logger.Log("Mirai session disposed");
        }
    }
}