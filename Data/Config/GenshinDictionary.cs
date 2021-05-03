using System.Collections.Generic;

namespace HeliumBot.Data.Config
{
    public class GenshinDictionary
    {
        public string Version { get; set; }
        public IEnumerable<Character> Characters { get; set; }

        public GenshinDictionary()
        {
            Characters = new[]
            {
                new Character(),
                new Character(),
            };
        }

        public class Character
        {
            public string Name { get; set; }
            public string EnglishName { get; set; }
            public string Rarity { get; set; }
        }
    }
}