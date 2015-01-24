using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dossieropvolging.Models
{
    public class Dossier
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Inhoud { get; set; }
        public Status Status { get; set; }
        public Prioriteit Prioriteit { get; set; }
        public DateTime OpstartDatum { get; set; }
        public DateTime AfsluitDatum { get; set; }
        public DateTime AlarmDatum { get; set; }
        public Gebruiker Auteur { get; set; }
        public Gebruiker Dossierbeheerder { get; set; }
        public string Besluit { get; set; }

        public virtual ICollection<Bijlage> Bijlages { get; set; }
        public virtual ICollection<Klant> Klanten { get; set; }
        public virtual ICollection<Actie> Acties { get; set; }
        public virtual ICollection<Personeelslid> BetrokkenPersoneelsleden { get; set; }
    }
}