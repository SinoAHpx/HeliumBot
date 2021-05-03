using System;
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
                $"{genshinAbyss.StartTime.TimestampToDateTime():d}-{genshinAbyss.StartTime.TimestampToDateTime():d}",
                $"共战斗了{genshinAbyss.TotalBattleTimes}次，其中胜利{genshinAbyss.TotalWinTimes}次",
                $"最深抵达{genshinAbyss.MaxFloor}共获得{genshinAbyss.TotalStar}颗渊星",
                $""
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
    }
}