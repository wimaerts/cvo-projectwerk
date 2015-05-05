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
    [Authorize(Roles = "Admin")]
    public class StatusController : Controller
    {
        private DossieropvolgingContext db = new DossieropvolgingContext();

        // GET: Status
        public ActionResult Index()
        {
            return View(db.Statussen.ToList());
        }

        // GET: Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naam")] Status status)
        {
            if (ModelState.IsValid && !String.IsNullOrEmpty(status.Naam))
            {
                db.Statussen.Add(status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Gelieve het naam veld in te vullen alvorens te bewaren.");

            return View(status);
        }

        // GET: Status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status status = db.Statussen.Find(id);
            if (status == null)
            {
                return HttpNotFound();
            }
            return View(status);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naam")] Status status)
        {
            if (ModelState.IsValid && !String.IsNullOrEmpty(status.Naam))
            {
                db.Entry(status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            ModelState.AddModelError("", "Gelieve het naam veld in te vullen alvorens te bewaren.");

            return View(status);
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
