using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HttpClientCognitive.Models;

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
