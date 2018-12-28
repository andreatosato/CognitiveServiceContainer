using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveApp.Entities
{
    public class Error
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
