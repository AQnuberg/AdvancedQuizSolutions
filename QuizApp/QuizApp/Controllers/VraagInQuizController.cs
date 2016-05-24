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
    public class VraagInQuizController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        // GET: VraagInQuiz
        public ActionResult Index()
        {
            var vraagInQuizs = db.VraagInQuizs.Include(v => v.QuizRonde).Include(v => v.QuizVraag);
            return View(vraagInQuizs.ToList());
        }

        // GET: VraagInQuiz/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VraagInQuiz vraagInQuiz = db.VraagInQuizs.Find(id);
            if (vraagInQuiz == null)
            {
                return HttpNotFound();
            }
            return View(vraagInQuiz);
        }

        // GET: VraagInQuiz/Create
        public ActionResult Create()
        {
            ViewBag.QuizRondeID = new SelectList(db.QuizRondes, "QuizRondeID", "Thema_Naam");
            ViewBag.QuizVraagID = new SelectList(db.QuizVraags, "QuizVraagID", "Thema_Naam");
            return View();
        }

        // POST: VraagInQuiz/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VraagInQuizID,QuizRondeID,QuizVraagID")] VraagInQuiz vraagInQuiz)
        {
            if (ModelState.IsValid)
            {
                db.VraagInQuizs.Add(vraagInQuiz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuizRondeID = new SelectList(db.QuizRondes, "QuizRondeID", "Thema_Naam", vraagInQuiz.QuizRondeID);
            ViewBag.QuizVraagID = new SelectList(db.QuizVraags, "QuizVraagID", "Thema_Naam", vraagInQuiz.QuizVraagID);
            return View(vraagInQuiz);
        }

        // GET: VraagInQuiz/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VraagInQuiz vraagInQuiz = db.VraagInQuizs.Find(id);
            if (vraagInQuiz == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuizRondeID = new SelectList(db.QuizRondes, "QuizRondeID", "Thema_Naam", vraagInQuiz.QuizRondeID);
            ViewBag.QuizVraagID = new SelectList(db.QuizVraags, "QuizVraagID", "Thema_Naam", vraagInQuiz.QuizVraagID);
            return View(vraagInQuiz);
        }

        // POST: VraagInQuiz/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VraagInQuizID,QuizRondeID,QuizVraagID")] VraagInQuiz vraagInQuiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vraagInQuiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuizRondeID = new SelectList(db.QuizRondes, "QuizRondeID", "Thema_Naam", vraagInQuiz.QuizRondeID);
            ViewBag.QuizVraagID = new SelectList(db.QuizVraags, "QuizVraagID", "Thema_Naam", vraagInQuiz.QuizVraagID);
            return View(vraagInQuiz);
        }

        // GET: VraagInQuiz/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VraagInQuiz vraagInQuiz = db.VraagInQuizs.Find(id);
            if (vraagInQuiz == null)
            {
                return HttpNotFound();
            }
            return View(vraagInQuiz);
        }

        // POST: VraagInQuiz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VraagInQuiz vraagInQuiz = db.VraagInQuizs.Find(id);
            db.VraagInQuizs.Remove(vraagInQuiz);
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
