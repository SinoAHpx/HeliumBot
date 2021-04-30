using System;
using System.Threading.Tasks;
using HeliumBot.Utils.Extensions;
using Mirai_CSharp;
using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;

namespace HeliumBot.Plugins.Group
{
    public class HelloWorldPlugin : IGroupMessage
    {
        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            if (e.Sender.Group.Id == 1042821169)
            {
                switch (e.GetMessage())
                {
                    case "/money":
                        await session.SendPlainText(e, "我是破晓，你能给我打钱吗？");
                        break;
                    case "/genshin":
                        await session.SendPlainText(e, "GENSIN IMPACT");
                        break;
                }
            }
            
            return false;
        }
    }
}