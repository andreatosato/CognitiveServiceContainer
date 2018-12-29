using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CognitiveApp.Models;
using CognitiveApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CognitiveApp.Controllers
{
    public class CustomVisionController : Controller
    {
        private readonly ICustomVision _customVision;
        public CustomVisionController(ICustomVision customVision)
        {
            _customVision = customVision;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> FromUrl(string url)
        {
            var response = await _customVision.FromUrlImage(new Uri(url));
            var viewModel = new CustomVisionViewModel
            {
                Predictions = response.Predictions.OrderByDescending(x => x.Probability).Select(x => new PredictionsViewModel {
                    Probability = x.Probability,
                    TagId = x.TagId,
                    TagName = x.TagName
                })
            };
            return View("Index", viewModel);
        }
    }
}