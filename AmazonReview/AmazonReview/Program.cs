using AmazonDatabase.Entity;
using CognitiveApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmazonReview
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Net.Http.HttpClient sentimentClient = new System.Net.Http.HttpClient() { BaseAddress = new Uri("http://sentiment.api:5000") };
            var sentimentService = new SentimentService(sentimentClient);
            AmazonReviewService amazonReviewService = new AmazonReviewService(sentimentService);
            var optionsBuilder = new DbContextOptionsBuilder<AmazonReviewContext>();
            optionsBuilder.UseSqlServer("Server=192.168.1.104,1433;Database=AmazonReviewContext;User Id=sa;Password=SA;");
            AmazonReviewContext db = new AmazonReviewContext(optionsBuilder.Options);
            int elements = await db.AmazonReview.CountAsync();
            List<AmazonReviewRequest> requests = new List<AmazonReviewRequest>();
            for (long i = 1; i != elements; i++)
            {
                var e = await db.AmazonReview.FindAsync(i);
                requests.Add(new AmazonReviewRequest { Id = e.Id, Text = e.ReviewText });
                if (i % 50 == 0 || i == elements)
                {
                    var results = await amazonReviewService.GetSentimentsAsync(requests);
                    foreach (var item in results)
                    {
                        var elementToUpdate = await db.AmazonReview.FindAsync(item.Id);
                        elementToUpdate.Sentiment = item.Score;
                    }
                    await db.SaveChangesAsync();
                    requests.Clear();
                    requests.Capacity = 0;
                }
            }
            
        }
    }
}
