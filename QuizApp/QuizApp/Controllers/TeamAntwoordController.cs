using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuizApp.Models;
using System.Data.Entity.Migrations;

namespace QuizApp.Controllers
{
    public class TeamAntwoordController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();


        // GET: TeamAntwoord/Index/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var team = from e in db.Team
                       where e.TeamID == id
                       select e;

            var firstOrDefault = team.FirstOrDefault<Team>();
            if (firstOrDefault != null)
                ViewBag.naamBijID = firstOrDefault.Teamnaam;
            ViewBag.TeamID = id;

            var teamAntwoords = db.TeamAntwoord.Include(t => t.QuizVraag).Include(t => t.Team);
            return View(teamAntwoords);
        }

        // GET: TeamAntwoord/Details/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamAntwoord teamAntwoord = db.TeamAntwoord.Find(id);
            if (teamAntwoord == null)
            {
                return HttpNotFound();
            }
            return View(teamAntwoord);
        }

        // GET: TeamAntwoord/Create
        [Authorize(Roles = "Beheerder")]
        public ActionResult Create()
        {
            ViewBag.QuizVraagID = new SelectList(db.QuizVraag, "QuizVraagID", "Vraag");
            ViewBag.TeamID = new SelectList(db.Team, "TeamID", "Teamnaam");
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
                db.TeamAntwoord.Add(teamAntwoord);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = teamAntwoord.TeamID });
            }

            ViewBag.QuizVraagID = new SelectList(db.QuizVraag, "QuizVraagID", "Thema_Naam", teamAntwoord.QuizVraagID);
            ViewBag.TeamID = new SelectList(db.Team, "TeamID", "Teamnaam", teamAntwoord.TeamID);
            return View(teamAntwoord);
        }

        // GET: TeamAntwoord/Edit/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamAntwoord teamAntwoord = db.TeamAntwoord.Find(id);
            if (teamAntwoord == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuizVragen = new SelectList(db.QuizVraag, "QuizVraagID", "Vraag", teamAntwoord.QuizVraagID);
            ViewBag.Teams = new SelectList(db.Team, "TeamID", "Teamnaam", teamAntwoord.TeamID);
            return View(teamAntwoord);
        }

        // POST: TeamAntwoord/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult Edit([Bind(Include = "TeamAntwoordID,TeamID,QuizVraagID,Gegeven_Antwoord")] TeamAntwoord teamAntwoord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamAntwoord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = teamAntwoord.TeamID });
            }
            ViewBag.QuizVraagID = new SelectList(db.QuizVraag, "QuizVraagID", "Thema_Naam", teamAntwoord.QuizVraagID);
            ViewBag.TeamID = new SelectList(db.Team, "TeamID", "Teamnaam", teamAntwoord.TeamID);
            return View(teamAntwoord);
        }

        // GET: TeamAntwoord/Delete/5
        [Authorize(Roles = "Beheerder")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamAntwoord teamAntwoord = db.TeamAntwoord.Find(id);
            if (teamAntwoord == null)
            {
                return HttpNotFound();
            }
            return View(teamAntwoord);
        }

        // POST: TeamAntwoord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder")]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamAntwoord teamAntwoord = db.TeamAntwoord.Find(id);
            db.TeamAntwoord.Remove(teamAntwoord);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = teamAntwoord.TeamID });
        }

        public ActionResult Play(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var team = from e in db.Team
                       where e.TeamID == id
                       select e;

            ViewBag.team = team.FirstOrDefault();
            ViewBag.gegevenAntwoord = "Nog geen antwoord gegeven.";

            var vragen = from t in db.Team
                         join r in db.QuizRonde on t.EvenementID equals r.EvenementID
                         where t.TeamID == id
                         join vq in db.VraagInQuiz on r.QuizRondeID equals vq.QuizRondeID
                         where vq.isActief == true
                         join v in db.QuizVraag on vq.QuizVraagID equals v.QuizVraagID
                         select v;

            if(vragen.FirstOrDefault() == null)
            {
                ViewBag.message = "Deze quiz wordt nu niet gespeeld";
                ViewBag.linkText = "Terug naar team";
                ViewBag.actionName = "index";
                ViewBag.routeValue = new { controller = "team" };
                return View("Error");
            }

            var antwoord = from a in db.TeamAntwoord
                           where a.TeamID == id
                           where a.QuizVraagID == vragen.FirstOrDefault().QuizVraagID
                           select a;

            if(antwoord.FirstOrDefault() != null)
            {
                ViewBag.gegevenAntwoord = antwoord.FirstOrDefault().Gegeven_Antwoord;
                ViewBag.gegevenantwoordID = antwoord.FirstOrDefault().TeamAntwoordID;
            }
            else
            {
                ViewBag.gegevenantwoordID = 0;
            }

            if (vragen.FirstOrDefault().Vraagtype == "Meerkeuze")
            {
                var meerkeuzeVraagAntwoorden = from ma in db.MeerkeuzeAntwoord
                                               join v in db.QuizVraag on ma.QuizVraagID equals v.QuizVraagID
                                               where ma.QuizVraagID == vragen.FirstOrDefault().QuizVraagID
                                               select ma;

                var antwoorden = meerkeuzeVraagAntwoorden.Select(s => new
                {
                    Text = s.Meerkeuze_Antwoord,
                    Value = s.MeerkeuzeAntwoordID
                }).ToList();

                ViewBag.meerkeuzeAntwoorden = new SelectList(antwoorden, "Text", "Text");
            }

            ViewBag.vraag = vragen.FirstOrDefault();

            return View();
        }

        // POST: TeamAntwoord/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Play([Bind(Include = "TeamAntwoordID,TeamID,QuizVraagID,Gegeven_Antwoord")] TeamAntwoord teamAntwoord)
        {
            if (teamAntwoord.Gegeven_Antwoord == null)
                teamAntwoord.Gegeven_Antwoord = "";

            var actieveVraag = from v in db.VraagInQuiz
                               join qr in db.QuizRonde on v.QuizRondeID equals qr.QuizRondeID
                               join t in db.Team on qr.EvenementID equals t.EvenementID
                               where t.TeamID == teamAntwoord.TeamID
                               where v.isActief == true
                               select v;

            if(actieveVraag.FirstOrDefault().QuizVraagID != teamAntwoord.QuizVraagID)
                return RedirectToAction("Play", new { id = teamAntwoord.TeamID });

            var antwoord = from t in db.TeamAntwoord
                           where t.QuizVraagID == teamAntwoord.QuizVraagID
                           where t.TeamID == teamAntwoord.TeamID
                           select t;

            if(ModelState.IsValid && antwoord.FirstOrDefault() != null)
            {
                db.Set<TeamAntwoord>().AddOrUpdate(teamAntwoord);
//                db.Entry(teamAntwoord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Play", new { id = teamAntwoord.TeamID });
            }


            if (ModelState.IsValid)
            {
                db.TeamAntwoord.Add(teamAntwoord);
                db.SaveChanges();
                return RedirectToAction("Play", new { id = teamAntwoord.TeamID });
            }

            ViewBag.QuizVraagID = new SelectList(db.QuizVraag, "QuizVraagID", "Thema_Naam", teamAntwoord.QuizVraagID);
            ViewBag.TeamID = new SelectList(db.Team, "TeamID", "Teamnaam", teamAntwoord.TeamID);
            return View(teamAntwoord);
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
