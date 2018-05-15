﻿using Newtonsoft.Json;

namespace Din.ExternalModels.Content
{
    public class UnsplashContainer
    {
        [JsonProperty("urls")]
        public Urls Urls { get; set; }
    }

    public class Urls
    {
        [JsonProperty("full")]
        public string Full { get; set; }
        [JsonProperty("regular")]
        public string Regular { get; set; }
        [JsonProperty("custom")]
        public string Custom { get; set; }
    }
}