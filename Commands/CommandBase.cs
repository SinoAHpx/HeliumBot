using System.Threading.Tasks;
using HeliumBot.Data.Command;

namespace HeliumBot.Commands
{
    public abstract class CommandBase
    {
        //demos:
        // /genshin 114514 -avatar
        // /genshin abyss 114514
        public abstract Task<string[]> Execute(CommandUsage command);
        public abstract string GetCommandPrefix();
    }
}