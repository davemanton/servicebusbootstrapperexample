using System.Collections.Generic;
using Newtonsoft.Json;

namespace ServiceBusBootstrapper
{
    public class Topic
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("frequencyIntervalSeconds")]
        public int FrequencyIntervalSeconds { get; set; }
        [JsonProperty("subscribers")]
        public ICollection<Subscriber> Subscribers { get; set; }
        
    }
}