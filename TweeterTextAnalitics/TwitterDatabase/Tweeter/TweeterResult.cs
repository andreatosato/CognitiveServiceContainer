using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tweetinvi.Models;
using Tweetinvi.Models.Entities;

namespace TwitterDatabase.Tweeter
{
    public class TweeterResult
    {
        public TweeterResult()
        {
            Retweets = new HashSet<Retweeted>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public int RetweetCount { get; set; }
        public int FavoriteCount { get; set; }
        public int? QuoteCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public string FullText { get; set; }
        public Language Language { get; set; }
        public string SearchKey { get; set; }
        public ICollection<Retweeted> Retweets { get; set; }


        public string LanguageISO6391 { get; set; }
        public double Sentiment { get; set; }
    }

    public class Retweeted
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public long RetweetedTweetId { get; set; }
        [ForeignKey("RetweetedTweetId")]
        public TweeterResult OriginalTweet { get; set; }
    }
}
