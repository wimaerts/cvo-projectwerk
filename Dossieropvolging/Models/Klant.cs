using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dossieropvolging.Models
{
    public class Klant
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Bedrijf { get; set; }        
        public string Email { get; set; }
        public string Telnr { get; set; }
        public string Opmerking { get; set; }
        public string Straat { get; set; }
        public string Huisnr { get; set; }
        public string Busnr { get; set; }
        public int Postcode { get; set; }
        [Required]
        public Gemeente Gemeente { get; set; }
        [Required]
        public Land Land { get; set; }

        public virtual ICollection<Dossier> Dossiers { get; set; }
    }
}