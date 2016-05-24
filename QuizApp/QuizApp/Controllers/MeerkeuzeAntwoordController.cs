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
        public ActionResult Index()
        {
            var meerkeuzeAntwoords = db.MeerkeuzeAntwoorden.Include(m => m.QuizVraag);
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
        public ActionResult Create()
        {
            ViewBag.QuizVraagID = new SelectList(db.QuizVragen, "QuizVraagID", "Thema_Naam");
            return View();
        }

        // POST: MeerkeuzeAntwoord/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeerkeuzeAntwoordID,Meerkeuze_Antwoord,QuizVraagID,Is_Juist")] MeerkeuzeAntwoord meerkeuzeAntwoord)
        {
            if (ModelState.IsValid)
            {
                db.MeerkeuzeAntwoorden.Add(meerkeuzeAntwoord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuizVraagID = new SelectList(db.QuizVragen, "QuizVraagID", "Thema_Naam", meerkeuzeAntwoord.QuizVraagID);
            return View(meerkeuzeAntwoord);
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
            ViewBag.QuizVraagID = new SelectList(db.QuizVragen, "QuizVraagID", "Thema_Naam", meerkeuzeAntwoord.QuizVraagID);
            return View(meerkeuzeAntwoord);
        }

        // POST: MeerkeuzeAntwoord/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MeerkeuzeAntwoordID,Meerkeuze_Antwoord,QuizVraagID,Is_Juist")] MeerkeuzeAntwoord meerkeuzeAntwoord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meerkeuzeAntwoord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuizVraagID = new SelectList(db.QuizVragen, "QuizVraagID", "Thema_Naam", meerkeuzeAntwoord.QuizVraagID);
            return View(meerkeuzeAntwoord);
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
