using CognitiveApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonReview
{
    public class AmazonReviewService
    {
        private readonly SentimentService _sentimentService;
        public AmazonReviewService(SentimentService sentimentService)
        {
            _sentimentService = sentimentService;
        }

        public async Task<List<AmazonReviewResult>> GetSentimentsAsync(List<AmazonReviewRequest> request)
        {
            var docRequest = request.Select(x => new CognitiveApp.Entities.DocumentSentimentRequest { Id = x.Id, Language = "en", Text = x.Text }).ToList();
            var r = await _sentimentService.GetListSentimentAsync(docRequest);
            return r.Select(x => new AmazonReviewResult() { Id = x.Id, Score = x.Score }).ToList();
        }
    }

    public class AmazonReviewResult
    {
        public long Id { get; set; }
        public double Score { get; set; }
    }

    public class AmazonReviewRequest
    {
        public long Id { get; set; }
        public string Text { get; set; }
    }
}
