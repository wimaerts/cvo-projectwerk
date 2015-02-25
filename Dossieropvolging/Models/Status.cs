using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dossieropvolging.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Naam { get; set; }

        public Status()
        {

        }

        public Status(string naam)
        {
            this.Naam = naam;
        }
    }
}