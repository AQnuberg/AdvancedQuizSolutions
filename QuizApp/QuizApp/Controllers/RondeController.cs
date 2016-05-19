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
    public class RondeController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        // GET: Ronde
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var rondeBijEvenement = from e in db.Ronde
                            select e;
            rondeBijEvenement = rondeBijEvenement.Where(s => s.EvenementID == id);

            var ronde = rondeBijEvenement.Include(r => r.Evenement).Include(r => r.Thema);
            return View(ronde.ToList());
        }

        // GET: Ronde/Details/5
        public ActionResult Details(int? id, int? evenementID)
        {
            if (id == null || evenementID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ronde ronde = db.Ronde.Find(id, evenementID);
            if (ronde == null)
            {
                return HttpNotFound();
            }
            return View(ronde);
        }

        // GET: Ronde/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.EvenementID = new SelectList(db.Evenement, "Evenementnaam", "Evenementnaam");
            ViewBag.ThemaNaam = new SelectList(db.Thema, "Naam", "Naam");
            return View();
        }

        // POST: Ronde/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EvenementID,Ronde1,ThemaNaam")] Ronde ronde)
        {
            if (ModelState.IsValid)
            {
                db.Ronde.Add(ronde);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EvenementID = new SelectList(db.Evenement, "EvenementID", "Email_Quizmaster", ronde.EvenementID);
            ViewBag.ThemaNaam = new SelectList(db.Thema, "Naam", "Naam", ronde.ThemaNaam);
            return View(ronde);
        }

        // GET: Ronde/Edit/5
        public ActionResult Edit(int? id, int? evenementID)
        {
            if (id == null || evenementID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ronde ronde = db.Ronde.Find(id,evenementID);
            if (ronde == null)
            {
                return HttpNotFound();
            }
            ViewBag.EvenementID = new SelectList(db.Evenement, "EvenementID", "Evenementnaam", ronde.EvenementID);
            ViewBag.ThemaNaam = new SelectList(db.Thema, "Naam", "Naam", ronde.ThemaNaam);
            return View(ronde);
        }

        // POST: Ronde/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EvenementID,Ronde1,ThemaNaam")] Ronde ronde)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ronde).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EvenementID = new SelectList(db.Evenement, "EvenementID", "Email_Quizmaster", ronde.EvenementID);
            ViewBag.ThemaNaam = new SelectList(db.Thema, "Naam", "Naam", ronde.ThemaNaam);
            return View(ronde);
        }

        // GET: Ronde/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ronde ronde = db.Ronde.Find(id);
            if (ronde == null)
            {
                return HttpNotFound();
            }
            return View(ronde);
        }

        // POST: Ronde/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ronde ronde = db.Ronde.Find(id);
            db.Ronde.Remove(ronde);
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
