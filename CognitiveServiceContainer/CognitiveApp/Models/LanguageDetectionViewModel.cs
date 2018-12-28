using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveApp.Models
{
    public class LanguageDetectionViewModel
    {
        public string Name { get; set; }
        public string Iso6391Name { get; set; }
        public double Score { get; set; }
    }
}
