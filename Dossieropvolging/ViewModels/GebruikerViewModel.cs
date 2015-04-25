using Dossieropvolging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dossieropvolging.ViewModels
{
    public class GebruikerViewModel
    {
        public ApplicationUser gebruiker { get; set; }
        public bool beheerder { get; set; }
        public string wachtwoord { get; set; }
    }
}