﻿using System;
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

        public string Titel { get; set; }
        public string Inhoud { get; set; }

        public bool AlarmDatumVerstreken { get; set; }

        public virtual Terkenniskoming Terkenniskoming { get; set; }
        public virtual Status Status { get; set; }
        public virtual Prioriteit Prioriteit { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? MeldingsDatum { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? OpstartDatum { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? AfsluitDatum { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? AlarmDatum { get; set; }

        public string Dossierbeheerder { get; set; }
        
        public virtual Kwalificatie Kwalificatie { get; set; }
        public string Besluit { get; set; }

        public virtual ICollection<Bijlage> Bijlages { get; set; }
        public virtual ICollection<Actie> Acties { get; set; }
    }
}