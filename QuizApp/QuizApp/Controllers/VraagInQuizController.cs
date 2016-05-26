using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using QuizApp.Models;

namespace QuizApp.Controllers
{
    public class VraagInQuizController : Controller
    {
        private AQSDatabaseEntities db = new AQSDatabaseEntities();

        // GET: VraagInQuiz
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var evenement = from e in db.Evenementen
                join q in db.QuizRondes on e.EvenementID equals q.EvenementID
                where q.QuizRondeID == id
                select e;

            var quizRonde = from r in db.QuizRondes
                where r.QuizRondeID == id
                select r;

            var vraagInRonde = from e in db.VraagInQuizzen
                                    select e;
            vraagInRonde = vraagInRonde.Where(s => s.QuizRondeID == id);

            ViewBag.naamBijQuizRondeID = evenement.First().Evenement_Naam;
            ViewBag.rondeNummer = quizRonde.First().Rondenummer;
            ViewBag.themaNaam = quizRonde.First().Thema_Naam;
            ViewBag.evenementID = evenement.First().EvenementID;
            ViewBag.quizRondeID = id; 

            var vraagInQuizs = vraagInRonde.Include(v => v.QuizRonde).Include(v => v.QuizVraag);
            return View(vraagInQuizs.ToList());
        }

        // GET: VraagInQuiz/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            /* In case of surrogate keys:
               var themaNaam = from t in db.Themas
                                join q in db.QuizRondes on t.themaId equals q.themaId
                                where q.QuizRondeID == id
                                select t.Thema_Naam;*/

            var quizRonde = from r in db.QuizRondes
                            where r.QuizRondeID == id
                            select r;

            var vragenBijThema = from v in db.QuizVragen
                where v.Thema_Naam == quizRonde.FirstOrDefault().Thema_Naam 
                select v;

            ViewBag.QuizRondeID = id;
            ViewBag.VragenBijThema = vragenBijThema;
            return View();
        }

        // POST: VraagInQuiz/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VraagInQuizID,QuizRondeID,QuizVraagID")] VraagInQuiz vraagInQuiz)
        {
            if (ModelState.IsValid)
            {
                db.VraagInQuizzen.Add(vraagInQuiz);
                db.SaveChanges();
                return RedirectToAction("Index", new {id = vraagInQuiz.QuizRondeID});
            }

            ViewBag.QuizRondeID = new SelectList(db.QuizRondes, "QuizRondeID", "Thema_Naam", vraagInQuiz.QuizRondeID);
            ViewBag.QuizVraagID = new SelectList(db.QuizVragen, "QuizVraagID", "Thema_Naam", vraagInQuiz.QuizVraagID);
            return View(vraagInQuiz);
        }

        // GET: VraagInQuiz/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VraagInQuiz vraagInQuiz = db.VraagInQuizzen.Find(id);
            if (vraagInQuiz == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuizRondeID = new SelectList(db.QuizRondes, "QuizRondeID", "Thema_Naam", vraagInQuiz.QuizRondeID);
            ViewBag.QuizVraagID = new SelectList(db.QuizVragen, "QuizVraagID", "Thema_Naam", vraagInQuiz.QuizVraagID);
            return View(vraagInQuiz);
        }

        // POST: VraagInQuiz/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VraagInQuizID,QuizRondeID,QuizVraagID")] VraagInQuiz vraagInQuiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vraagInQuiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuizRondeID = new SelectList(db.QuizRondes, "QuizRondeID", "Thema_Naam", vraagInQuiz.QuizRondeID);
            ViewBag.QuizVraagID = new SelectList(db.QuizVragen, "QuizVraagID", "Thema_Naam", vraagInQuiz.QuizVraagID);
            return View(vraagInQuiz);
        }

        // GET: VraagInQuiz/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VraagInQuiz vraagInQuiz = db.VraagInQuizzen.Find(id);
            if (vraagInQuiz == null)
            {
                return HttpNotFound();
            }
            return View(vraagInQuiz);
        }

        // POST: VraagInQuiz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VraagInQuiz vraagInQuiz = db.VraagInQuizzen.Find(id);
            db.VraagInQuizzen.Remove(vraagInQuiz);
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
