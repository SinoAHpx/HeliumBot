using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeliumBot.Commands;
using HeliumBot.Commands.Genshin;
using HeliumBot.Configs;
using HeliumBot.Data.Command;
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
        public IEnumerable<CommandBase> CommandBases { get; set; }
        
        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            if (e.GetMessage().IsCommand())
            {
                var executor = new CommandExecutor
                {
                    Commands = CommandBases
                };

                var inputCommand = e.GetMessage().ParseCommand();

                try
                {
                    var text = await executor.ExecuteCommand(new CommandUsage
                    {
                        Prefix = inputCommand[0],
                        MainParam = inputCommand[1],
                        Options = inputCommand.HasOptions() ? inputCommand.CopyStringArray(2) : null
                    });
                
                    if(text != null) await session.SendPlainText(e, text);
                }
                catch (Exception exception)
                {
                    await session.SendPlainText(e, "请求失败:", exception.Message);
                }
            }

            return false;
        }
    }
}