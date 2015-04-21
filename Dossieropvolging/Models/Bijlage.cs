using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dossieropvolging.Models
{
    public class Bijlage
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public byte[] Inhoud { get; set; }
    }
}