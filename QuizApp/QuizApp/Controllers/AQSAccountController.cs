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
            var accounts = db.Accounts.Include(a => a.Rol);
            return View(accounts.ToList());
        }

        // GET: AQSAccount/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: AQSAccount/Create
        public ActionResult Create()
        {
            ViewBag.Rolnaam = new SelectList(db.Rols, "Rolnaam", "Rolnaam");
            return View();
        }

        // POST: AQSAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,Rolnaam,Wachtwoord,Voornaam,Achternaam,Woonplaats,Straatnaam,Huisnummer,Postcode,Telefoonnummer")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Rolnaam = new SelectList(db.Rols, "Rolnaam", "Rolnaam", account.Rolnaam);
            return View(account);
        }

        // GET: AQSAccount/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rolnaam = new SelectList(db.Rols, "Rolnaam", "Rolnaam", account.Rolnaam);
            return View(account);
        }

        // POST: AQSAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Email,Rolnaam,Wachtwoord,Voornaam,Achternaam,Woonplaats,Straatnaam,Huisnummer,Postcode,Telefoonnummer")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Rolnaam = new SelectList(db.Rols, "Rolnaam", "Rolnaam", account.Rolnaam);
            return View(account);
        }

        // GET: AQSAccount/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: AQSAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
