using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using HeliumBot.Utils.Extensions;
using Mirai_CSharp;
using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;
using RestSharp;

namespace HeliumBot.Plugins.Group
{
    public class HelloWorldPlugin : IGroupMessage
    {
        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            if (e.GetMessage().IsCommand())
            {
                var assembly = Assembly.GetExecutingAssembly();
                var fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location);
                
                switch (e.GetMessage())
                {
                    case "/Help":
                        await session.SendPlainText(e, $"欢迎使用HeliumBot {fileVersion.FileVersion}", "===================",
                            "/Help : 查看帮助",
                            "/Genshin <uid> : 获取指定原神UID的公开信息",
                            "/Genshin <uid> -Avatar : 获取指定原神UID的公开信息，并查看获得的角色",
                            "/GenshinAbyss <uid> : 获取指定原神UID的深渊战绩",
                            "/GenshinAbyss <uid> -Detail : 获取指定原神UID的深渊战绩，并且查看每层每间详情",
                            "/Money : 给作者打钱",
                            "/About : 查看Bot的关于信息");
                        break;
                    case "/Money":
                        var img = await session.UploadPictureAsync(UploadTarget.Group,
                            await new HttpClient().GetStreamAsync("https://i.loli.net/2021/05/04/fXOm1xnrsBg45Zh.jpg"));

                        await session.SendGroupMessageAsync(e.Sender.Group.Id, img);
                        break;
                    case "/About":
                        await session.SendPlainText(e, $"HeliumBot {fileVersion.FileVersion}",
                            $"Maintain by AHpx(https://github.com/AHpxChina)",
                            $"Based on Mirai-CSharp(https://github.com/Executor-Cheng/Mirai-CSharp)",
                            $"Open source repository on https://github.com/AHpxChina/HeliumBot",
                            $"Acknowledgment:",
                            $"mirai(https://github.com/mamoe/mirai)",
                            $"mirai-api-http(https://github.com/mamoe/mirai-api-http)",
                            $"mirai-console(https://github.com/mamoe/mirai-console)");
                        break;
                }
            }

            return false;
        }
    }
}