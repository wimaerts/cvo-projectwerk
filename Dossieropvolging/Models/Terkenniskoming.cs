using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dossieropvolging.Models
{
    public class Terkenniskoming
    {
        public int Id { get; set; }
        public string Naam { get; set; }

        public Terkenniskoming()
        {

        }

        public Terkenniskoming(string naam)
        {
            this.Naam = naam;
        }
    }
}