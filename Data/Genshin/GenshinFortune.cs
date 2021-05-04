using System.Collections.Generic;

namespace HeliumBot.Data.Genshin
{
    public class GenshinFortune
    {
        public bool IsGood { get; set; }
        public IEnumerable<FortuneItem> Items { get; set; }
        
        public class FortuneItem
        {
            public string Title { get; set; }
            public string Description { get; set; }
        }
    }
}