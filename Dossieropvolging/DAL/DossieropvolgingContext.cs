using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dossieropvolging.Models;

namespace Dossieropvolging.DAL
{
    public class DossieropvolgingContext : DbContext
    {
        public DossieropvolgingContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Dossier> Dossiers { get; set; }
        public DbSet<Actie> Acties { get; set; }
        public DbSet<Bijlage> Bijlages { get; set; }
        public DbSet<Prioriteit> Prioriteiten { get; set; }
        public DbSet<Status> Statussen { get; set; }
        public DbSet<Kwalificatie> Kwalificaties { get; set; }
        public DbSet<Terkenniskoming> Terkenniskomingen { get; set; }
    }
}
