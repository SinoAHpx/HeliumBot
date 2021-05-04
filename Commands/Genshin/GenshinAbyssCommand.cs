using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeliumBot.Configs;
using HeliumBot.Data.Command;
using HeliumBot.Data.Config;
using HeliumBot.Data.Genshin;
using HeliumBot.Implements;
using HeliumBot.Utils.Extensions;

namespace HeliumBot.Commands.Genshin
{
    public class GenshinAbyssCommand : CommandBase
    {
        public override async Task<string[]> Execute(CommandUsage command)
        {
            var genshinConfig = Configurator<GenshinConfig>.ReadConfig();
            var genshinQuery = new GenshinQuery
            {
                AppVersion = genshinConfig.AppVersion,
                Salt = genshinConfig.Salt,
                Cookie = genshinConfig.Cookie,
                Uid = command.MainParam
            };

            var genshinAbyss = await genshinQuery.QueryGenshinAbyss();

            return new[]
            {
                $"UID{genshinQuery.Uid}的账号的第{genshinAbyss.ScheduleId}期深渊战绩",
                $"{genshinAbyss.StartTime.TimestampToDateTime():d}-{genshinAbyss.EndTime.TimestampToDateTime():d}",
                $"共战斗了{genshinAbyss.TotalBattleTimes}次，其中胜利{genshinAbyss.TotalWinTimes}次",
                $"最深抵达{genshinAbyss.MaxFloor}，共获得{genshinAbyss.TotalStar}颗渊星",
                $"上场次数排行:{ParseAvatar(genshinAbyss.RevealRank.Take(4))}",
                $"击败数量排行:{ParseAvatar(genshinAbyss.DefeatRank.Take(4))}",
                $"最强一击:{ParseAvatar(genshinAbyss.DamageRank.Take(1), "")}",
                $"受到伤害排行:{ParseAvatar(genshinAbyss.TakeDamageRank.Take(4), "")}",
                $"元素战技释放数排行:{ParseAvatar(genshinAbyss.NormalSkillRank.Take(4))}",
                $"元素爆发释放数排行:{ParseAvatar(genshinAbyss.EnergySkillRank.Take(4))}"
                + $"{ParseOptions(genshinAbyss, command)}",
            };
        }

        public override string GetCommandPrefix()
        {
            return "/GenshinAbyss";
        }

        private string ParseOptions(GenshinAbyss genshinAbyss, CommandUsage commandUsage)
        {
            if (!commandUsage.Options.HasOptions())
            {
                return "";
            }

            var re = "";
            if (commandUsage.Options.Any("-Detail"))
            {
                foreach (var floor in genshinAbyss.Floors)
                {
                    re += $"第{floor.Index}，已获得{floor.Star}/{floor.MaxStar}颗渊星:\n";
                    foreach (var chamber in floor.Levels)
                    {
                        re += $"{floor.Index}-{chamber.Index}，已获得{chamber.Star}/{chamber.MaxStar}颗渊星:\n";
                        foreach (var battle in chamber.Battles)
                        {
                            var index = battle.Index == 1 ? "上半" : "下半";
                            re += $"{floor.Index}-{chamber.Index}{index}：{ParseAvatar(battle.Avatars, "级", true)}\n";
                        }
                    }
                }
            }

            return $"\n{re}";
        }

        private string ParseAvatar(IEnumerable<GenshinAbyss.Avatar> avatar, string suffix = "次", bool hasLevel =false)
        {
            var re = "";
            foreach (var a in avatar)
            {
                var name = (hasLevel ? a.Icon : a.AvatarIcon).UrlToAvatarName();
                re += $"{name}-{(hasLevel ? a.Level : a.Value)}{suffix}; ";
            }

            return re.Trim();
        }
    }
}