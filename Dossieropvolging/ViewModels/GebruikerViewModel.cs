using Dossieropvolging.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dossieropvolging.ViewModels
{
    public class GebruikerViewModel
    {
        public ApplicationUser gebruiker { get; set; }
        public bool beheerder { get; set; }

        [DataType(DataType.Password)]
        public string wachtwoord { get; set; }
    }
}