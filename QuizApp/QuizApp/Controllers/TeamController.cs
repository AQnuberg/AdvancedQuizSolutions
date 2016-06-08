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
    public class TeamController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        // GET: Team 
        public ActionResult Index()
        {
            var teams = db.Team.Include(t => t.Account).Include(t => t.Evenement);
            return View(teams.ToList());
        }

        // GET: Team/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Team.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            ViewBag.accounts = new SelectList(db.Account, "AccountID", "email");
            ViewBag.evenementen = new SelectList(db.Evenement, "EvenementID", "Evenement_Naam");
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EvenementID,Teamnaam,AccountID,Puntentotaal")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.SP_Team_Toevoegen(team.EvenementID, team.Teamnaam, (int?)team.AccountID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Email_Teamleider = new SelectList(db.Account, "Email", "Rolnaam", team.AccountID);
            ViewBag.EvenementID = new SelectList(db.Evenement, "EvenementID", "Email_Quizmaster", team.EvenementID);
            return View(team);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Team.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            ViewBag.Accounts = new SelectList(db.Account, "accountID", "email", team.AccountID);
            return View(team);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamID,EvenementID,Teamnaam,AccountID,Puntentotaal")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Email_Teamleider = new SelectList(db.Account, "Email", "Rolnaam", team.AccountID);
            ViewBag.EvenementID = new SelectList(db.Evenement, "EvenementID", "Email_Quizmaster", team.EvenementID);
            return View(team);
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Team.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Team.Find(id);
            db.Team.Remove(team);
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
