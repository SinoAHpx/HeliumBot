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
    public class GenshinIndexCommand : CommandBase
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

            var genshinIndex = await genshinQuery.QueryGenshinIndex();

            return new[]
            {
                $"UID{genshinQuery.Uid}的账号",
                $"共有{genshinIndex.Stats.Avatars}个角色{ParseAvatars(genshinIndex, command)}",
                $"活跃了{genshinIndex.Stats.ActiveDays}天",
                $"取得了{genshinIndex.Stats.Achievements}个成就",
                $"找到了{genshinIndex.Stats.AnemoCulus}个风神瞳和{genshinIndex.Stats.GeoCulus}个岩神瞳",
                $"解锁了{genshinIndex.Stats.Waypoints}个传送点和{genshinIndex.Stats.Domains}个秘境",
                $"开启了{genshinIndex.Stats.LuxuriousChests}个华丽的宝箱",
                $"{genshinIndex.Stats.PreciousChests}个珍贵的宝箱",
                $"{genshinIndex.Stats.ExquisiteChests}个精致的宝箱",
                $"{genshinIndex.Stats.CommonChests}个普通的宝箱",
                $"凹到了深渊{genshinIndex.Stats.SpiralAbyss}"
            };
        }

        public override string GetCommandPrefix()
        {
            return "/Genshin";
        }

        private string ParseAvatars(GenshinIndex genshinIndex, CommandUsage command)
        {
            var avatarsText = genshinIndex.Avatars.Select(avatar =>
                $"{avatar.Name} {avatar.Level}级 好感{avatar.RelationShip}级 {avatar.Constellations}命 ");

            if (!command.HasOption("-Avatar"))
                return string.Empty;

            return $":\n{string.Join(";", avatarsText).Trim()}";
        }
    }
}