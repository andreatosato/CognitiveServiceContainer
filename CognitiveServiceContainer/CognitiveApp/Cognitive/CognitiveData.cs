using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveApp.Cognitive
{
    public class SentimentRequest
    {
        [JsonProperty("documents")]
        public List<DocumentRequest> Documents { get; set; }
    }
    
    public class DocumentRequest
    {
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }


    public class SentimentResponse
    {
        [JsonProperty("documents")]
        public List<DocumentResponse> Documents { get; set; }
        [JsonProperty("errors")]
        public List<object> Errors { get; set; }
    }

    public class DocumentResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("score")]
        public double Score { get; set; }
    }


}
