using CognitiveApp.Cognitive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CognitiveApp.Services
{
    public class SentimentService : ISentimentService
    {
        private readonly HttpClient _client;
        private const string ServiceEndpoint = "text/analytics/v2.0/sentiment";
        public SentimentService(HttpClient client)
        {
            _client = client;
        }

        public async Task<double> GetSingleSentiment(DocumentRequest request)
        {
            SentimentRequest requestData = new SentimentRequest()
            {
                Documents = new[] { request }.ToList()
            };

            HttpResponseMessage reponseMessage = await _client.PostAsJsonAsync(ServiceEndpoint, requestData);
            reponseMessage.EnsureSuccessStatusCode();
            var result = await reponseMessage.Content.ReadAsAsync<SentimentResponse>();
            return result.Documents.SingleOrDefault().Score;
        }
    }

    public interface ISentimentService
    {
        Task<double> GetSingleSentiment(DocumentRequest request);
    }
}
