using System.Threading.Tasks;

namespace HeliumBot.Commands
{
    public abstract class CommandBase
    {
        //demos:
        // /genshin 114514 -avatar
        // /genshin abyss 114514
        public abstract Task<string[]> Execute(string[] command);

        public readonly string Usage;
    }
}