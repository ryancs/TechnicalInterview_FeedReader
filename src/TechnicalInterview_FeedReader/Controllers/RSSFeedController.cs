using Argotic.Common;
using Argotic.Syndication;
using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using TechnicalInterview_FeedReader.Models;
using WebMatrix.WebData;

namespace TechnicalInterview_FeedReader.Controllers
{
    public class RSSFeedController : Controller
    {
        [HttpGet]
        public ActionResult Read(string URL)
        {
            return View(ReadFeedUrl(URL));
        }

        [HttpGet]
        public ActionResult ReadAll()
        {
            RSSFeedReaderModel displayAll = new RSSFeedReaderModel();
            List<RSSFeedModel> allFeeds = new List<RSSFeedModel>();
            FeedSubsContext feedSub = new FeedSubsContext();
            UsersContext users = new UsersContext();
            UserProfile user = users.UserProfiles.FirstOrDefault(usr => usr.UserName.ToLower() == User.Identity.Name.ToLower());

            RSSFeedReaderModel RSSModel = new RSSFeedReaderModel();
            try
            {

                RSSModel.RSSURLS = feedSub.RSSFeeds.Where(p => p.UserID == user.UserId);
            }
            catch (Exception e) { return View(); }

            foreach(SubscribeModel subs in  RSSModel.RSSURLS)
            {
                foreach(RSSFeedModel feed in ReadFeedUrl(subs.FeedUri).RSSFeeds)
                {
                    allFeeds.Add(feed);
                }
            }
            displayAll.RSSFeeds = allFeeds;

            return View(displayAll);
        }
        //
        // GET: /RSSFeed/
        [HttpGet]
        public ActionResult Index()
        {
            FeedSubsContext feedSub = new FeedSubsContext();
            UsersContext users = new UsersContext();
            UserProfile user = users.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == User.Identity.Name.ToLower());
            ViewBag.LoggedIn = user != null;

            RSSFeedReaderModel RSSModel = new RSSFeedReaderModel();
            try
            {
                
                RSSModel.RSSURLS = feedSub.RSSFeeds.Where(p => p.UserID == user.UserId);
            }
            catch (Exception e) { }

            return View(RSSModel);
        }

        [HttpPost]
        public ActionResult Index(RSSFeedReaderModel model)
        {
            FeedSubsContext feedSub = new FeedSubsContext();
            UsersContext users = new UsersContext();

            SubscribeModel subs = new SubscribeModel();

            UserProfile user = users.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == User.Identity.Name.ToLower());
            subs.UserID = user.UserId;
            subs.FeedUri = model.RSSFeedURL;

            feedSub.RSSFeeds.Add(subs);
            feedSub.SaveChanges();
            // add the url from here, to a table that has UserID and FeedURL's
            return RedirectToAction("Index");
        }

        public RSSFeedReaderModel ReadFeedUrl(string url)
        {
            SyndicationResourceLoadSettings rssLimit = new SyndicationResourceLoadSettings { RetrievalLimit = 10 };
            GenericSyndicationFeed rssFeed = GenericSyndicationFeed.Create(new Uri(url), rssLimit);

            if (rssFeed.Resource is RssFeed)
            {
                return new RSSFeedReaderModel
                {
                    RSSTitle = ((RssFeed)rssFeed.Resource).Channel.Title,
                    RSSFeeds = ((RssFeed)rssFeed.Resource).Channel.Items.Select(feed => new RSSFeedModel 
                                                                                            {
                                                                                                RSSFeedSource = feed.Link,
                                                                                                RSSFeedTitle = feed.Title,
                                                                                                RSSFeedDescription = feed.Description.Trim().Replace("<br>", ""),
                                                                                                RSSFeedDate = feed.PublicationDate
                                                                                             
                                                                                            })
                };
            }

            return null;
        }

    }
}
