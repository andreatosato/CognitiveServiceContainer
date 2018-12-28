using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CognitiveApp.Models;
using CognitiveApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CognitiveApp.Controllers
{
    public class TextAnalisysController : Controller
    {
        private readonly ISentimentService _sentimentService;
        private readonly ILanguageService _languageService;
        public TextAnalisysController(ISentimentService sentimentService, ILanguageService languageService)
        {
            _sentimentService = sentimentService;
            _languageService = languageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DetectSentiment(string text)
        {
            var resultLanguage = await _languageService.GetSingleLanguageAsync(new Entities.DocumentLanguageRequest()
            {
                Id = 1,
                Text = text
            });

            var resultSentiment = await _sentimentService.GetSingleSentimentAsync(new Entities.DocumentSentimentRequest()
            {
                Id = 1,
                Language = resultLanguage.Iso6391Name,
                Text = text
            });

            return View("Index", new TextAnalisysViewModel()
            {
                Name = resultLanguage.Name,
                Iso6391Name = resultLanguage.Iso6391Name,
                Score = resultSentiment
            });
        }
        
    }
}