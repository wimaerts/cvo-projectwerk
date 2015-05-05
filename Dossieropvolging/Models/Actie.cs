using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dossieropvolging.Models
{
    public class Actie
    {
        public int Id { get; set; }
        public DateTime ActieDatum { get; set; }
        public string Inhoud { get; set; }        
    }
}