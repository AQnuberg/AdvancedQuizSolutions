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
    public class MeerkeuzeAntwoordsController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        // GET: MeerkeuzeAntwoords
        public ActionResult Index(int id)
        {
            var antwoord = from a in db.MeerkeuzeAntwoord
                           select a;

            antwoord = antwoord.Where(s => s.VraagID == id);

            var meerkeuzeAntwoord = antwoord.Include(m => m.Vraag);
            return View(meerkeuzeAntwoord.ToList());
        }

        // GET: MeerkeuzeAntwoords/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeerkeuzeAntwoord meerkeuzeAntwoord = db.MeerkeuzeAntwoord.Find(id);
            if (meerkeuzeAntwoord == null)
            {
                return HttpNotFound();
            }
            return View(meerkeuzeAntwoord);
        }

        // GET: MeerkeuzeAntwoords/Create
        public ActionResult Create()
        {
            ViewBag.VraagID = new SelectList(db.Vraag, "VraagID", "ThemaNaam");
            return View();
        }

        // POST: MeerkeuzeAntwoords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeerkeuzeAntwoord1,VraagID,isJuist")] MeerkeuzeAntwoord meerkeuzeAntwoord)
        {
            if (ModelState.IsValid)
            {
                db.MeerkeuzeAntwoord.Add(meerkeuzeAntwoord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VraagID = new SelectList(db.Vraag, "VraagID", "ThemaNaam", meerkeuzeAntwoord.VraagID);
            return View(meerkeuzeAntwoord);
        }

        // GET: MeerkeuzeAntwoords/Edit/5
        public ActionResult Edit(string id, string antwoord)
        {
            if (antwoord == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeerkeuzeAntwoord meerkeuzeAntwoord = db.MeerkeuzeAntwoord.Find(id, antwoord);
            if (meerkeuzeAntwoord == null)
            {
                return HttpNotFound();
            }
            ViewBag.VraagID = new SelectList(db.Vraag, "VraagID", "ThemaNaam", meerkeuzeAntwoord.VraagID);
            return View(meerkeuzeAntwoord);
        }

        // POST: MeerkeuzeAntwoords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MeerkeuzeAntwoord1,VraagID,isJuist")] MeerkeuzeAntwoord meerkeuzeAntwoord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meerkeuzeAntwoord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VraagID = new SelectList(db.Vraag, "VraagID", "ThemaNaam", meerkeuzeAntwoord.VraagID);
            return View(meerkeuzeAntwoord);
        }

        // GET: MeerkeuzeAntwoords/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeerkeuzeAntwoord meerkeuzeAntwoord = db.MeerkeuzeAntwoord.Find(id);
            if (meerkeuzeAntwoord == null)
            {
                return HttpNotFound();
            }
            return View(meerkeuzeAntwoord);
        }

        // POST: MeerkeuzeAntwoords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MeerkeuzeAntwoord meerkeuzeAntwoord = db.MeerkeuzeAntwoord.Find(id);
            db.MeerkeuzeAntwoord.Remove(meerkeuzeAntwoord);
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
