using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ARMWebApp.Models;
using ARMWebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;

namespace ARMWebApp.Controllers
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
                ImageUrl = url,
                Predictions = response.Predictions.OrderByDescending(x => x.Probability).Select(x => new PredictionsViewModel
                {
                    Probability = x.Probability,
                    TagId = x.TagId.ToString(),
                    TagName = x.TagName,
                    BoundingBox = new BoundingBoxViewModel
                    {
                        Left = x.BoundingBox.Left,
                        Top = x.BoundingBox.Top,
                        Height = x.BoundingBox.Height,
                        Width = x.BoundingBox.Width
                    }
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
                var response = await _customVision.FromByteArrayImage(ms);
                MemoryStream imageOutput = new MemoryStream();                
                await imageSource.CopyToAsync(imageOutput);
                imageOutput.Position = 0;
                string imageBase64 = Convert.ToBase64String(imageOutput.ToArray());
                var viewModel = new CustomVisionViewModel
                {
                    ImageSource = imageBase64,
                    Predictions = response.Predictions.OrderByDescending(x => x.Probability).Select(x => new PredictionsViewModel
                    {
                        Probability = x.Probability,
                        TagId = x.TagId.ToString(),
                        TagName = x.TagName,
                        BoundingBox = new BoundingBoxViewModel
                        {
                            Left = x.BoundingBox.Left,
                            Top = x.BoundingBox.Top,
                            Height = x.BoundingBox.Height,
                            Width = x.BoundingBox.Width
                        }
                    })
                };
                return View("Index", viewModel);
            }
            
        }

       
        private async Task<byte[]> GetImageAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
        }
    }
}