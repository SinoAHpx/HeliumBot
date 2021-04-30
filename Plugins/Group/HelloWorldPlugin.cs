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
                    case "/help":
                        await session.SendPlainText(e, "欢迎使用HeliumBot 1.0", "===================",
                            "/help : 查看帮助",
                            "/genshin <uid> : 获取指定原神UID的公开信息",
                            "/money : 给作者打钱",
                            "/about : 查看Bot的关于信息和");
                        break;
                }
            }
            
            return false;
        }
    }
}