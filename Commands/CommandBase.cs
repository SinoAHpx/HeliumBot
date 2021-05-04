using System.Collections.Generic;
using System.Threading.Tasks;
using HeliumBot.Data.Command;

namespace HeliumBot.Commands
{
    public abstract class CommandBase
    {
        public abstract Task<string[]> Execute(CommandUsage command);
        public abstract string GetCommandPrefix();
    }
}