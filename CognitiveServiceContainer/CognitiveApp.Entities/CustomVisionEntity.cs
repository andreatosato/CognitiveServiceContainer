using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CognitiveApp.Entities
{
    public class CustomVisionRequest
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public partial class CustomVisionResponse
    {
        [JsonProperty("id")]
        public Guid? Id { get; set; }
        [JsonProperty("project")]
        public Guid? Project { get; set; }
        [JsonProperty("iteration")]
        public Guid? Iteration { get; set; }
        [JsonProperty("created")]
        public DateTimeOffset Created { get; set; }
        [JsonProperty("predictions")]
        public List<Prediction> Predictions { get; set; }
    }

    public partial class Prediction
    {
        [JsonProperty("boundingBox")]
        public string BoundingBox { get; set; }
        [JsonProperty("probability")]
        public double Probability { get; set; }
        [JsonProperty("tagid")]
        public string TagId { get; set; }
        [JsonProperty("tagname")]
        public string TagName { get; set; }
    }

}
