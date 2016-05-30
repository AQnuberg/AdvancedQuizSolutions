using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuizApp.Models;


namespace QuizApp.Controllers
{
    public class SpelleiderController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();
        // GET: Spelleider
        public ActionResult Index()
        {
            var evenement = from e in db.Evenementen
                            where e.Eindtijd >= DateTime.Now
                            select e;

            return View(evenement.ToList());
        }

        // GET: Spelleider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenement evenement = db.Evenementen.Find(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            var evenementVragen = from e in db.Evenementen
                                  join qr in db.QuizRondes on e.EvenementID equals qr.EvenementID
                                  join viq in db.VraagInQuizzen on qr.QuizRondeID equals viq.QuizRondeID
                                  join qv in db.QuizVragen on viq.QuizVraagID equals qv.QuizVraagID
                                  where e.EvenementID == id
                                  select new  {e.EvenementID,e.Evenement_Naam,qr.Rondenummer, qr.Thema_Naam, qv.Vraag};
            var model = evenementVragen.Select(x => new SpelleiderEvenementVraagModels
            {
                EvenementID = x.EvenementID,
                Evenement_Naam = x.Evenement_Naam,
                Rondenummer = x.Rondenummer,
                Thema_Naam = x.Thema_Naam,
                Vraag = x.Vraag
            });
            return View(model);
        }
    }
}