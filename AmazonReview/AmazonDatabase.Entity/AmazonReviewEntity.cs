using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AmazonDatabase.Entity
{
    public class AmazonReviewEntity
    {
        public AmazonReviewEntity()
        {

        }
        public AmazonReviewEntity(AmazonReviewData d)
        {
            ReviewerId = d.ReviewerId;
            Asin = d.Asin;
            ReviewerName = d.ReviewerName;
            ReviewText = d.ReviewText;
            ReviewTime = d.UnixTimeStampToDateTime(d.UnixReviewTime);
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string ReviewerId { get; set; }
        public string Asin { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewTime { get; set; }
        public double Sentiment { get; set; }
    }
}
