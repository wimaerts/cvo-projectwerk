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

        // GET: Dossier/Create
        public ActionResult Create()
        {
            //PopulateStatusLijst();

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

            dossierViewModel.lstStatus = statusQry.ToList();
            dossierViewModel.lstTerkenniskoming = terkenniskomingQry.ToList();
            dossierViewModel.lstPrioriteit = prioriteitQry.ToList();
            dossierViewModel.lstKwalificatie = kwalificatieQry.ToList();

            return View(dossierViewModel);
        }

        // POST: Dossier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titel,Inhoud,MeldingsDatum,AfsluitDatum,AlarmDatum,Besluit,Status,Terkenniskoming,Prioriteit,Kwalificatie")] Dossier dossier)
        {
            var status = db.Statussen.Where(s => s.Id == dossier.Status.Id);
            var terkenniskoming = db.Terkenniskomingen.Where(t => t.Id == dossier.Terkenniskoming.Id);
            var prioriteit = db.Prioriteiten.Where(p => p.Id == dossier.Prioriteit.Id);
            var kwalificatie = db.Kwalificaties.Where(k => k.Id == dossier.Kwalificatie.Id);

            dossier.Status = status.First();
            dossier.Terkenniskoming = terkenniskoming.First();
            dossier.Prioriteit = prioriteit.First();
            dossier.Kwalificatie = kwalificatie.First();
            dossier.OpstartDatum = DateTime.Now;

            

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

        // GET: Dossier/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Dossier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titel,Inhoud,MeldingsDatum,OpstartDatum,AfsluitDatum,AlarmDatum,Besluit")] Dossier dossier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dossier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dossier);
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

        // Opvullen Status lijst
        //private void PopulateStatusLijst(object selectedStatus = null)
        //{
        //    var statusQuery = from s in db.Statussen
        //                      orderby s.Naam
        //                      select s;

        //    ViewBag.StatusLijst = new SelectList(statusQuery, "Id", "Naam", selectedStatus);
        //}

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
