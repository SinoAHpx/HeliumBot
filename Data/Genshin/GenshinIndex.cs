using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeliumBot.Data.Genshin
{
    public class GenshinIndex
    {
        [JsonProperty("avatars")]
        public List<Avatar> Avatars { get; set; }
        
        [JsonProperty("city_explorations")]
        public List<CityExploration> CityExplorations { get; set; }
        
        [JsonProperty("world_explorations")]
        public List<WorldExploration> WorldExplorations { get; set; }
        
        [JsonProperty("stats")]
        public Statistics Stats { get; set; }

        //array item
        public class CityExploration
        {
            [JsonProperty("level")]
            public int Level { get; set; }

            [JsonProperty("exploration_percentage")]
            public int ExplorationRate { get; set; }
            
            [JsonProperty("icon")]
            public string Icon { get; set; }
            
            [JsonProperty("name")]
            public string Name { get; set; }
        }
        
        //array item
        public class WorldExploration
        {
            [JsonProperty("level")]
            public int Level { get; set; }
            
            [JsonProperty("exploration_percentage")]
            public int ExplorationRate { get; set; }
            
            [JsonProperty("name")]
            public string Name { get; set; }
            
            [JsonProperty("icon")]
            public string Icon { get; set; }
            
            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public class Statistics
        {
            [JsonProperty("active_day_number")]
            public int ActiveDays { get; set; }
            
            [JsonProperty("achievement_number")]
            public int Achievements { get; set; }
            
            [JsonProperty("anemoculus_number")]
            public int AnemoCulus { get; set; }
            
            [JsonProperty("geoculus_number")]
            public int GeoCulus { get; set; }
            
            [JsonProperty("avatar_number")]
            public int Avatars { get; set; }
            
            [JsonProperty("way_point_number")]
            public int Waypoints { get; set; }
            
            [JsonProperty("domain_number")]
            public int Domains { get; set; }
            
            [JsonProperty("precious_chest_number")]
            public int PreciousChests { get; set; }
            
            [JsonProperty("luxurious_chest_number")]
            public int LuxuriousChests { get; set; }
            
            [JsonProperty("exquisite_chest_number")]
            public int ExquisiteChests { get; set; }
            
            [JsonProperty("common_chest_number")]
            public int CommonChests { get; set; }
            
            [JsonProperty("spiral_abyss")]
            public string SpiralAbyss { get; set; }
        }
        
        //array item
        public class Avatar
        {
            [JsonProperty("name")]
            public string Name { get; set; }
            
            [JsonProperty("element")]
            public string Element { get; set; }
            
            [JsonProperty("image")]
            public string Image { get; set; }
            
            [JsonProperty("fetter")]
            public int RelationShip { get; set; }
            
            [JsonProperty("level")]
            public int Level { get; set; }
            
            [JsonProperty("rarity")]
            public int Rarity { get; set; }
            
            [JsonProperty("actived_constellation_num")]
            public int Constellations { get; set; }
        }
    }
}