using Newtonsoft.Json;

namespace ServiceBusBootstrapper.DataAccess
{
    public class Subscriber
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}