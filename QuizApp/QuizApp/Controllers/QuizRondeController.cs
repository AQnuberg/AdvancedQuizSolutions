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
    public class QuizRondeController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        // GET: QuizRonde
        public ActionResult Index(int? id)
        {
            if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

            var evenementNaam = from e in db.Evenementen
                                where e.EvenementID == id
                                select e;
            var firstOrDefault = evenementNaam.FirstOrDefault<Evenement>();
            if (firstOrDefault != null)
            ViewBag.naamBijID = firstOrDefault.Evenement_Naam;
            ViewBag.evenementID = id;


            var rondeBijEvenement = from e in db.QuizRondes
                                    select e;
            rondeBijEvenement = rondeBijEvenement.Where(s => s.EvenementID == id);
            var quizRondes = rondeBijEvenement.Include(r => r.Evenement).Include(r => r.Thema);
            
            return View(quizRondes.ToList());
        }

        // GET: QuizRonde/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizRonde quizRonde = db.QuizRondes.Find(id);
            if (quizRonde == null)
            {
                return HttpNotFound();
            }
            return View(quizRonde);
        }

        // GET: QuizRonde/Create/5
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.EvenementID = id;
            ViewBag.Thema_Naam = new SelectList(db.Themas, "Thema_Naam", "Thema_Naam");
            return View();
        }

        // POST: QuizRonde/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuizRondeID,EvenementID,Rondenummer,Thema_Naam")] QuizRonde quizRonde)
        {
            if (ModelState.IsValid)
            {
                db.QuizRondes.Add(quizRonde);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = quizRonde.EvenementID });
            }

            ViewBag.EvenementID = new SelectList(db.Evenementen, "EvenementID", "Email_Quizmaster", quizRonde.EvenementID);
            ViewBag.Thema_Naam = new SelectList(db.Themas, "Thema_Naam", "Thema_Naam", quizRonde.Thema_Naam);
            return View(quizRonde);
        }

        // GET: QuizRonde/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizRonde quizRonde = db.QuizRondes.Find(id);
            if (quizRonde == null)
            {
                return HttpNotFound();
            }
            ViewBag.EvenementID = new SelectList(db.Evenementen, "EvenementID", "Evenement_Naam", quizRonde.EvenementID);
            ViewBag.Thema_Naam = new SelectList(db.Themas, "Thema_Naam", "Thema_Naam", quizRonde.Thema_Naam);
            return View(quizRonde);
        }

        // POST: QuizRonde/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuizRondeID,EvenementID,Rondenummer,Thema_Naam")] QuizRonde quizRonde)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quizRonde).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = quizRonde.EvenementID });
            }
            ViewBag.EvenementID = new SelectList(db.Evenementen, "EvenementID", "Email_Quizmaster", quizRonde.EvenementID);
            ViewBag.Thema_Naam = new SelectList(db.Themas, "Thema_Naam", "Thema_Naam", quizRonde.Thema_Naam);
            return View(quizRonde);
        }

        // GET: QuizRonde/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizRonde quizRonde = db.QuizRondes.Find(id);
            if (quizRonde == null)
            {
                return HttpNotFound();
            }
            return View(quizRonde);
        }

        // POST: QuizRonde/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuizRonde quizRonde = db.QuizRondes.Find(id);
            db.QuizRondes.Remove(quizRonde);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = quizRonde.EvenementID });
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
