using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace URLShortener.Models
{
    public class URLShortenerApiResult
    {
        public string kind { get; set; }
        public string id { get; set; }
        public string longUrl { get; set; }
    }
}