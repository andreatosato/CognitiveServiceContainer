using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARMWebApp.Models
{
    public class CustomVisionViewModel
    {
        public string ImageSource { get; set; }
        public IEnumerable<PredictionsViewModel> Predictions { get; set; }
    }

    public class PredictionsViewModel
    {
        public double Probability { get; set; }

        public string TagId { get; set; }

        public string TagName { get; set; }
        public BoundingBoxViewModel BoundingBox { get; set; }
    }

    public class BoundingBoxViewModel
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
