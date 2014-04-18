using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnicalInterview_FeedReader.Models
{
    public class RSSFeedModel
    {
        public Uri RSSFeedSource { get; set; }
        public string RSSFeedTitle { get; set; }
        public string RSSFeedDescription { get; set; }
        public DateTime RSSFeedDate { get; set; }
    }
}