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
            switch (e.GetMessage())
            {
                case "/Help":
                    await session.SendPlainText(e, "欢迎使用HeliumBot 1.0", "===================",
                        "/Help : 查看帮助",
                        "/Genshin <uid> : 获取指定原神UID的公开信息",
                        "/Genshin <uid> -avatar : 获取指定原神UID的公开信息，并查看获得的角色",
                        "/GenshinAbyss <uid> -avatar : 获取指定原神UID的深渊战绩",
                        "/Money : 给作者打钱",
                        "/About : 查看Bot的关于信息");
                    break;
            }
            
            return false;
        }
    }
}