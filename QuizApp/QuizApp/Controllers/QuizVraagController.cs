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
    public class QuizVraagController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        // GET: QuizVraag
        public ActionResult Index()
        {
            var quizVraags = db.QuizVraags.Include(q => q.Thema);
            return View(quizVraags.ToList());
        }

        // GET: QuizVraag/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizVraag quizVraag = db.QuizVraags.Find(id);
            if (quizVraag == null)
            {
                return HttpNotFound();
            }
            return View(quizVraag);
        }

        // GET: QuizVraag/Create
        public ActionResult Create()
        {
            ViewBag.Thema_Naam = new SelectList(db.Themas, "Thema_Naam", "Thema_Naam");
            return View();
        }

        // POST: QuizVraag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuizVraagID,Thema_Naam,Vraag,Vraagtype,Open_Vraag_Antwoord")] QuizVraag quizVraag)
        {
            if (ModelState.IsValid)
            {
                db.QuizVraags.Add(quizVraag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Thema_Naam = new SelectList(db.Themas, "Thema_Naam", "Thema_Naam", quizVraag.Thema_Naam);
            return View(quizVraag);
        }

        // GET: QuizVraag/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizVraag quizVraag = db.QuizVraags.Find(id);
            if (quizVraag == null)
            {
                return HttpNotFound();
            }
            ViewBag.Thema_Naam = new SelectList(db.Themas, "Thema_Naam", "Thema_Naam", quizVraag.Thema_Naam);
            return View(quizVraag);
        }

        // POST: QuizVraag/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuizVraagID,Thema_Naam,Vraag,Vraagtype,Open_Vraag_Antwoord")] QuizVraag quizVraag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quizVraag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Thema_Naam = new SelectList(db.Themas, "Thema_Naam", "Thema_Naam", quizVraag.Thema_Naam);
            return View(quizVraag);
        }

        // GET: QuizVraag/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizVraag quizVraag = db.QuizVraags.Find(id);
            if (quizVraag == null)
            {
                return HttpNotFound();
            }
            return View(quizVraag);
        }

        // POST: QuizVraag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuizVraag quizVraag = db.QuizVraags.Find(id);
            db.QuizVraags.Remove(quizVraag);
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