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

            #region seed status
            Status statusNieuw = new Status("Nieuw");
            Status statusActief = new Status("Actief");
            Status statusBevroren= new Status("Bevroren");
            Status statusAfgesloten= new Status("Afgesloten");
            context.Statussen.Add(statusNieuw);
            context.Statussen.Add(statusActief);
            context.Statussen.Add(statusBevroren);
            context.Statussen.Add(statusAfgesloten);
            context.SaveChanges();
            #endregion

            #region seed prioriteit
            Prioriteit prioriteitLaag = new Prioriteit("Laag");
            Prioriteit prioriteitNormaal = new Prioriteit("Normaal");
            Prioriteit prioriteitHoog = new Prioriteit("Hoog");
            context.Prioriteiten.Add(prioriteitLaag);
            context.Prioriteiten.Add(prioriteitNormaal);
            context.Prioriteiten.Add(prioriteitHoog);
            context.SaveChanges();
            #endregion

            #region seed terkenniskoming
            Terkenniskoming terkenniskomingEmail = new Terkenniskoming("E-mail");
            Terkenniskoming terkenniskomingTelefoon = new Terkenniskoming("Telefoon");
            Terkenniskoming terkenniskomingBrief = new Terkenniskoming("Brief");
            context.Terkenniskomingen.Add(terkenniskomingEmail);
            context.Terkenniskomingen.Add(terkenniskomingTelefoon);
            context.Terkenniskomingen.Add(terkenniskomingBrief);
            context.SaveChanges();
            #endregion

            #region seed kwalificatie
            Kwalificatie kwalificatieGegrond = new Kwalificatie("Gegrond");
            Kwalificatie kwalificatieOngegrond= new Kwalificatie("Ongegrond");
            Kwalificatie kwalificatieOnbevoegd = new Kwalificatie("Onbevoegd");
            context.Kwalificaties.Add(kwalificatieGegrond);
            context.Kwalificaties.Add(kwalificatieOngegrond);
            context.Kwalificaties.Add(kwalificatieOnbevoegd);
            context.SaveChanges();
            #endregion

            #region seed dossier
            Dossier d = new Dossier()
            {
                Titel = "TestDossier",
                Inhoud = "Test",
                Terkenniskoming = terkenniskomingEmail,
                OpstartDatum = DateTime.Now,
                MeldingsDatum = DateTime.Now,
                Prioriteit = prioriteitNormaal,
                Status = statusNieuw,
                Kwalificatie = kwalificatieGegrond
            };
            context.Dossiers.Add(d);
            context.SaveChanges();
            #endregion
        }
    }
}
