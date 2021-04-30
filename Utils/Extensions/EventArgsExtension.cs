using Mirai_CSharp.Models;

namespace HeliumBot.Utils.Extensions
{
    public static class EventArgsExtension
    {
        public static string GetMessage(this IGroupMessageEventArgs ex)
        {
            return ex.Chain[1].ToString();
        }

        /// <summary>
        /// child0: command with /
        /// child1: param 1
        /// child2: param 2...
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string[] ParseCommand(this string ex)
        {
            var sp = ex.Split(" ");

            return sp;
        }
    }
}