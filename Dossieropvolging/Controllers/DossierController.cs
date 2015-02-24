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
            return View();
        }

        // POST: Dossier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titel,Inhoud,MeldingsDatum,OpstartDatum,AfsluitDatum,AlarmDatum,Besluit")] Dossier dossier)
        {
            if (ModelState.IsValid)
            {
                db.Dossiers.Add(dossier);
                db.SaveChanges();
                return RedirectToAction("Index");
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
