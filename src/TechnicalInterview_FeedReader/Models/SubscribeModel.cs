using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TechnicalInterview_FeedReader.Models
{
    public class SubscribeModel
    {
        [Key]
        public int UserFeedID { get; set; }
        public int UserID { get; set; }
        public string FeedUri { get; set; }
    }

    public class FeedSubsContext : DbContext
    {
        public FeedSubsContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<SubscribeModel> RSSFeeds { get; set; }
    }
}