using System.Collections.Generic;
using Newtonsoft.Json;

namespace SPapi.NET.Entities
{
    public class Messages
    {
        [JsonProperty("messages")]
        public List<Message> PlayerMessages { get; set; }
    }
}
