using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dossieropvolging.Models
{
    public class Prioriteit
    {
        public int Id { get; set; }
        public string Naam { get; set; }

        public Prioriteit()
        {

        }

        public Prioriteit(String naam)
        {
            this.Naam = naam;
        }
    }
}