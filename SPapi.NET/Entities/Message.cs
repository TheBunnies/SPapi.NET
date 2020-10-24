using System;
using Newtonsoft.Json;

namespace SPapi.NET.Entities
{
    public class Message
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("time")]
        internal string RawTime { get; set; }

        public DateTime Time
        {
            get => UnixTimeConverter.UnixTimeStampToDateTime(Convert.ToDouble(RawTime));
        }

        [JsonProperty("message")]
        public string Content { get; set; }
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
    }
}
