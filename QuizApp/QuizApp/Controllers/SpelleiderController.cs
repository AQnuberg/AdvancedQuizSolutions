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
            var evenement = from e in db.Evenement
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
            Evenement evenement = db.Evenement.Find(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            var evenementVragen = from e in db.Evenement
                                  join qr in db.QuizRonde on e.EvenementID equals qr.EvenementID
                                  join viq in db.VraagInQuiz on qr.QuizRondeID equals viq.QuizRondeID
                                  join qv in db.QuizVraag on viq.QuizVraagID equals qv.QuizVraagID
                                  where e.EvenementID == id
                                  select new  {e.EvenementID,e.Evenement_Naam,qr.Rondenummer, qv.Thema.Thema_Naam, qv.Vraag};
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