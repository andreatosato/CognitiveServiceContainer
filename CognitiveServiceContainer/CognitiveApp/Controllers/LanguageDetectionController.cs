using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CognitiveApp.Models;
using CognitiveApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CognitiveApp.Controllers
{
    public class LanguageDetectionController : Controller
    {
        private ILanguageService _languageService;
        public LanguageDetectionController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DetectLanguage(string text)
        {
            var result = await _languageService.GetSingleLanguageAsync(new Entities.DocumentLanguageRequest()
            {
                Id = 1,
                Text = text
            });

            return View("Index",  new LanguageDetectionViewModel()
            {
                Name = result.Name,
                Iso6391Name = result.Iso6391Name,
                Score = result.Score
            });
        }
    }
}