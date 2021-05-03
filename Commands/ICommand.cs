using System.Threading.Tasks;

namespace HeliumBot.Commands
{
    public interface ICommand
    {
        //demos:
        // /genshin 114514 -avatar
        // /genshin abyss 114514
        public Task<string[]> Execute(string[] command);
    }
}