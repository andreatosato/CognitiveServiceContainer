using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CognitiveApp.Models;
using CognitiveApp.Services;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
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

        [HttpPost]
        public async Task<IActionResult> FromByteArray(IFormFile imageSource)
        {
            using(var ms = new MemoryStream())
            {
                await imageSource.CopyToAsync(ms);
                ms.Position = 0;
                var response = await _customVision.FromByteArrayImage(ms);
                var viewModel = new CustomVisionViewModel
                {
                    Predictions = response.Predictions.OrderByDescending(x => x.Probability).Select(x => new PredictionsViewModel
                    {
                        Probability = x.Probability,
                        TagId = x.TagId,
                        TagName = x.TagName
                    })
                };
                return View("Index", viewModel);
            }
           // var response = await _customVision.FromByteArrayImage(await System.IO.File.ReadAllBytesAsync(imageSource.FileName));
           
        }
    }
}