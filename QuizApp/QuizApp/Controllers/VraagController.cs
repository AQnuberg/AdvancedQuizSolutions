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
    public class VraagController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        // GET: Vraag
        public ActionResult Index()
        {
            var vraag = db.Vraag.Include(v => v.Thema);
            return View(vraag.ToList());
        }

        // GET: Vraag/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vraag vraag = db.Vraag.Find(id);
            if (vraag == null)
            {
                return HttpNotFound();
            }
            return View(vraag);
        }

        // GET: Vraag/Create
        public ActionResult Create()
        {
            List<SelectListItem> vraagtypes = new List<SelectListItem>();
            vraagtypes.Add(new SelectListItem { Text = "Open"});
            vraagtypes.Add(new SelectListItem { Text = "Meerkeuze"});
            ViewBag.Vraagtype = new SelectList(vraagtypes, "Text", "Text");
            ViewBag.ThemaNaam = new SelectList(db.Thema, "Naam", "Naam");
            return View();
        }

        // POST: Vraag/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VraagID,ThemaNaam,Vraag1,Vraagtype,AntwoordOpenVraag")] Vraag vraag)
        {
            if (ModelState.IsValid)
            {
                db.Vraag.Add(vraag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ThemaNaam = new SelectList(db.Thema, "Naam", "Naam", vraag.ThemaNaam);
            return View(vraag);
        }

        // GET: Vraag/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vraag vraag = db.Vraag.Find(id);
            if (vraag == null)
            {
                return HttpNotFound();
            }
            ViewBag.ThemaNaam = new SelectList(db.Thema, "Naam", "Naam", vraag.ThemaNaam);
            return View(vraag);
        }

        // POST: Vraag/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VraagID,ThemaNaam,Vraag1,Vraagtype,AntwoordOpenVraag")] Vraag vraag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vraag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ThemaNaam = new SelectList(db.Thema, "Naam", "Naam", vraag.ThemaNaam);
            return View(vraag);
        }

        // GET: Vraag/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vraag vraag = db.Vraag.Find(id);
            if (vraag == null)
            {
                return HttpNotFound();
            }
            return View(vraag);
        }

        // POST: Vraag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vraag vraag = db.Vraag.Find(id);
            db.Vraag.Remove(vraag);
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
