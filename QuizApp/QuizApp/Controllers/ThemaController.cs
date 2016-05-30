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
    public class ThemaController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        // GET: Thema
        public ActionResult Index()
        {
            return View(db.Thema.ToList());
        }

        // GET: Thema/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thema thema = db.Thema.Find(id);
            if (thema == null)
            {
                return HttpNotFound();
            }
            return View(thema);
        }

        // GET: Thema/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Thema/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Thema_Naam")] Thema thema)
        {
            var themanamen = from t in db.Thema
                             where t.Thema_Naam == thema.Thema_Naam
                             select t;
            if (themanamen.FirstOrDefault() == null)
            {

                if (ModelState.IsValid)
                {
                    db.Thema.Add(thema);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(thema);
                }
            }
            else
            {
                ViewBag.message = "Thema_naam bestaat al";
                ViewBag.linkText = "Terug naar thema";
                ViewBag.actionName = "index";
                ViewBag.routeValue = new { controller = "thema" };
                return View("Error");
            }
        }

        // GET: Thema/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thema thema = db.Thema.Find(id);
            if (thema == null)
            {
                return HttpNotFound();
            }
            return View(thema);
        }

        // POST: Thema/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ThemaID,Thema_Naam")] Thema thema)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thema).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thema);
        }

        // GET: Thema/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thema thema = db.Thema.Find(id);
            if (thema == null)
            {
                return HttpNotFound();
            }
            return View(thema);
        }

        // POST: Thema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thema thema = db.Thema.Find(id);
            db.Thema.Remove(thema);
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
