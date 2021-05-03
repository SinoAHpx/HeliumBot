using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeliumBot.Data.Command;

namespace HeliumBot.Commands
{
    public class CommandExecutor
    {
        public IEnumerable<CommandBase> Commands { get; set; }
        
        public async Task<string[]> ExecuteCommand(CommandUsage command)
        {
            foreach (var commandBase in Commands)
            {
                if (commandBase.GetCommandPrefix() == command.Prefix)
                {
                    return await commandBase.Execute(command);
                }
            }

            return null;
        }
    }
}