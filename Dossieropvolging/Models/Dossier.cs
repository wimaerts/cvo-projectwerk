using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dossieropvolging.Models
{
    public class Dossier
    {
        [DisplayName("Dossier")]
        public int Id { get; set; }

        //[Required]
        public string Titel { get; set; }

        //[Required]
        public string Inhoud { get; set; }

        //[Required]
        public virtual Terkenniskoming Terkenniskoming { get; set; }

        //[Required]
        public virtual Status Status { get; set; }

        //[Required]
        public virtual Prioriteit Prioriteit { get; set; }

        //[Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? MeldingsDatum { get; set; }

        //[Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? OpstartDatum { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? AfsluitDatum { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? AlarmDatum { get; set; }

        //public virtual Gebruiker Auteur { get; set; }
        public string Dossierbeheerder { get; set; }
        public virtual Kwalificatie Kwalificatie { get; set; }
        public string Besluit { get; set; }

        public virtual ICollection<Bijlage> Bijlages { get; set; }
        public virtual ICollection<Klant> Klanten { get; set; }
        public virtual ICollection<Actie> Acties { get; set; }
        public virtual ICollection<Personeelslid> BetrokkenPersoneelsleden { get; set; }
    }
}