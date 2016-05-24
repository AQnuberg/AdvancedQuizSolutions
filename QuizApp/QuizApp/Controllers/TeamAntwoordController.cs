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
    public class TeamAntwoordController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        // GET: TeamAntwoord
        public ActionResult Index()
        {
            var teamAntwoords = db.TeamAntwoords.Include(t => t.QuizVraag).Include(t => t.Team);
            return View(teamAntwoords.ToList());
        }

        // GET: TeamAntwoord/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamAntwoord teamAntwoord = db.TeamAntwoords.Find(id);
            if (teamAntwoord == null)
            {
                return HttpNotFound();
            }
            return View(teamAntwoord);
        }

        // GET: TeamAntwoord/Create
        public ActionResult Create()
        {
            ViewBag.QuizVraagID = new SelectList(db.QuizVraags, "QuizVraagID", "Thema_Naam");
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "Teamnaam");
            return View();
        }

        // POST: TeamAntwoord/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamAntwoordID,TeamID,QuizVraagID,Gegeven_Antwoord")] TeamAntwoord teamAntwoord)
        {
            if (ModelState.IsValid)
            {
                db.TeamAntwoords.Add(teamAntwoord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuizVraagID = new SelectList(db.QuizVraags, "QuizVraagID", "Thema_Naam", teamAntwoord.QuizVraagID);
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "Teamnaam", teamAntwoord.TeamID);
            return View(teamAntwoord);
        }

        // GET: TeamAntwoord/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamAntwoord teamAntwoord = db.TeamAntwoords.Find(id);
            if (teamAntwoord == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuizVraagID = new SelectList(db.QuizVraags, "QuizVraagID", "Thema_Naam", teamAntwoord.QuizVraagID);
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "Teamnaam", teamAntwoord.TeamID);
            return View(teamAntwoord);
        }

        // POST: TeamAntwoord/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamAntwoordID,TeamID,QuizVraagID,Gegeven_Antwoord")] TeamAntwoord teamAntwoord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamAntwoord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuizVraagID = new SelectList(db.QuizVraags, "QuizVraagID", "Thema_Naam", teamAntwoord.QuizVraagID);
            ViewBag.TeamID = new SelectList(db.Teams, "TeamID", "Teamnaam", teamAntwoord.TeamID);
            return View(teamAntwoord);
        }

        // GET: TeamAntwoord/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamAntwoord teamAntwoord = db.TeamAntwoords.Find(id);
            if (teamAntwoord == null)
            {
                return HttpNotFound();
            }
            return View(teamAntwoord);
        }

        // POST: TeamAntwoord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamAntwoord teamAntwoord = db.TeamAntwoords.Find(id);
            db.TeamAntwoords.Remove(teamAntwoord);
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
