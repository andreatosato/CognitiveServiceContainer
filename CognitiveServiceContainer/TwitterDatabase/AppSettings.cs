using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterDatabase
{
    public class AppSettings
    {
        public TwitterSettings Twitter {get; set;}
    }

    public class TwitterSettings
    {
        public string CONSUMER_KEY { get; set; }
        public string CONSUMER_SECRET { get; set; }
        public string ACCESS_TOKEN { get; set; }
        public string ACCESS_TOKEN_SECRET { get; set; }
    }
}
