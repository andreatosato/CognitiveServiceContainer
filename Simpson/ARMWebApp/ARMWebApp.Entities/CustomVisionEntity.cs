using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ARMWebApp.Entities
{
    public class CustomVisionRequest
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class CustomVisionStreamRequest
    {
        [JsonProperty("imageData")]
        public byte[] ImageData { get; set; }
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
        public List<Prediction> Predictions { get; set; } = new List<Prediction>();
    }

    public partial class Prediction
    {
        [JsonProperty("boundingBox")]
        public BoundingBox BoundingBox { get; set; }
        [JsonProperty("probability")]
        public double Probability { get; set; }
        [JsonProperty("tagid")]
        public string TagId { get; set; }
        [JsonProperty("tagname")]
        public string TagName { get; set; }
    }

    public class BoundingBox
    {
        [JsonProperty("height")]
        public double Height { get; set; }
        [JsonProperty("left")]
        public double Left { get; set; }
        [JsonProperty("top")]
        public double Top { get; set; }
        [JsonProperty("width")]
        public double Width { get; set; }
    }
}
