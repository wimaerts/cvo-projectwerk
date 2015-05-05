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
    public class TerkenniskomingController : Controller
    {
        private DossieropvolgingContext db = new DossieropvolgingContext();

        // GET: Terkenniskoming
        public ActionResult Index()
        {
            return View(db.Terkenniskomingen.ToList());
        }

        // GET: Terkenniskoming/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Terkenniskoming/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naam")] Terkenniskoming Terkenniskoming)
        {
            if (ModelState.IsValid && !String.IsNullOrEmpty(Terkenniskoming.Naam))
            {
                db.Terkenniskomingen.Add(Terkenniskoming);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Gelieve het naam veld in te vullen alvorens te bewaren.");

            return View(Terkenniskoming);
        }

        // GET: Terkenniskoming/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Terkenniskoming Terkenniskoming = db.Terkenniskomingen.Find(id);
            if (Terkenniskoming == null)
            {
                return HttpNotFound();
            }
            return View(Terkenniskoming);
        }

        // POST: Terkenniskoming/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naam")] Terkenniskoming Terkenniskoming)
        {
            if (ModelState.IsValid && !String.IsNullOrEmpty(Terkenniskoming.Naam))
            {
                db.Entry(Terkenniskoming).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Gelieve het naam veld in te vullen alvorens te bewaren.");

            return View(Terkenniskoming);
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
