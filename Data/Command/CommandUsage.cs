namespace HeliumBot.Data.Command
{
    public class CommandUsage
    {
        public string Prefix { get; set; }
        public string MainParam { get; set; }
        public string[] Options { get; set; }
    }
}