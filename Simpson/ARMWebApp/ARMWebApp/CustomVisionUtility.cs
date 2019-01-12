using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARMWebApp
{
    public class CustomVisionUtility
    {
        public CustomVisionUtility()
        {

        }

        public CustomVisionPredictionClient Client { get; set; }
        public Guid ProjectId { get; set; }
    }
}
