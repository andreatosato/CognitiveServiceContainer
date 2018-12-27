using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CognitiveApp.Cognitive;
using CognitiveApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CognitiveApp.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }
        public double Score { get; set; }
        private ISentimentService _sentimentService;
        public ContactModel(ISentimentService sentimentService)
        {
            _sentimentService = sentimentService;
        }

        public void OnGet()
        {
            Message = "Your contact page.";
        }

        public async Task OnPostProva()
        {
            SentimentRequest data = new SentimentRequest()
            {
                Documents = new[]
                {
                    new DocumentRequest()
                    {
                        Language = "it",
                        Text = "Ciao sono Cristiano"
                    }
                }.ToList()
            };


            Score = await _sentimentService.GetSingleSentiment(data.Documents.First());

            //            var requestText = JsonConvert.SerializeObject(data);
            //var httpResult = await client.PostAsJsonAsync("text/analytics/v2.0/sentiment", data);
            //if (httpResult.IsSuccessStatusCode)
            //{
            //    var resultComplete = await httpResult.Content.ReadAsStringAsync();
            //    var response = JsonConvert.DeserializeObject<SentimentResponse>(resultComplete);
            //    Score = response.Documents[0].Score;
            //}
            //else
            //{
            //    var resultError = await httpResult.Content.ReadAsStringAsync();
            //}

        }
    }
}
