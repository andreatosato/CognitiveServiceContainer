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

        public async Task SearchAll(string searchQuery, string keyword, TweeterServiceData tweeterServiceData)
        {
            List<TweeterResult> result = new List<TweeterResult>();
            var searchParameter = new SearchTweetsParameters(searchQuery) { TweetSearchType = TweetSearchType.All };
            IEnumerable<ITweet> tweets = Search.SearchTweets(searchParameter);
            try
            {
                if (tweets != null)
                {
                    result.AddRange(await ManualMap(tweets, keyword));
                    await tweeterServiceData.SaveCollection(result);
                    result.Clear();
                    while (tweets != null && tweets.Any())
                    {
                        long maxId = tweets.Min(x => x.Id);
                        searchParameter.MaxId = maxId;
                        tweets = Search.SearchTweets(searchParameter);
                        if (tweets != null)
                            result.AddRange(await ManualMap(tweets, keyword));
                        Console.WriteLine($"{searchQuery} - element loaded: {result.Count}");
                        await tweeterServiceData.SaveCollection(result);
                        result.Clear();
                    }
                }
            }
            catch (Exception ex)
            {


            }
            
        }

        private async Task<List<TweeterResult>> ManualMap(IEnumerable<ITweet> old, string keyword)
        {
            List<TweeterResult> mapped = new List<TweeterResult>();
            foreach (var o in old.Where(x => !x.IsRetweet))
            {
                var tweet = new TweeterResult
                {
                    RetweetCount = o.RetweetCount,
                    FavoriteCount = o.FavoriteCount,
                    Id = o.Id,
                    //UserMentions = o.Entities.UserMentions.Select(x => new UserMention { Id = x.Id.Value, IdStr = x.IdStr, Name = x.Name, ScreenName = x.ScreenName }).ToList(),                        
                    QuoteCount = o.QuoteCount,
                    CreatedAt = o.CreatedAt,
                    Text = o.Text,
                    FullText = o.FullText,
                    Language = o.Language,
                    SearchKey = keyword
                };

                if(o.RetweetCount > 0)
                {
                    foreach (var r in await o.GetRetweetsAsync())
                    {
                        tweet.Retweets.Add(new Retweeted()
                        {
                            CreatedAt = r.CreatedAt,
                            Id = r.Id,
                            RetweetedTweetId = r.RetweetedTweet.Id,
                            Text = r.Text
                        });
                    }
                }
                mapped.Add(tweet);
            }
            return mapped;
        }
    }
}
