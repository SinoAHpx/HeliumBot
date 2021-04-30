using System;
using System.Threading.Tasks;
using HeliumBot.Plugins.Group;
using Mirai_CSharp;
using Mirai_CSharp.Models;

namespace HeliumBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sessionOptions = new MiraiHttpSessionOptions("127.0.0.1",29331,"5a72244029a548e88c262ae31750eb52");
            var session = new MiraiHttpSession();
            
            session.AddPlugin(new HelloWorldPlugin());

            await session.ConnectAsync(sessionOptions, 2672886221);

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