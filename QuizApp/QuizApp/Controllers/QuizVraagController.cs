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
        [Authorize(Roles = "Beheerder")]
        public ActionResult Index()
        {
            var quizVraags = db.QuizVraag.Include(q => q.Thema);
            return View(quizVraags.ToList());
        }

        // GET: QuizVraag/Details/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizVraag quizVraag = db.QuizVraag.Find(id);
            if (quizVraag == null)
            {
                return HttpNotFound();
            }
            return View(quizVraag);
        }

        // GET: QuizVraag/Create
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create()
        {
            List < SelectListItem > vraagtypes = new List<SelectListItem>();
            vraagtypes.Add(new SelectListItem { Text = "Open" });
            vraagtypes.Add(new SelectListItem { Text = "Meerkeuze" });
            ViewBag.Vraagtype = new SelectList(vraagtypes, "Text", "Text");
            ViewBag.Themas = new SelectList(db.Thema, "ThemaID", "Thema_Naam");
            return View();
        }

        // POST: QuizVraag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create([Bind(Include = "QuizVraagID,ThemaID,Vraag,Vraagtype,Open_Vraag_Antwoord")] QuizVraag quizVraag)
        {
            var quizvragen = from qv in db.QuizVraag
                             where qv.Vraag == quizVraag.Vraag
                             select qv;

            if (quizvragen.FirstOrDefault() == null) {
                if (ModelState.IsValid)
                {
                    db.QuizVraag.Add(quizVraag);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Thema_Naam = new SelectList(db.Thema, "Thema_Naam", "Thema_Naam", quizVraag.ThemaID);
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
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizVraag quizVraag = db.QuizVraag.Find(id);
            if (quizVraag == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> vraagtypes = new List<SelectListItem>();
            vraagtypes.Add(new SelectListItem { Text = "Open" });
            vraagtypes.Add(new SelectListItem { Text = "Meerkeuze" });
            ViewBag.Vraagtype = new SelectList(vraagtypes, "Text", "Text", quizVraag.Vraagtype);
            ViewBag.Themas = new SelectList(db.Thema, "ThemaID", "Thema_Naam", quizVraag.ThemaID);
            return View(quizVraag);
        }

        // POST: QuizVraag/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit([Bind(Include = "QuizVraagID,ThemaID,Vraag,Vraagtype,Open_Vraag_Antwoord")] QuizVraag quizVraag)
        {
            var quizvragen = from qv in db.QuizVraag
                             where qv.Vraag == quizVraag.Vraag && qv.QuizVraagID != quizVraag.QuizVraagID
                             select qv;

            if (quizvragen.FirstOrDefault() == null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(quizVraag).State = System.Data.Entity.EntityState.Modified; ;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Thema_Naam = new SelectList(db.Thema, "Thema_Naam", "Thema_Naam", quizVraag.ThemaID);
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
        [Authorize(Roles = "Beheerder")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizVraag quizVraag = db.QuizVraag.Find(id);
            if (quizVraag == null)
            {
                return HttpNotFound();
            }
            return View(quizVraag);
        }

        // POST: QuizVraag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult DeleteConfirmed(int id)
        {
            QuizVraag quizVraag = db.QuizVraag.Find(id);
            db.QuizVraag.Remove(quizVraag);
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
