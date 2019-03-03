using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClientCognitive.Models
{
    public class IndexViewModel
    {
        public string Text { get; set; }
    }

    public class ResultViewModel
    {
        public LanguageResponse Response { get; set; }
    }
}
