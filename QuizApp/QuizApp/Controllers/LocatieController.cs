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
    public class LocatieController : Controller
    {
        private Entities db = new Entities();

        // GET: Locatie
        public ActionResult Index()
        {
            return View(db.Locaties.ToList());
        }

        // GET: Locatie/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locatie locatie = db.Locaties.Find(id);
            if (locatie == null)
            {
                return HttpNotFound();
            }
            return View(locatie);
        }

        // GET: Locatie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locatie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Naam,Plaatsnaam,Postcode,Huisnummer,Straat")] Locatie locatie)
        {
            if (ModelState.IsValid)
            {
                db.Locaties.Add(locatie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(locatie);
        }

        // GET: Locatie/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locatie locatie = db.Locaties.Find(id);
            if (locatie == null)
            {
                return HttpNotFound();
            }
            return View(locatie);
        }

        // POST: Locatie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Naam,Plaatsnaam,Postcode,Huisnummer,Straat")] Locatie locatie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locatie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locatie);
        }

        // GET: Locatie/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locatie locatie = db.Locaties.Find(id);
            if (locatie == null)
            {
                return HttpNotFound();
            }
            return View(locatie);
        }

        // POST: Locatie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Locatie locatie = db.Locaties.Find(id);
            db.Locaties.Remove(locatie);
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
