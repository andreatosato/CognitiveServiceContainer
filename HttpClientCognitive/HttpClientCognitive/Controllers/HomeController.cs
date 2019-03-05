using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HttpClientCognitive.Models;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using HttpClientCognitive.Models.SDK;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;

namespace HttpClientCognitive.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICognitiveClient _cognitiveClient;
        public HomeController(ICognitiveClient cognitiveClient)
        {
            _cognitiveClient = cognitiveClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetLanguage(IndexViewModel model)
        {
            List<DocumentLanguageRequest> languageRequest = new List<DocumentLanguageRequest>();
            languageRequest.Add(new DocumentLanguageRequest() { Id = 1, Text = model.Text });

            var response = await _cognitiveClient.GetListLanguageAsync(new LanguageRequest() { Documents = languageRequest });
            return View("Result", new ResultViewModel { Response = response });
        }

        [HttpPost]
        public async Task<IActionResult> GetLanguageSDK(IndexViewModel model)
        {
            // Create a client.
            ITextAnalyticsClient client = new TextAnalyticsClient(
                new ApiKeyServiceClientCredentials(), 
                new System.Net.Http.HttpClient() {BaseAddress = new Uri("http://language.api:5000/") },
                false)
            {
                Endpoint = "https://westeurope.api.cognitive.microsoft.com"
            };

            var result = await client.DetectLanguageAsync(new BatchInput(
                   new List<Input>()
                       {
                          new Input("1", model.Text)
                   }));
            // MAP
            var response = new LanguageResponse()
            {
                Documents = new List<DocumentLanguageResponse>()
            };
            foreach (var item in result.Documents)
            {
                List<Models.DetectedLanguage> detecteds = new List<Models.DetectedLanguage>();
                foreach (var l in item.DetectedLanguages)
                {
                    detecteds.Add(new Models.DetectedLanguage()
                    {
                        Iso6391Name = l.Iso6391Name,
                        Name = l.Name,
                        Score = l.Score.Value
                    });
                }
                response.Documents.Add(new DocumentLanguageResponse() { Id = long.Parse(item.Id), DetectedLanguage = detecteds });
            }
            // END MAP
            return View("Result", new ResultViewModel { Response = response });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
