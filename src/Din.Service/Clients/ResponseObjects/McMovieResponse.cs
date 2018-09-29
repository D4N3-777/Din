﻿using Newtonsoft.Json;

namespace Din.Service.Clients.ResponseObjects
{
    public class McMovieResponse
    {
        [JsonProperty("tmdbid")] public int Id { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("downloaded")] public bool Downloaded { get; set; }
        [JsonProperty("hasFile")] public bool HasFile { get; set; }
    }
}
