using System.Threading.Tasks;
using Mirai_CSharp;
using Mirai_CSharp.Extensions;
using Mirai_CSharp.Models;

namespace HeliumBot.Utils.Extensions
{
    public static class SessionExtension
    {
        public static async Task SendPlainText(this MiraiHttpSession session, IGroupMessageEventArgs e, string text)
        {
            var builder = new MessageBuilder();
            builder.AddAtMessage(e.Sender.Id);
            builder.AddPlainMessage(" ");
            builder.AddPlainMessage(text);
                    
            await session.SendGroupMessageAsync(e.Sender.Group.Id ,builder);
        }
        
        public static async Task SendPlainText(this MiraiHttpSession session, IGroupMessageEventArgs e, params string[] text)
        {
            var builder = new MessageBuilder();
            builder.AddAtMessage(e.Sender.Id);
            builder.AddPlainMessage(" ");
            builder.AddPlainMessage(string.Join('\n', text));
                    
            await session.SendGroupMessageAsync(e.Sender.Group.Id ,builder);
        }
    }
}