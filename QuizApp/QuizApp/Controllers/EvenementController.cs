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
    public class EvenementController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        // GET: Evenement
        public ActionResult Index()
        {
            var evenements = db.Evenements.Include(e => e.Account).Include(e => e.Locatie);
            return View(evenements.ToList());
        }

        // GET: Evenement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenement evenement = db.Evenements.Find(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            return View(evenement);
        }

        // GET: Evenement/Create
        public ActionResult Create()
        {
            ViewBag.Email_Quizmaster = new SelectList(db.Accounts, "Email", "Rolnaam");
            ViewBag.LocatieID = new SelectList(db.Locaties, "LocatieID", "Locatie_Naam");
            return View();
        }

        // POST: Evenement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EvenementID,LocatieID,Email_Quizmaster,Evenement_Naam,Begintijd,Eindtijd,Evenement_Type")] Evenement evenement)
        {
            if (ModelState.IsValid)
            {
                db.Evenements.Add(evenement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Email_Quizmaster = new SelectList(db.Accounts, "Email", "Rolnaam", evenement.Email_Quizmaster);
            ViewBag.LocatieID = new SelectList(db.Locaties, "LocatieID", "Locatie_Naam", evenement.LocatieID);
            return View(evenement);
        }

        // GET: Evenement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenement evenement = db.Evenements.Find(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            ViewBag.Email_Quizmaster = new SelectList(db.Accounts, "Email", "Rolnaam", evenement.Email_Quizmaster);
            ViewBag.LocatieID = new SelectList(db.Locaties, "LocatieID", "Locatie_Naam", evenement.LocatieID);
            return View(evenement);
        }

        // POST: Evenement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EvenementID,LocatieID,Email_Quizmaster,Evenement_Naam,Begintijd,Eindtijd,Evenement_Type")] Evenement evenement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evenement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Email_Quizmaster = new SelectList(db.Accounts, "Email", "Rolnaam", evenement.Email_Quizmaster);
            ViewBag.LocatieID = new SelectList(db.Locaties, "LocatieID", "Locatie_Naam", evenement.LocatieID);
            return View(evenement);
        }

        // GET: Evenement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenement evenement = db.Evenements.Find(id);
            if (evenement == null)
            {
                return HttpNotFound();
            }
            return View(evenement);
        }

        // POST: Evenement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evenement evenement = db.Evenements.Find(id);
            db.Evenements.Remove(evenement);
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
