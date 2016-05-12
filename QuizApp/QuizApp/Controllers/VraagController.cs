﻿using System;
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
    public class VraagController : Controller
    {
        private Entities db = new Entities();

        // GET: Vraag
        public ActionResult Index()
        {
            var vragen = db.Vragen.Include(v => v.Thema);
            return View(vragen.ToList());
        }

        // GET: Vraag/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vraag vraag = db.Vragen.Find(id);
            if (vraag == null)
            {
                return HttpNotFound();
            }
            return View(vraag);
        }

        // GET: Vraag/Create
        public ActionResult Create()
        {
            ViewBag.ThemaNaam = new SelectList(db.Themas, "Naam", "Naam");
            return View();
        }

        // POST: Vraag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VraagID,ThemaNaam,Vraag1,Vraagtype,AntwoordOpenVraag")] Vraag vraag)
        {
            if (ModelState.IsValid)
            {
                db.Vragen.Add(vraag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ThemaNaam = new SelectList(db.Themas, "Naam", "Naam", vraag.ThemaNaam);
            return View(vraag);
        }

        // GET: Vraag/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vraag vraag = db.Vragen.Find(id);
            if (vraag == null)
            {
                return HttpNotFound();
            }
            ViewBag.ThemaNaam = new SelectList(db.Themas, "Naam", "Naam", vraag.ThemaNaam);
            return View(vraag);
        }

        // POST: Vraag/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VraagID,ThemaNaam,Vraag1,Vraagtype,AntwoordOpenVraag")] Vraag vraag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vraag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ThemaNaam = new SelectList(db.Themas, "Naam", "Naam", vraag.ThemaNaam);
            return View(vraag);
        }

        // GET: Vraag/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vraag vraag = db.Vragen.Find(id);
            if (vraag == null)
            {
                return HttpNotFound();
            }
            return View(vraag);
        }

        // POST: Vraag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vraag vraag = db.Vragen.Find(id);
            db.Vragen.Remove(vraag);
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
