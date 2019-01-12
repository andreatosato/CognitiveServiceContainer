using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARMWebApp.Models
{
    public class CustomVisionViewModel
    {
        public IEnumerable<PredictionsViewModel> Predictions { get; set; }
    }

    public class PredictionsViewModel
    {
        public double Probability { get; set; }

        public string TagId { get; set; }

        public string TagName { get; set; }
    }
}
