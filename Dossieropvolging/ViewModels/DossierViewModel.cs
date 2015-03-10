using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dossieropvolging.Models;

namespace Dossieropvolging.ViewModels
{
    public class DossierViewModel
    {
        public ApplicationUser MyProperty { get; set; }

        public List<Status> lstStatus { get; set; }
        public List<Terkenniskoming> lstTerkenniskoming { get; set; }
        public List<Prioriteit> lstPrioriteit { get; set; }
        public List<Kwalificatie> lstKwalificatie { get; set; }
        public Dossier Dossier { get; set; }
    }
}