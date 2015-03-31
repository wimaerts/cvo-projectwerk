using Dossieropvolging.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dossieropvolging.ViewModels
{
    public class BeheerViewModel
    {
        public List<ApplicationUser> lstGebruikers { get; set; }
        //public List<IdentityRole> lstRollen { get; set; }
        public List<ApplicationUser> lstAdmins { get; set; }
    }
}