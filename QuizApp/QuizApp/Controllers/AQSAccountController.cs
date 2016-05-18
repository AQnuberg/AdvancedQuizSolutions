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
            var account = db.Account.Include(a => a.Rol1);
            return View(account.ToList());
        }

        // GET: AQSAccount/Details/5
        public ActionResult Details(string id)
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
            ViewBag.Rol = new SelectList(db.Rol, "Rol1", "Rol1");
            return View();
        }

        // POST: AQSAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Email,Rol,Wachtwoord,Voornaam,Intitialen,Achternaam,Woonplaats,Straat,Huisnummer,Postcode,Telefoonnummer")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Account.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Rol = new SelectList(db.Rol, "Rol1", "Rol1", account.Rol);
            return View(account);
        }

        // GET: AQSAccount/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.Rol = new SelectList(db.Rol, "Rol1", "Rol1", account.Rol);
            return View(account);
        }

        // POST: AQSAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Email,Rol,Wachtwoord,Voornaam,Intitialen,Achternaam,Woonplaats,Straat,Huisnummer,Postcode,Telefoonnummer")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Rol = new SelectList(db.Rol, "Rol1", "Rol1", account.Rol);
            return View(account);
        }

        // GET: AQSAccount/Delete/5
        public ActionResult Delete(string id)
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
        public ActionResult DeleteConfirmed(string id)
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
