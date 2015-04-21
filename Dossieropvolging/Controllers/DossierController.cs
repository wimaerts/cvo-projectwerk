using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dossieropvolging.DAL;
using Dossieropvolging.Models;
using System.Data.Entity.Infrastructure;
using Dossieropvolging.ViewModels;
using System.Web.Security;

namespace Dossieropvolging.Controllers
{
    public class DossierController : Controller
    {
        private DossieropvolgingContext db = new DossieropvolgingContext();

        // GET: Dossier
        public ActionResult Index()
        {
            return View(db.Dossiers.ToList());
        }

        // GET: Dossier/Details/5
        public ActionResult Details(int? id)
        {
            var dossierViewModel = DossierViewModelAanmaken();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dossier dossier = db.Dossiers.Find(id);
            if (dossier == null)
            {
                return HttpNotFound();
            }

            dossierViewModel.Dossier = dossier;
            return View(dossierViewModel);
        }

        // GET: Dossier/Create
        public ActionResult Create()
        {
            var dossierViewModel = DossierViewModelAanmaken();

            return View(dossierViewModel);
        }


        // POST: Dossier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dossier dossier)
        {
            dossier.OpstartDatum = DateTime.Now;
            dossier.Status = db.Statussen.Single(s => s.Id == dossier.Status.Id);
            dossier.Terkenniskoming = db.Terkenniskomingen.Single(t => t.Id == dossier.Terkenniskoming.Id);
            dossier.Prioriteit = db.Prioriteiten.Single(p => p.Id == dossier.Prioriteit.Id);
            dossier.Kwalificatie = db.Kwalificaties.Single(k => k.Id == dossier.Kwalificatie.Id);

            try
            {
                if (ModelState.IsValid)
                {
                    db.Dossiers.Add(dossier);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Het was niet mogelijk om de wijzigingen te bewaren!");
            }

            return View(dossier);
        }

        // GET: Dossier/Bijlage/5
        public ActionResult Bijlage(int? id)
        {
            var dossierViewModel = DossierViewModelAanmaken();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dossier dossier = db.Dossiers.Find(id);
            if (dossier == null)
            {
                return HttpNotFound();
            }

            dossierViewModel.Dossier = dossier;
            return View(dossierViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bijlage(HttpPostedFileBase upload, Dossier dossier)
        {
            var dbDossier = db.Dossiers.Single(d => d.Id == dossier.Id);

            if (upload != null && upload.ContentLength > 0)
            {
                var bijlage = new Bijlage
                {
                    Naam = System.IO.Path.GetFileName(upload.FileName)
                };
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    bijlage.Inhoud = reader.ReadBytes(upload.ContentLength);
                }

                dbDossier.Bijlages.Add(bijlage);

                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                    return RedirectToAction("Bijlage");
                }
            }

            return View(dbDossier);
        }

        // GET: Dossier/Actie/5
        public ActionResult Actie(int? id)
        {
            var dossierViewModel = DossierViewModelAanmaken();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dossier dossier = db.Dossiers.Find(id);
            if (dossier == null)
            {
                return HttpNotFound();
            }

            dossierViewModel.Dossier = dossier;
            return View(dossierViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Actie(Actie actie, Dossier dossier)
        {
            var dbDossier = db.Dossiers.Single(d => d.Id == dossier.Id);

            Actie nieuweActie = new Actie();

            nieuweActie.Inhoud = actie.Inhoud;
            nieuweActie.ActieDatum = DateTime.Now;

            dbDossier.Acties.Add(nieuweActie);

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Actie");
            }

            return View(dbDossier);
        }

        // GET: Dossier/Edit/5
        public ActionResult Edit(int? id)
        {
            var dossierViewModel = DossierViewModelAanmaken();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dossier dossier = db.Dossiers.Find(id);
            if (dossier == null)
            {
                return HttpNotFound();
            }

            dossierViewModel.Dossier = dossier;
            return View(dossierViewModel);
        }

        // POST: Dossier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dossier dossier)
        {
            var dbDossier = db.Dossiers.Single(d => d.Id == dossier.Id);

            dbDossier.OpstartDatum = dossier.OpstartDatum;
            dbDossier.Titel = dossier.Titel;
            dbDossier.Inhoud = dossier.Inhoud;
            dbDossier.MeldingsDatum = dossier.MeldingsDatum;
            dbDossier.AlarmDatum = dossier.AlarmDatum;
            dbDossier.Besluit = dossier.Besluit;
            dbDossier.Dossierbeheerder = dossier.Dossierbeheerder;
            dbDossier.Status = db.Statussen.Single(s => s.Id == dossier.Status.Id);
            dbDossier.Terkenniskoming = db.Terkenniskomingen.Single(t => t.Id == dossier.Terkenniskoming.Id);
            dbDossier.Prioriteit = db.Prioriteiten.Single(p => p.Id == dossier.Prioriteit.Id);
            dbDossier.Kwalificatie = db.Kwalificaties.Single(k => k.Id == dossier.Kwalificatie.Id);

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dbDossier);
        }

        // GET: Dossier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dossier dossier = db.Dossiers.Find(id);
            if (dossier == null)
            {
                return HttpNotFound();
            }
            return View(dossier);
        }

        // POST: Dossier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dossier dossier = db.Dossiers.Find(id);
            db.Dossiers.Remove(dossier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Hulpmethode om viewmodel aan te maken
        private DossierViewModel DossierViewModelAanmaken()
        {
            var dossierViewModel = new DossierViewModel();

            var statusQry = from s in db.Statussen
                            orderby s.Naam
                            select s;

            var terkenniskomingQry = from t in db.Terkenniskomingen
                                     orderby t.Naam
                                     select t;

            var prioriteitQry = from p in db.Prioriteiten
                                orderby p.Naam
                                select p;

            var kwalificatieQry = from k in db.Kwalificaties
                                  orderby k.Naam
                                  select k;

            var gebruikersContext = new ApplicationDbContext();


            dossierViewModel.lstStatus = statusQry.ToList();
            dossierViewModel.lstTerkenniskoming = terkenniskomingQry.ToList();
            dossierViewModel.lstPrioriteit = prioriteitQry.ToList();
            dossierViewModel.lstKwalificatie = kwalificatieQry.ToList();
            dossierViewModel.lstGebruikers = gebruikersContext.Users.ToList();

            return dossierViewModel;
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
