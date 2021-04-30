using Mirai_CSharp.Models;

namespace HeliumBot.Utils.Extensions
{
    public static class EventArgsExtension
    {
        public static string GetMessage(this IGroupMessageEventArgs ex, int index = 1)
        {
            return ex.Chain[index].ToString();
        }
    }
}