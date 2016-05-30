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
    public class AQSAccountController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        // GET: AQSAccount
        public ActionResult Index()
        {
            var accounts = db.Account.Include(a => a.Rol);
            return View(accounts.ToList());
        }

        // GET: AQSAccount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: AQSAccount/Create
        public ActionResult Create()
        {
            ViewBag.Rollen = new SelectList(db.Rol, "RolID", "Rolnaam");
            return View();
        }

        // POST: AQSAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,RolID,Wachtwoord,Voornaam,Achternaam,Woonplaats,Straatnaam,Huisnummer,Postcode,Telefoonnummer")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Account.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Rolnaam = new SelectList(db.Rol, "Rolnaam", "Rolnaam", account.RolID);
            return View(account);
        }

        // GET: AQSAccount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rollen = new SelectList(db.Rol, "RolID", "Rolnaam", account.RolID);
            return View(account);
        }

        // POST: AQSAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountID,Email,RolID,Wachtwoord,Voornaam,Achternaam,Woonplaats,Straatnaam,Huisnummer,Postcode,Telefoonnummer")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Rolnaam = new SelectList(db.Rol, "RolID", "Rolnaam", account.RolID);
            return View(account);
        }

        // GET: AQSAccount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Account.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: AQSAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Account.Find(id);
            db.Account.Remove(account);
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
