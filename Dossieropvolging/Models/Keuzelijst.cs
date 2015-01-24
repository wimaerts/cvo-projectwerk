using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dossieropvolging.Models
{
    public class Keuzelijst
    {
        public int Id { get; set; }
        public string Naam { get; set; }

        public virtual ICollection<String> Inhoud { get; set; }
    }
}