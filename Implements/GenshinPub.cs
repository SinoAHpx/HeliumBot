using System.Threading.Tasks;
using HeliumBot.Utils.Extensions;
using HtmlAgilityPack;

namespace HeliumBot.Implements
{
    public class GenshinPub
    {
        public static async Task<string> FetchJson()
        {
            var url = "http://genshin.pub";
            var document = new HtmlDocument();
            document.Load(await url.Request());

            var node = document.DocumentNode;

            return null;
        }
    }
}