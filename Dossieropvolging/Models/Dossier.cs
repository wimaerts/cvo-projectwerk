using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dossieropvolging.Models
{
    public class Dossier
    {
        public int Id { get; set; }
        [Required]
        public string Titel { get; set; }
        [Required]
        public string Inhoud { get; set; }
        [Required]
        public Terkenniskoming Terkenniskoming { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public Prioriteit Prioriteit { get; set; }
        [Required]
        public DateTime MeldingsDatum { get; set; }
        [Required]
        public DateTime OpstartDatum { get; set; }
        public DateTime? AfsluitDatum { get; set; }
        public DateTime? AlarmDatum { get; set; }
        public Gebruiker Auteur { get; set; }
        public Gebruiker Dossierbeheerder { get; set; }
        public Kwalificatie Kwalificatie { get; set; }
        public string Besluit { get; set; }

        public virtual ICollection<Bijlage> Bijlages { get; set; }
        public virtual ICollection<Klant> Klanten { get; set; }
        public virtual ICollection<Actie> Acties { get; set; }
        public virtual ICollection<Personeelslid> BetrokkenPersoneelsleden { get; set; }
    }
}