using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class HomeController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        public ActionResult Index()
        {
            var evenementen = from e in db.Evenementen
                           where e.Begintijd > DateTime.Now
                           orderby e.Begintijd ascending
                           select e;
            return View(evenementen.ToList());
            //return View(db.Evenementen);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}