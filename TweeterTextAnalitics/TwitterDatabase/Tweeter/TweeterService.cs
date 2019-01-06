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
            if(Auth.Credentials == null)
            {
                Auth.SetUserCredentials(settings.CONSUMER_KEY, settings.CONSUMER_SECRET, settings.ACCESS_TOKEN, settings.ACCESS_TOKEN_SECRET);
            }
            _currentUser = User.GetAuthenticatedUser();
        }

        public async Task SearchAll(string searchQuery, string keyword, TweeterServiceData tweeterServiceData, long? maxIdStored = null)
        {
            List<TweeterResult> result = new List<TweeterResult>();
            var searchParameter = new SearchTweetsParameters(searchQuery)
            {
                TweetSearchType = TweetSearchType.All,
                MaximumNumberOfResults = 100
            };
            if(maxIdStored.HasValue)
                searchParameter.MaxId = maxIdStored.Value;
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
                if(o.Language == Language.Spanish || o.Language == Language.Italian || o.Language == Language.English ||
                    o.Language == Language.French || o.Language == Language.German || o.Language == Language.Portuguese)
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

                    if (o.RetweetCount > 0)
                    {
                        var retweets = await o.GetRetweetsAsync();
                        if(retweets != null)
                        {
                            foreach (var r in retweets)
                            {
                                if (r.Language == Language.Spanish || r.Language == Language.Italian || r.Language == Language.English ||
                                r.Language == Language.French || r.Language == Language.German || r.Language == Language.Portuguese)
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
                        }
                        
                    }
                    mapped.Add(tweet);
                }
                
            }
            return mapped;
        }
    }
}
