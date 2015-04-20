using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dossieropvolging.Models;
using System.Web.Security;

namespace Dossieropvolging.ViewModels
{
    public class DossierViewModel
    {
        public ApplicationUser MyProperty { get; set; }

        public List<Status> lstStatus { get; set; }
        public List<Terkenniskoming> lstTerkenniskoming { get; set; }
        public List<Prioriteit> lstPrioriteit { get; set; }
        public List<Kwalificatie> lstKwalificatie { get; set; }
        public List<ApplicationUser> lstGebruikers { get; set; }
        public Dossier Dossier { get; set; }
        public Actie Actie { get; set; }
    }
}