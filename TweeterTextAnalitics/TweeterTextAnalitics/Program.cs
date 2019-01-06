using CognitiveApp.Services;
using FluentScheduler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using TwitterDatabase.Tweeter;

namespace TwitterDatabase
{
    public class Program
    {       
        public static async Task Main(string[] args)
        {
            string jsonContent = await File.ReadAllTextAsync($@"{Environment.CurrentDirectory}/appsettings.json");
            AppSettings settings = JsonSerializer.ConvertJsonTo<AppSettings>(jsonContent);
            string conn = settings.ConnectionString;
            var db = GetDb(conn);
            System.Net.Http.HttpClient sentimentClient = new System.Net.Http.HttpClient() { BaseAddress = new Uri("http://sentiment.api:5000") };
            TweeterSentiment tweeterSentiment = new TweeterSentiment(new SentimentService(sentimentClient));
            await EseguiAsync("CristianoRonaldo", tweeterSentiment, db);
            await EseguiAsync("Higuain", tweeterSentiment, db);
            await EseguiAsync("Ibra", tweeterSentiment, db);
            await EseguiAsync("Insigne", tweeterSentiment, db);
            await EseguiAsync("Juventus", tweeterSentiment, db);
            await EseguiAsync("Milan", tweeterSentiment, db);
            await EseguiAsync("Nainggolan", tweeterSentiment, db);
            await EseguiAsync("Napoli", tweeterSentiment, db);
            await EseguiAsync("Pogba", tweeterSentiment, db);
            await EseguiAsync("RealMadrid", tweeterSentiment, db);
        }


        private static async Task EseguiAsync(string searchKey, TweeterSentiment tweeterSentiment, TweeterContext db)
        {
            var searchTweet = await db.Tweets.Where(x => x.SearchKey == searchKey).Select(x => new TweeterSentimentEntity
            {
                Id = x.Id,
                Text = x.FullText,
                Language = GetLanguage(x.Language)
            }).ToListAsync();
            var result = await tweeterSentiment.ElaborateList(searchTweet);
            for (int i = 0; i < result.Count; i++)
            {
                var e = result.ElementAt(i);
                var entity = await db.Tweets.FindAsync(e.Id);
                if (entity != null && entity.Sentiment == 0)
                {
                    entity.Sentiment = e.Sentiment;
                    entity.LanguageISO6391 = GetLanguage(entity.Language);
                    if(i % 200 == 0 || i == result.Count - 1)
                        await db.SaveChangesAsync();
                }
            }
           
        }


        private static string GetLanguage(Language value)
        {
            switch (value)
            {
                case Language.English:
                    return "en";
                case Language.Italian:
                    return "it";
                case Language.Portuguese:
                    return "pt";
                case Language.French:
                    return "fr";
                case Language.German:
                    return "de";
                case Language.Spanish:
                    return "es";
                default:
                    return "it";
            }
        }

        private static TweeterContext GetDb(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TweeterContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new TweeterContext(optionsBuilder.Options);
        }
    }
}
