using System.Linq;
using HeliumBot.Data.Command;

namespace HeliumBot.Utils.Extensions
{
    public static class CommandExtension
    {
        public static bool HasOptions(this string[] ex)
        {
            return ex.Length > 0;
        }

        public static bool HasOption(this CommandUsage ex, string optionName)
        {
            return ex.Options.HasOptions() && ex.Options.Any(s => s == optionName);
        }
    }
}