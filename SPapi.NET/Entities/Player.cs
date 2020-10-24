using Newtonsoft.Json;

namespace SPapi.NET.Entities
{
    public class Player
    {
        internal Player() {}
        
        [JsonProperty("nick")]
        public string Nickname { get; set; }
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }
}
