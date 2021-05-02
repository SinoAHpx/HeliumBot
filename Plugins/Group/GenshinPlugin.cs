using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeliumBot.Configs;
using HeliumBot.Data.Config;
using HeliumBot.Implements;
using HeliumBot.Utils.Extensions;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;

namespace HeliumBot.Plugins.Group
{
    public class GenshinPlugin : IGroupMessage
    {
        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            var gc = Configurator<GenshinConfig>.ReadConfig();
            var gq = new GenshinQuery
            {
                AppVersion = gc.AppVersion,
                Salt = gc.Salt,
                Cookie = gc.Cookie
            };

            if (e.GetMessage().Contains("/genshin"))
            {
                if (e.GetMessage().ParseCommand().Length < 2)
                {
                    await session.SendPlainText(e, "请输入Uid！");
                }
                else if (e.GetMessage().ParseCommand()[1] == "abyss")
                {
                    if (e.GetMessage().ParseCommand().Length < 3)
                    {
                        await session.SendPlainText(e, "请输入Uid！");
                    }
                    else
                    {
                        try
                        {
                            gq.Uid = e.GetMessage().ParseCommand()[2];

                            //-previous
                            var schedule = e.GetMessage().ParseCommand().Any(x => x.Contains("-previous")) ? 1 : 2;
                            var ga = await gq.QueryGenshinAbyss(schedule);

                            await session.SendPlainText(e,
                                $"第{ga.ScheduleId}期深渊战绩:",//({DateTime.Parse(ga.StartTime).Date:d}-{DateTime.Parse(ga.EndTime).Date:d})
                                $"总共战斗{ga.TotalBattleTimes}，其中成功了{ga.TotalWinTimes}次",
                                $"最深抵达{ga.MaxFloor}，共获取{ga.TotalStar}颗渊星");
                        }
                        catch (Exception exception)
                        {
                            await session.SendPlainText(e, "请求失败: ", exception.Message);
                        }
                    }
                }
                else
                {
                    try
                    {
                        gq.Uid = e.GetMessage().ParseCommand()[1];
                        var gi = await gq.QueryGenshinIndex();

                        var avatarTexts = new List<string>();
                        foreach (var avatar in gi.Avatars)
                        {
                            avatarTexts.Add(
                                $"{avatar.Name} {avatar.Level}级 好感{avatar.RelationShip}级 {avatar.Constellations}命");
                        }

                        var avatarText = string.Empty;
                        if (e.GetMessage().ParseCommand().Length >= 3)
                        {
                            if (e.GetMessage().ParseCommand().Any(x => x == "-avatar"))
                            {
                                avatarText = $"\n{string.Join(";", avatarTexts)}";
                            }
                        }

                        await session.SendPlainText(e,
                            $"此账号,",
                            $"共有{gi.Stats.Avatars}个角色{avatarText}",
                            $"活跃了{gi.Stats.ActiveDays}天",
                            $"取得了{gi.Stats.Achievements}个成就",
                            $"找到了{gi.Stats.AnemoCulus}个风神瞳和{gi.Stats.GeoCulus}个岩神瞳",
                            $"解锁了{gi.Stats.Waypoints}个传送点和{gi.Stats.Domains}个秘境",
                            $"开启了{gi.Stats.LuxuriousChests}个华丽的宝箱",
                            $"{gi.Stats.PreciousChests}个珍贵的宝箱",
                            $"{gi.Stats.ExquisiteChests}个精致的宝箱",
                            $"{gi.Stats.CommonChests}个普通的宝箱",
                            $"凹到了深渊{gi.Stats.SpiralAbyss}");
                    }
                    catch (Exception exception)
                    {
                        await session.SendPlainText(e, "请求失败: ", exception.Message);
                    }
                }
            }

            return false;
        }
    }
}