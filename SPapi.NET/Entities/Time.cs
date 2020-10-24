using System.Text.Json.Serialization;
using Newtonsoft.Json;
using SPapi.NET.Enums;

namespace SPapi.NET.Entities
{
    public class Time
    {
        [JsonProperty("time")]
        [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
        public DayTime DayTime { get; set; }

        [JsonProperty("ticks")]
        public int Ticks { get; set; }
    }
}
