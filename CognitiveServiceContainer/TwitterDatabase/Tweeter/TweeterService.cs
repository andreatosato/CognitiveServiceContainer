using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace TwitterDatabase.Tweeter
{
    public class TweeterService
    {
        private IAuthenticatedUser _currentUser;
        public TweeterService(TwitterSettings settings)
        {
            Auth.SetUserCredentials(settings.CONSUMER_KEY, settings.CONSUMER_SECRET, settings.ACCESS_TOKEN, settings.ACCESS_TOKEN_SECRET);
            _currentUser = User.GetAuthenticatedUser();
        }

        public List<TweeterResult> SearchAll(string searchQuery, string keyword)
        {
            List<TweeterResult> result = new List<TweeterResult>();
            var searchParameter = new SearchTweetsParameters(searchQuery) { TweetSearchType = TweetSearchType.OriginalTweetsOnly };
            IEnumerable<ITweet> tweets = Search.SearchTweets(searchParameter);
            try
            {
                if (tweets != null)
                {
                    result.AddRange(ManualMap(tweets, keyword));
                    while (tweets != null && tweets.Any())
                    {
                        long maxId = tweets.Min(x => x.Id);
                        searchParameter.MaxId = maxId;
                        tweets = Search.SearchTweets(searchParameter);
                        if(tweets != null)
                            result.AddRange(ManualMap(tweets, keyword));
                        Console.WriteLine($"{searchQuery} - element loaded: {result.Count}");
                    }
                }
            }
            catch (Exception ex)
            {


            }
            
            return result;
        }

        private List<TweeterResult> ManualMap(IEnumerable<ITweet> old, string keyword)
        {
            List<TweeterResult> mapped = new List<TweeterResult>();
            foreach (var o in old)
            {
                if (!string.IsNullOrEmpty(o.InReplyToScreenName) ||
                    !string.IsNullOrEmpty(o.InReplyToUserIdStr) ||
                    !string.IsNullOrEmpty(o.InReplyToStatusIdStr))
                    mapped.Add(new TweeterResult
                    {
                        RetweetCount = o.RetweetCount,
                        FavoriteCount = o.FavoriteCount,
                        Favorited = o.Favorited,
                        Retweeted = o.Retweeted,
                        Id = o.Id,
                        TweetLocalCreationDate = o.TweetLocalCreationDate,
                        Hashtags = o.Entities.Hashtags.Select(x => new HashtagEntity { Text = x.Text }).ToList(),
                        UserMentions = o.Entities.UserMentions.Select(x => new UserMention { Id = x.Id, IdStr = x.IdStr, Name = x.Name, ScreenName = x.ScreenName }).ToList(),
                        IsRetweet = o.IsRetweet,
                        RetweetedTweetId = o.RetweetedTweet?.Id,
                        QuoteCount = o.QuoteCount,
                        QuotedTweetId = o.QuotedTweet?.Id,
                        CreatedAt = o.CreatedAt,
                        Text = o.Text,
                        Prefix = o.Prefix,
                        Suffix = o.Suffix,
                        FullText = o.FullText,
                        Language = o.Language,
                        Keyword = keyword
                    });
            }
            return mapped;
        }
    }
}
