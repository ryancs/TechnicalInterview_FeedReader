using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnicalInterview_FeedReader.Models
{
    public class RSSFeedReaderModel
    {
        public string RSSTitle { get; set; }
        public string RSSFeedURL { get; set; }
        public IEnumerable<RSSFeedModel> RSSFeeds { get; set; }
        public IEnumerable<SubscribeModel> RSSURLS { get; set; }
    }
}