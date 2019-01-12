using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ARMWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;

namespace ARMWebApp.Controllers
{
    public class CustomVisionController : Controller
    {
        private readonly CustomVisionPredictionClient _cvClient;
        private readonly Guid _projectId;
        public CustomVisionController(CustomVisionUtility utility)
        {
            _cvClient = utility.Client;
            _projectId = utility.ProjectId;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FromUrl(string url)
        {
            ImagePrediction response = await _cvClient.PredictImageUrlAsync(_projectId, new ImageUrl(url));
            var viewModel = new CustomVisionViewModel
            {
                Predictions = response.Predictions.OrderByDescending(x => x.Probability).Select(x => new PredictionsViewModel
                {
                    Probability = x.Probability,
                    TagId = x.TagId.ToString(),
                    TagName = x.TagName,                    
                })
            };
            return View("Index", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> FromByteArray(IFormFile imageSource)
        {
            using (var ms = new MemoryStream())
            {
                await imageSource.CopyToAsync(ms);
                ms.Position = 0;
                var response = await _cvClient.PredictImageAsync(_projectId, ms);
                var viewModel = new CustomVisionViewModel
                {
                    Predictions = response.Predictions.OrderByDescending(x => x.Probability).Select(x => new PredictionsViewModel
                    {
                        Probability = x.Probability,
                        TagId = x.TagId.ToString(),
                        TagName = x.TagName
                    })
                };
                return View("Index", viewModel);
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            _cvClient.Dispose();
            base.Dispose(disposing);            
        }
    }
}