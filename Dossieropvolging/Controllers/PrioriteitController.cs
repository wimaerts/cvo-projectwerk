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
    public class PrioriteitController : Controller
    {
        private DossieropvolgingContext db = new DossieropvolgingContext();

        // GET: Prioriteit
        public ActionResult Index()
        {
            return View(db.Prioriteiten.ToList());
        }

        // GET: Prioriteit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prioriteit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naam")] Prioriteit Prioriteit)
        {
            if (ModelState.IsValid && !String.IsNullOrEmpty(Prioriteit.Naam))
            {
                db.Prioriteiten.Add(Prioriteit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Gelieve het naam veld in te vullen alvorens te bewaren.");

            return View(Prioriteit);
        }

        // GET: Prioriteit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prioriteit Prioriteit = db.Prioriteiten.Find(id);
            if (Prioriteit == null)
            {
                return HttpNotFound();
            }
            return View(Prioriteit);
        }

        // POST: Prioriteit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naam")] Prioriteit Prioriteit)
        {
            if (ModelState.IsValid && !String.IsNullOrEmpty(Prioriteit.Naam))
            {
                db.Entry(Prioriteit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Gelieve het naam veld in te vullen alvorens te bewaren.");

            return View(Prioriteit);
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
