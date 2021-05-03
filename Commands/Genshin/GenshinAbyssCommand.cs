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
                $"最深抵达{genshinAbyss.MaxFloor}共获得{genshinAbyss.TotalStar}颗渊星",
                $"上场次数排行:{ParseAvatar(genshinAbyss.RevealRank.Take(4))}",
                $"击败数量排行:{ParseAvatar(genshinAbyss.DefeatRank.Take(4))}",
                $"元素战技释放数排行:{ParseAvatar(genshinAbyss.NormalSkillRank.Take(4))}",
                $"元素爆发释放数排行:{ParseAvatar(genshinAbyss.EnergySkillRank.Take(4))}",
            };
        }

        public override string GetCommandPrefix()
        {
            return "/GenshinAbyss";
        }

        private string ParseOptions(GenshinAbyss genshinAbyss, CommandUsage commandUsage)
        {
            return "";
        }

        private string ParseAvatar(IEnumerable<GenshinAbyss.Avatar> avatar)
        {
            var re = "";
            foreach (var a in avatar)
            {
                re += $"{a.AvatarIcon.UrlToAvatarName()} {a.Value}次; ";
            }

            return re.Trim();
        }
    }
}