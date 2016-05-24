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
        public ActionResult Index()
        {
            var quizRondes = db.QuizRondes.Include(q => q.Evenement).Include(q => q.Thema);
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

        // GET: QuizRonde/Create
        public ActionResult Create()
        {
            ViewBag.EvenementID = new SelectList(db.Evenements, "EvenementID", "Email_Quizmaster");
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
                return RedirectToAction("Index");
            }

            ViewBag.EvenementID = new SelectList(db.Evenements, "EvenementID", "Email_Quizmaster", quizRonde.EvenementID);
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
            ViewBag.EvenementID = new SelectList(db.Evenements, "EvenementID", "Email_Quizmaster", quizRonde.EvenementID);
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
                return RedirectToAction("Index");
            }
            ViewBag.EvenementID = new SelectList(db.Evenements, "EvenementID", "Email_Quizmaster", quizRonde.EvenementID);
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
            return RedirectToAction("Index");
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
