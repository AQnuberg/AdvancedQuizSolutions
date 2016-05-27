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
    public class QuizVraagController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        // GET: QuizVraag
        public ActionResult Index()
        {
            var quizVraags = db.QuizVragen.Include(q => q.Thema);
            return View(quizVraags.ToList());
        }

        // GET: QuizVraag/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizVraag quizVraag = db.QuizVragen.Find(id);
            if (quizVraag == null)
            {
                return HttpNotFound();
            }
            return View(quizVraag);
        }

        // GET: QuizVraag/Create
        public ActionResult Create()
        {
            List < SelectListItem > vraagtypes = new List<SelectListItem>();
            vraagtypes.Add(new SelectListItem { Text = "Open" });
            vraagtypes.Add(new SelectListItem { Text = "Meerkeuze" });
            ViewBag.Vraagtype = new SelectList(vraagtypes, "Text", "Text");
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
            var quizvragen = from qv in db.QuizVragen
                             where qv.Vraag == quizVraag.Vraag
                             select qv;

            if (quizvragen.FirstOrDefault() == null) {
                if (ModelState.IsValid)
                {
                    db.QuizVragen.Add(quizVraag);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Thema_Naam = new SelectList(db.Themas, "Thema_Naam", "Thema_Naam", quizVraag.Thema_Naam);
                return View(quizVraag);
            }
            else
            {
                ViewBag.message = "Vraag bestaat al";
                ViewBag.linkText = "Terug naar vraag";
                ViewBag.actionName = "index";
                ViewBag.routeValue = new { controller = "QuizVraag" };
                return View("Error");
            }
        }

        // GET: QuizVraag/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizVraag quizVraag = db.QuizVragen.Find(id);
            if (quizVraag == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> vraagtypes = new List<SelectListItem>();
            vraagtypes.Add(new SelectListItem { Text = "Open" });
            vraagtypes.Add(new SelectListItem { Text = "Meerkeuze" });
            ViewBag.Vraagtype = new SelectList(vraagtypes, "Text", "Text", quizVraag.Vraagtype);
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
            var quizvragen = from qv in db.QuizVragen
                             where qv.Vraag == quizVraag.Vraag && qv.QuizVraagID != quizVraag.QuizVraagID
                             select qv;

            if (quizvragen.FirstOrDefault() == null)
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
            else
            {
                ViewBag.message = "Vraag bestaat al";
                ViewBag.linkText = "Terug naar vraag";
                ViewBag.actionName = "index";
                ViewBag.routeValue = new { controller = "QuizVraag" };
                return View("Error");
            }
        }

        // GET: QuizVraag/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizVraag quizVraag = db.QuizVragen.Find(id);
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
            QuizVraag quizVraag = db.QuizVragen.Find(id);
            db.QuizVragen.Remove(quizVraag);
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
