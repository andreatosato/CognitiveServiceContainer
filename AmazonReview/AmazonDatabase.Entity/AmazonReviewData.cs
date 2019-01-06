using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AmazonDatabase.Entity
{
    public partial class AmazonReviewData
    {
        [JsonProperty("reviewerID")]
        public string ReviewerId { get; set; }

        [JsonProperty("asin")]
        public string Asin { get; set; }

        [JsonProperty("reviewerName")]
        public string ReviewerName { get; set; }

        [JsonProperty("helpful")]
        public List<long> Helpful { get; set; }

        [JsonProperty("reviewText")]
        public string ReviewText { get; set; }

        [JsonProperty("overall")]
        public long Overall { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("unixReviewTime")]
        public long UnixReviewTime { get; set; }

        [JsonProperty("reviewTime")]
        public string ReviewTime { get; set; }

        public DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
