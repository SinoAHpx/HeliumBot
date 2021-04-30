using System.Threading.Tasks;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;

namespace HeliumBot.Plugins.Group
{
    public class HelloWorldPlugin : IGroupMessage
    {
        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            if (e.Sender.Group.Id == 110838222)
            {
                if (e.Chain[1].ToString() == "-hello")
                {
                    await session.ExecuteCommandAsync("hello");
                }
                
            }
            
            return false;
        }
    }
}