using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveApp.Entities
{
    public class SentimentRequest
    {
        [JsonProperty("documents")]
        public List<DocumentSentimentRequest> Documents { get; set; }
    }

    public class DocumentSentimentRequest
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
        public List<DocumentSentimentResponse> Documents { get; set; }
        [JsonProperty("errors")]
        public List<Error> Errors { get; set; }
    }

    public class DocumentSentimentResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("score")]
        public double Score { get; set; }
    }
}
