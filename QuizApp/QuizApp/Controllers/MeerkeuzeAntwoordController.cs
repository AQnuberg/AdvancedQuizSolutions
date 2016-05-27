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
    public class MeerkeuzeAntwoordController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        // GET: MeerkeuzeAntwoord
        public ActionResult Index(int id)
        {
            var QueryQuizvraag  = from qv in db.QuizVragen
                                where qv.QuizVraagID == id
                                select qv;
            var Vraag = QueryQuizvraag.FirstOrDefault<QuizVraag>();

            var QueryMKAntwoord = from mk in db.MeerkeuzeAntwoorden
                                  where mk.QuizVraagID == id
                                  select mk;

            ViewBag.Vraag = Vraag.Vraag;
            ViewBag.VraagID = Vraag.QuizVraagID;
            var meerkeuzeAntwoords = QueryMKAntwoord.Include(m => m.QuizVraag);
            return View(meerkeuzeAntwoords.ToList());
        }

        // GET: MeerkeuzeAntwoord/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeerkeuzeAntwoord meerkeuzeAntwoord = db.MeerkeuzeAntwoorden.Find(id);
            if (meerkeuzeAntwoord == null)
            {
                return HttpNotFound();
            }
            return View(meerkeuzeAntwoord);
        }

        // GET: MeerkeuzeAntwoord/Create
        public ActionResult Create(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var QueryQuizvraag = from qv in db.QuizVragen
                                 where qv.QuizVraagID == id
                                 select qv;
            var Vraag = QueryQuizvraag.FirstOrDefault<QuizVraag>();

            ViewBag.Vraag = Vraag.Vraag;
            ViewBag.VraagID = id;
            return View();
        }

        // POST: MeerkeuzeAntwoord/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeerkeuzeAntwoordID,Meerkeuze_Antwoord,QuizVraagID,Is_Juist")] MeerkeuzeAntwoord meerkeuzeAntwoord)
        {
            var antwoord = from a in db.MeerkeuzeAntwoorden
                           where a.QuizVraagID == meerkeuzeAntwoord.QuizVraagID && a.Meerkeuze_Antwoord == meerkeuzeAntwoord.Meerkeuze_Antwoord
                           select a;
            if (antwoord.FirstOrDefault() == null)
            {
                if (ModelState.IsValid)
                {
                    db.MeerkeuzeAntwoorden.Add(meerkeuzeAntwoord);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = meerkeuzeAntwoord.QuizVraagID });
                }

                ViewBag.QuizVraagID = new SelectList(db.QuizVragen, "QuizVraagID", "Thema_Naam", meerkeuzeAntwoord.QuizVraagID);
                return View(meerkeuzeAntwoord);
            }
            else
            {
                ViewBag.message = "Antwoord bestaat al";
                ViewBag.linkText = "Terug naar meerkeuzeAntwoord";
                ViewBag.actionName = "index";
                ViewBag.routeValue = new { controller = "MeerkeuzeAntwoord", id = meerkeuzeAntwoord.QuizVraagID};
                return View("Error");
            }
        }

        // GET: MeerkeuzeAntwoord/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeerkeuzeAntwoord meerkeuzeAntwoord = db.MeerkeuzeAntwoorden.Find(id);
            if (meerkeuzeAntwoord == null)
            {
                return HttpNotFound();
            }
            return View(meerkeuzeAntwoord);
        }

        // POST: MeerkeuzeAntwoord/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MeerkeuzeAntwoordID,Meerkeuze_Antwoord,QuizVraagID,Is_Juist,Quizvraag")] MeerkeuzeAntwoord meerkeuzeAntwoord)
        {
            var antwoord = from a in db.MeerkeuzeAntwoorden
                           where a.QuizVraagID == meerkeuzeAntwoord.QuizVraagID && 
                           a.Meerkeuze_Antwoord == meerkeuzeAntwoord.Meerkeuze_Antwoord &&
                           a.MeerkeuzeAntwoordID != meerkeuzeAntwoord.MeerkeuzeAntwoordID
                           select a;
            if (antwoord.FirstOrDefault() == null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(meerkeuzeAntwoord).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = meerkeuzeAntwoord.QuizVraagID });
                }
                ViewBag.QuizVraagID = new SelectList(db.QuizVragen, "QuizVraagID", "Thema_Naam", meerkeuzeAntwoord.QuizVraagID);
                return View(meerkeuzeAntwoord);
            }
            else
            {
                ViewBag.message = "Antwoord bestaat al";
                ViewBag.linkText = "Terug naar meerkeuzeAntwoord";
                ViewBag.actionName = "index";
                ViewBag.routeValue = new { controller = "MeerkeuzeAntwoord", id = meerkeuzeAntwoord.QuizVraagID };
                return View("Error");
            }
        }

        // GET: MeerkeuzeAntwoord/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeerkeuzeAntwoord meerkeuzeAntwoord = db.MeerkeuzeAntwoorden.Find(id);
            if (meerkeuzeAntwoord == null)
            {
                return HttpNotFound();
            }
            return View(meerkeuzeAntwoord);
        }

        // POST: MeerkeuzeAntwoord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MeerkeuzeAntwoord meerkeuzeAntwoord = db.MeerkeuzeAntwoorden.Find(id);
            db.MeerkeuzeAntwoorden.Remove(meerkeuzeAntwoord);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = meerkeuzeAntwoord.QuizVraagID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
