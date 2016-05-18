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
    public class LocatiesController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        // GET: Locaties
        public ActionResult Index()
        {
            return View(db.Locatie.ToList());
        }

        // GET: Locaties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locatie locatie = db.Locatie.Find(id);
            if (locatie == null)
            {
                return HttpNotFound();
            }
            return View(locatie);
        }

        // GET: Locaties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locaties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocatieID,Locatienaam,Plaatsnaam,Postcode,Huisnummer,Straat")] Locatie locatie)
        {
            if (ModelState.IsValid)
            {
                db.Locatie.Add(locatie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(locatie);
        }

        // GET: Locaties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locatie locatie = db.Locatie.Find(id);
            if (locatie == null)
            {
                return HttpNotFound();
            }
            return View(locatie);
        }

        // POST: Locaties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocatieID,Locatienaam,Plaatsnaam,Postcode,Huisnummer,Straat")] Locatie locatie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locatie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(locatie);
        }

        // GET: Locaties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Locatie locatie = db.Locatie.Find(id);
            if (locatie == null)
            {
                return HttpNotFound();
            }
            return View(locatie);
        }

        // POST: Locaties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Locatie locatie = db.Locatie.Find(id);
            db.Locatie.Remove(locatie);
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
