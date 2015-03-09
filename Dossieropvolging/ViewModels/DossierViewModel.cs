using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dossieropvolging.Models;

namespace Dossieropvolging.ViewModels
{
    public class DossierViewModel
    {
        public List<Status> lstStatus { get; set; }
        public Dossier Dossier { get; set; }
    }
}