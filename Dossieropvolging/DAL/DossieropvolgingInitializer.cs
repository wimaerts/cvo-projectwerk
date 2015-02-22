using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dossieropvolging.Models;

namespace Dossieropvolging.DAL
{
    public class DossieropvolgingInitializer : DropCreateDatabaseAlways<DossieropvolgingContext>
    {
        protected override void Seed(DossieropvolgingContext context)
        {
            base.Seed(context);

            Status s = new Status()
            {
                Naam = "Nieuw"
            };

            Prioriteit p = new Prioriteit()
            {
                Naam = "Normaal"
            };

            Dossier d = new Dossier()
            {
                Titel = "TestDossier",
                Inhoud = "Test",
                Terkenniskoming = "Telefoon",
                OpstartDatum = DateTime.Now,
                MeldingsDatum = DateTime.Now,
                Prioriteit = p,
                Status = s
            };

            context.Dossiers.Add(d);
            context.SaveChanges();
        }
    }
}
