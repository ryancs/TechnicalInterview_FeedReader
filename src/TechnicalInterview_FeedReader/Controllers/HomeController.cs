using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechnicalInterview_FeedReader.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the homepage for my RSS feeder.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your own personal news feed reader!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Have any questions about the site or your account information? Contact us through one of these channels!";

            return View();
        }
    }
}
