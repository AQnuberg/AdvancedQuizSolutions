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
            var teams = db.Teams.Include(t => t.Account).Include(t => t.Evenement);
            return View(teams.ToList());
        }

        // GET: Team/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            ViewBag.Email_Teamleider = new SelectList(db.Accounts, "Email", "Rolnaam");
            ViewBag.EvenementID = new SelectList(db.Evenements, "EvenementID", "Email_Quizmaster");
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamID,EvenementID,Teamnaam,Email_Teamleider,Puntentotaal")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Email_Teamleider = new SelectList(db.Accounts, "Email", "Rolnaam", team.Email_Teamleider);
            ViewBag.EvenementID = new SelectList(db.Evenements, "EvenementID", "Email_Quizmaster", team.EvenementID);
            return View(team);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            ViewBag.Email_Teamleider = new SelectList(db.Accounts, "Email", "Rolnaam", team.Email_Teamleider);
            ViewBag.EvenementID = new SelectList(db.Evenements, "EvenementID", "Email_Quizmaster", team.EvenementID);
            return View(team);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamID,EvenementID,Teamnaam,Email_Teamleider,Puntentotaal")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Email_Teamleider = new SelectList(db.Accounts, "Email", "Rolnaam", team.Email_Teamleider);
            ViewBag.EvenementID = new SelectList(db.Evenements, "EvenementID", "Email_Quizmaster", team.EvenementID);
            return View(team);
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
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
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
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
