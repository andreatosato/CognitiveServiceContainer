using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveApp.Cognitive
{
    public class CognitiveData
    {
        [JsonProperty("documents")]
        public List<Document> Documents { get; set; }
    }
    
    public partial class Document
    {
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }

}
