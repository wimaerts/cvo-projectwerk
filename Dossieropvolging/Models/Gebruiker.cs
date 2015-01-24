using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dossieropvolging.Models
{
    public class Gebruiker : Personeelslid
    {
        public virtual ICollection<Gebruikersgroep> Lidmaadschap { get; set; }
    }
}