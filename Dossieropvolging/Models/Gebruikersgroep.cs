using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dossieropvolging.Models
{
    public class Gebruikersgroep
    {
        public int Id { get; set; }
        public string Naam { get; set; }

        public virtual ICollection<Gebruiker> Gebruikers { get; set; }
    }
}