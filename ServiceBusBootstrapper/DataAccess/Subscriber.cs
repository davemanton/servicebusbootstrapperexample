﻿using Newtonsoft.Json;

namespace ServiceBusBootstrapper
{
    public class Subscriber
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}