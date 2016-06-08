using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class PuntenTotaalController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenement evenement = db.Evenement.Find(id);
            var teams = from t in db.Team
                        where t.EvenementID == evenement.EvenementID
                        select t.TeamID;

            List<int> teamIDs = new List<int>();
            foreach (var t in teams)
            {
                teamIDs.Add(t);
            }
            
            foreach (int i in teamIDs)
            {
                db.SP_Team_PuntenTotaal(i);
                db.SaveChanges();
            }

            var teamsBag = from t in db.Team
                        where t.EvenementID == evenement.EvenementID
                        select t;

            ViewBag.evenementNaam = evenement.Evenement_Naam;
            return View(teamsBag.ToList());
        }
    }
}