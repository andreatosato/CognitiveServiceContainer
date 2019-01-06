using CognitiveApp.Entities;
using CognitiveApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterDatabase.Tweeter
{
    public class TweeterSentiment
    {
        private readonly ISentimentService _sentimentService;
        public TweeterSentiment(ISentimentService sentimentService)
        {
            _sentimentService = sentimentService;
        }

        public async Task<List<TweeterSentimentResult>> ElaborateList(List<TweeterSentimentEntity> tweeterSentimentEntities)
        {
            List<DocumentSentimentRequest> requests = new List<DocumentSentimentRequest>();
            List<DocumentSentimentResponse> response = new List<DocumentSentimentResponse>();
            List<TweeterSentimentResult> results = new List<TweeterSentimentResult>();
            
            for(int i = 0; i < tweeterSentimentEntities.Count; i++)
            {
                var e = tweeterSentimentEntities.ElementAt(i);
                requests.Add(new DocumentSentimentRequest()
                {
                    Id = e.Id,
                    Language = e.Language,
                    Text = e.Text
                });
                if((i % 50 == 0 && i != 0) || i == tweeterSentimentEntities.Count - 1)
                {
                    response = await _sentimentService.GetListSentimentAsync(requests);
                    foreach (var item in response)
                    {
                        results.Add(new TweeterSentimentResult
                        {
                            Id = item.Id,
                            Sentiment = item.Score
                        });
                    }
                    requests.Clear();
                    requests.Capacity = 0;
                }
            }
            return results;
        }
    }

    public class TweeterSentimentEntity
    {
        public long Id { get; set; }
        public string Language { get; set; }
        public string Text { get; set; }
    }

    public class TweeterSentimentResult
    {
        public long Id { get; set; }
        public double Sentiment { get; set; }
    }
}
