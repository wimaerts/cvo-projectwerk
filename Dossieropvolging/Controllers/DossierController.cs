﻿using System;
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
            PopulateStatusLijst();
            return View();
        }

        // POST: Dossier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string titel, string inhoud, DateTime meldingsDatum, DateTime? alarmDatum, int statusId)
        {
            var status = db.Statussen.Where(s => s.Id == statusId);

            Dossier dossier = new Dossier()
            {
                Titel = titel,
                Inhoud = inhoud,
                MeldingsDatum = meldingsDatum,
                AlarmDatum = alarmDatum,
                Status = status.ToList().FirstOrDefault(),
                OpstartDatum = DateTime.Now
            };            

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
        private void PopulateStatusLijst(object selectedStatus = null)
        {
            var statusQuery = from s in db.Statussen
                              orderby s.Naam
                              select s;

            ViewBag.StatusLijst = new SelectList(statusQuery, "Id", "Naam", selectedStatus);
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
