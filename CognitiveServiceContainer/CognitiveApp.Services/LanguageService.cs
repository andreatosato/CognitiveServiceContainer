using CognitiveApp.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveApp.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly HttpClient _client;
        private const string ServiceEndpoint = "text/analytics/v2.0/languages";
        public LanguageService(HttpClient client)
        {
            _client = client;
        }

        public async Task<DetectedLanguage> GetSingleLanguageAsync(DocumentLanguageRequest request)
        {
            IEnumerable<DocumentLanguageResponse> result = await GetListLanguageAsync(new[] { request }.ToList());
            return result.ElementAt(0).DetectedLanguage.ElementAtOrDefault(0);
        }

        public async Task<IEnumerable<DocumentLanguageResponse>> GetListLanguageAsync(List<DocumentLanguageRequest> request)
        {
            LanguageRequest requestData = new LanguageRequest()
            {
                Documents = request
            };

            HttpResponseMessage reponseMessage = await _client.PostAsync(ServiceEndpoint, new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json"));
            reponseMessage.EnsureSuccessStatusCode();
            LanguageResponse result = JsonConvert.DeserializeObject<LanguageResponse>(await reponseMessage.Content.ReadAsStringAsync());
            if (result.Errors.Any())
            {
                //TODO: log errors
            }
            return result.Documents;
        }
    }
}
