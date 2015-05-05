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
using Microsoft.AspNet.Identity;

namespace Dossieropvolging.Controllers
{
    [Authorize(Roles = "Admin, Gebruiker")]
    public class DossierController : Controller
    {
        private DossieropvolgingContext db = new DossieropvolgingContext();

        // GET: Dossier
        public ActionResult Index()
        {
            // Alleen dossiers ophalen die nog niet werden afgesloten
            var dossierQry = db.Dossiers.Where(d => d.Status.Naam != "Afgesloten");
            List<Dossier> dossiers = dossierQry.ToList();

            // Nakijken welke dossiers over hun alarmdatum zitten
            Dossier.AlarmDatumControle(dossiers);

            return View(dossiers.OrderBy(d => d.MeldingsDatum));
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

        // GET: Opzoeken
        public ActionResult Opzoeken()
        {
            var dossierViewModel = DossierViewModelAanmaken();
            return View(dossierViewModel);
        }

        // POST: Opzoeken
        [HttpPost]
        public ActionResult Opzoeken(Dossier dossier, string ZoekMeldingsDatum1, string ZoekMeldingsDatum2, string DossierId, string StatusId, string PrioriteitId)
        {
            IEnumerable<Dossier> gevondenDossiersQry = db.Dossiers.ToList();

            // Nakijken of dossier nummer wordt gevonden
            if (!String.IsNullOrEmpty(DossierId))
            {
                try
                {
                    int dossierId = Convert.ToInt32(DossierId);
                    gevondenDossiersQry = gevondenDossiersQry.Where(d => d.Id == dossierId);
                }
                catch(Exception)
                {
                    ModelState.AddModelError("", "Dossier nummer moet een numerieke waarde zijn.");
                }                
            }

            // Nakijken welke dossiers de gekozen status hebben
            if (!String.IsNullOrEmpty(StatusId))
            {
                int statusId = Convert.ToInt32(StatusId);
                gevondenDossiersQry = gevondenDossiersQry.Where(d => d.Status.Id == statusId);
            }

            // Nakijken welke dossiers de gekozen prioriteit hebben
            if (!String.IsNullOrEmpty(PrioriteitId))
            {
                int prioriteitId = Convert.ToInt32(PrioriteitId);
                gevondenDossiersQry = gevondenDossiersQry.Where(d => d.Prioriteit.Id == prioriteitId);
            }

            // Nakijken welke dossiers in aanmerking komen op basis van de 2 opgegeven datums
            if (!String.IsNullOrEmpty(ZoekMeldingsDatum1) && !String.IsNullOrEmpty(ZoekMeldingsDatum2))
            {
                DateTime zoekMeldingsDatum1 = DateTime.Parse(ZoekMeldingsDatum1);
                DateTime zoekMeldingsDatum2 = DateTime.Parse(ZoekMeldingsDatum2);

                gevondenDossiersQry = gevondenDossiersQry.Where(d => d.MeldingsDatum >= zoekMeldingsDatum1 && d.MeldingsDatum <= zoekMeldingsDatum2);
            }

            // Daarna nakijken of in die gevonden dossiers de ingegeven melder voorkomt
            if (!String.IsNullOrEmpty(dossier.Melder))
            {
                gevondenDossiersQry = gevondenDossiersQry.Where(d => d.Melder.ToLower().Contains(dossier.Melder.ToLower()));
            }

            // Daarna nakijken of in die gevonden dossiers de ingegeven titel voorkomt
            if (!String.IsNullOrEmpty(dossier.Titel))
            {
                gevondenDossiersQry = gevondenDossiersQry.Where(d => d.Titel.ToLower().Contains(dossier.Titel.ToLower()));
            }

            // Daarna nakijken of in de nu nog resterende dossiers de inhoud overen komt
            if (!String.IsNullOrEmpty(dossier.Inhoud))
            {
                gevondenDossiersQry = gevondenDossiersQry.Where(d => d.Inhoud.ToLower().Contains(dossier.Inhoud.ToLower()));
            }

            var dossierViewModel = DossierViewModelAanmaken();
            dossierViewModel.GevondenDossiers = gevondenDossiersQry.ToList();

            return View(dossierViewModel);
        }

        // GET: Dossier/Create
        public ActionResult Create()
        {
            var dossierViewModel = DossierViewModelAanmaken();
            dossierViewModel.Dossier = new Dossier();

            // Voor een nieuw dossier zetten we de meldingsdatum standaard op vandaag            
            dossierViewModel.Dossier.MeldingsDatum = DateTime.Now;

            // voor een nieuw dossier zetten we de standaard prioriteit op normaal
            dossierViewModel.Dossier.Prioriteit = db.Prioriteiten.Single(p => p.Naam.Equals("Normaal"));

            // voor een nieuw dossier zetten we de standaard terkenniskoming op e-mail
            dossierViewModel.Dossier.Terkenniskoming = db.Terkenniskomingen.Single(p => p.Naam.Equals("E-mail"));

            // voor een nieuw dossier zetten we de standaard alarm datum op +30 dagen
            dossierViewModel.Dossier.AlarmDatum = DateTime.Now.AddDays(30);

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

            var gebruikersContext = new ApplicationDbContext();
            dossier.DossierbeheerderNaam = gebruikersContext.Users.Find(dossier.Dossierbeheerder).VolledigeNaam;

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

        // POST: Bijlage Toevoegen
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Bijlage(HttpPostedFileBase upload, Dossier dossier)
        {
            var dbDossier = db.Dossiers.Single(d => d.Id == dossier.Id);

            // Als er een bestand werd toegevoegd met inhoud gaan we dat proberen toe te voegen.
            if (upload != null && upload.ContentLength > 0)
            {
                var bijlage = new Bijlage
                {
                    Naam = System.IO.Path.GetFileName(upload.FileName),
                    ToegevoegdOp = DateTime.Now
                };
                using (var reader = new System.IO.BinaryReader(upload.InputStream))
                {
                    bijlage.Inhoud = reader.ReadBytes(upload.ContentLength);
                }

                dbDossier.Bijlages.Add(bijlage);

                if (ModelState.IsValid)
                {
                    db.SaveChanges();
                }
            }
            else
            {
                // Als er geen geldig bestand werd geselecteerd sturen we een foutboodschap terug
                ModelState.AddModelError("", "U moet een bestand selecteren!");
            }

            var dossierViewModel = DossierViewModelAanmaken();
            dossierViewModel.Dossier = dbDossier;

            return View(dossierViewModel);         
        }

        // GET: Bijlage Verwijderen
        public ActionResult DeleteBijlage(int? id, int? dossierId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Bijlage bijlage = db.Bijlages.Find(id);

            if (bijlage == null)
            {
                return HttpNotFound();
            }

            // Bijlage verwijderen
            db.Bijlages.Remove(bijlage);
            db.SaveChanges();

            var dossierViewModel = DossierViewModelAanmaken();            

            if (dossierId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dossier dossier = db.Dossiers.Find(dossierId);
            if (dossier == null)
            {
                return HttpNotFound();
            }

            dossierViewModel.Dossier = dossier;
            return View("Bijlage", dossierViewModel);
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

        // POST: Actie
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

            var gebruikersContext = new ApplicationDbContext();
            dbDossier.DossierbeheerderNaam = gebruikersContext.Users.Find(dossier.Dossierbeheerder).VolledigeNaam;

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

        // Hulpmethode om dossier viewmodel aan te maken
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
