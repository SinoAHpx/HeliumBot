using System;
using System.Threading.Tasks;
using HeliumBot.Implements;
using HeliumBot.Plugins.Group;
using HeliumBot.Utils.Extensions;
using Mirai_CSharp;
using Mirai_CSharp.Models;

namespace HeliumBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sessionOptions = new MiraiHttpSessionOptions("127.0.0.1",2334,"b5d83202bb874250984b20e6b0a4121c");
            var session = new MiraiHttpSession();
            
            session.AddPlugin(new HelloWorldPlugin());
            session.AddPlugin(new GenshinPlugin());
            
            await session.ConnectAsync(sessionOptions, 2672886221);

            Console.WriteLine("Bot launched!");
            
            while (true)
            {
                if (Console.ReadLine() == "exit")
                {
                    return;
                }
            }
        }
    }
}