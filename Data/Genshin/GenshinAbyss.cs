using System.Collections.Generic;
using Newtonsoft.Json;

namespace HeliumBot.Data.Genshin
{
    public class GenshinAbyss
    {
        /// <summary>
        /// 第几期深渊
        /// </summary>
        [JsonProperty("schedule_id")]
        public string ScheduleId { get; set; }
        
        /// <summary>
        /// 当期深渊开始时间
        /// </summary>
        [JsonProperty("start_time")]
        public string StartTime { get; set; }
        
        /// <summary>
        /// 当期深渊结束时间
        /// </summary>
        [JsonProperty("end_time")]
        public string EndTime { get; set; }
        
        /// <summary>
        /// 总战斗此时
        /// </summary>
        [JsonProperty("total_battle_times")]
        public int TotalBattleTimes { get; set; }
        
        /// <summary>
        /// 总成功次数
        /// </summary>
        [JsonProperty("total_win_times")]
        public int TotalWinTimes { get; set; }
        
        /// <summary>
        /// 最深抵达
        /// </summary>
        [JsonProperty("max_floor")]
        public string MaxFloor { get; set; }
        
        /// <summary>
        /// 总共获取的渊星
        /// </summary>
        [JsonProperty("total_star")]
        public int TotalStar { get; set; }
        
        /// <summary>
        /// 已解锁深境螺旋
        /// </summary>
        [JsonProperty("is_unlock")]
        public bool IsUnlock { get; set; }
        
        /// <summary>
        /// 出场次数排名
        /// </summary>
        [JsonProperty("reveal_rank")]
        public IEnumerable<Avatar> RevealRank { get; set; }
        
        /// <summary>
        /// 击败次数排名
        /// </summary>
        [JsonProperty("defeat_rank")]
        public IEnumerable<Avatar> DefeatRank { get; set; }
        
        /// <summary>
        /// 最高伤害
        /// </summary>
        [JsonProperty("damage_rank")]
        public IEnumerable<Avatar> DamageRank { get; set; }
        
        /// <summary>
        /// 收到伤害排名
        /// </summary>
        [JsonProperty("take_damage_rank")]
        public IEnumerable<Avatar> TakeDamageRank { get; set; }
        
        /// <summary>
        /// 元素战技释放次数
        /// </summary>
        [JsonProperty("normal_skill_rank")]
        public IEnumerable<Avatar> NormalSkillRank { get; set; }
        
        /// <summary>
        /// 元素爆发释放次数
        /// </summary>
        [JsonProperty("energy_skill_rank")]
        public IEnumerable<Avatar> EnergySkillRank { get; set; }
        
        /// <summary>
        /// 每层详情
        /// </summary>
        [JsonProperty("floors")]
        public IEnumerable<Floor> Floors { get; set; }
        
        public class Avatar
        {
            /// <summary>
            /// 角色图标url
            /// </summary>
            [JsonProperty("avatar_icon")]
            public string AvatarIcon { get; set; }

            [JsonProperty("icon")] 
            public string Icon { get; set; }
            
            /// <summary>
            /// 数值
            /// </summary>
            [JsonProperty("value")]
            public int Value { get; set; }
            
            /// <summary>
            /// 星级
            /// </summary>
            [JsonProperty("rarity")]
            public int Rarity { get; set; }

            /// <summary>
            /// 等级
            /// </summary>
            [JsonProperty("level")]
            public int Level { get; set; }
        }
        
        public class Floor
        {
            /// <summary>
            /// 第几层
            /// </summary>
            [JsonProperty("index")]
            public int Index { get; set; }
            
            /// <summary>
            /// 是否解锁
            /// </summary>
            [JsonProperty("is_unlock")]
            public string IsUnlock { get; set; }
            
            /// <summary>
            /// 未知
            /// </summary>
            [JsonProperty("settle_time")]
            public string SettleTime { get; set; }
            
            /// <summary>
            /// 图标，为空
            /// </summary>
            [JsonProperty("icon")]
            public string Icon { get; set; }
            
            /// <summary>
            /// 获取渊星
            /// </summary>
            [JsonProperty("star")]
            public int Star { get; set; }
            
            /// <summary>
            /// 总共渊星
            /// </summary>
            [JsonProperty("max_star")]
            public int MaxStar { get; set; }
            
            /// <summary>
            /// 每间详情
            /// </summary>
            [JsonProperty("levels")]
            public IEnumerable<Chamber> Levels { get; set; }
        }
        
        public class Chamber
        {
            /// <summary>
            /// 第几间
            /// </summary>
            [JsonProperty("index")]
            public int Index { get; set; }
            
            /// <summary>
            /// 此间获取的渊星
            /// </summary>
            [JsonProperty("star")]
            public int Star { get; set; }
            
            /// <summary>
            /// 此间的渊星
            /// </summary>
            [JsonProperty("max_star")]
            public int MaxStar { get; set; }
            
            /// <summary>
            /// 战斗详情
            /// </summary>
            [JsonProperty("battles")]
            public IEnumerable<Battle> Battles { get; set; }
        }
        
        public class Battle
        {
            /// <summary>
            /// 1是上半，2是下半
            /// </summary>
            [JsonProperty("index")]
            public int Index { get; set; }
            
            /// <summary>
            /// 时间
            /// </summary>
            [JsonProperty("timestamp")]
            public string Timestamp { get; set; }
            
            /// <summary>
            /// 使用的角色，在此level有值，value无值
            /// </summary>
            [JsonProperty("avatars")]
            public IEnumerable<Avatar> Avatars { get; set; }
        }
    }
}