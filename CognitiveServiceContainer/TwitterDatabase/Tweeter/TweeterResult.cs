using System;
using System.Collections.Generic;
using System.Text;
using Tweetinvi.Models;
using Tweetinvi.Models.Entities;

namespace TwitterDatabase.Tweeter
{
    public class TweeterResult
    {
        public long Id { get; set; }
        public int RetweetCount { get; set; }
        public bool Favorited { get; set; }
        public int FavoriteCount { get; set; }
        public bool Retweeted { get; set; }
        public DateTime TweetLocalCreationDate { get; set; }
        public List<HashtagEntity> Hashtags { get; set; }
        //public List<ITweet> Retweets { get; set; }
        public bool IsRetweet { get; set; }
        //ITweet RetweetedTweet { get; set; }
        public long? RetweetedTweetId { get; set; }
        public int? QuoteCount { get; set; }
        //ITweet QuotedTweet { get; set; }
        public long? QuotedTweetId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public string FullText { get; set; }
        public List<UserMention> UserMentions {get; set;}
        public Language Language { get; set; }
        public string Keyword { get; set; }
    }

    public class HashtagEntity
    {
        public string Text { get; set; }
    }

    public class UserMention
    {
        public long? Id { get; set; }
        public string IdStr { get; set; }
        public string ScreenName { get; set; }
        public string Name { get; set; }
    }
}
