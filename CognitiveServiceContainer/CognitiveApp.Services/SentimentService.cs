using CognitiveApp.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        public async Task<double> GetSingleSentimentAsync(DocumentSentimentRequest request)
        {
            List<DocumentSentimentResponse> result = await GetListSentimentAsync(new[] { request }.ToList());
            return result.SingleOrDefault().Score;
        }

        public async Task<List<DocumentSentimentResponse>> GetListSentimentAsync(List<DocumentSentimentRequest> request)
        {
            SentimentRequest requestData = new SentimentRequest()
            {
                Documents = request
            };

            HttpResponseMessage reponseMessage = await _client.PostAsync(ServiceEndpoint, new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json"));
            reponseMessage.EnsureSuccessStatusCode();
            SentimentResponse result = JsonConvert.DeserializeObject<SentimentResponse>(await reponseMessage.Content.ReadAsStringAsync());
            if (result.Errors.Any())
            {
                //TODO: log errors
            }
            return result.Documents;
        }
    }
}
