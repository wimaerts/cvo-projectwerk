using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dossieropvolging.Models
{
    public class Kwalificatie
    {
        public int Id { get; set; }
        public string Naam { get; set; }

        public Kwalificatie()
        {

        }

        public Kwalificatie(string naam)
        {
            this.Naam = naam;
        }
    }
}