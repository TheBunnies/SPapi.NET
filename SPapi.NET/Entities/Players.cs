using System.Collections.Generic;
using Newtonsoft.Json;

namespace SPapi.NET.Entities
{
    public class Players
    {
        internal Players() {}
        [JsonProperty("players")]
        public List<Player> ServerPlayers { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("max")]
        public int Max { get; set; }
    }
}
