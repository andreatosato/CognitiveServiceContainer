using AmazonDatabase.Entity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmazonDatabase
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string line;
            List<AmazonReviewData> result = new List<AmazonReviewData>();
            var optionsBuilder = new DbContextOptionsBuilder<AmazonReviewContext>();
            optionsBuilder.UseSqlServer("Server=192.168.1.104,1433;Database=AmazonReviewContext;User Id=sa;Password=SA;");
            AmazonReviewContext db = new AmazonReviewContext(optionsBuilder.Options);
            System.IO.StreamReader file = new System.IO.StreamReader(@"E:\Progetti\CognitiveServiceContainer\AmazonReview\AmazonDatabase\Dataset\reviews\Electronics.json");
            int i = 0;
            while ((line = file.ReadLine()) != null)
            {
                var v = JsonConvert.DeserializeObject<AmazonReviewData>(line);
                db.AmazonReview.Add(new AmazonReviewEntity(v));
                i++;
                if (i % 1000 == 0 || file.EndOfStream)
                    await db.SaveChangesAsync();
            }
            
        }
    }
}
