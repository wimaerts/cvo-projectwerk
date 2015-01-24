using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dossieropvolging.Models
{
    public class Personeelslid
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Graad { get; set; }
        public string Sigel { get; set; }
        public int Stamnummer { get; set; }
    }
}