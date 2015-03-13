namespace Dossieropvolging.Migrations
{
    using Dossieropvolging.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Dossieropvolging.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Dossieropvolging.Models.ApplicationDbContext context)
        {

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //var passwordHash = new PasswordHasher();
            //string password = passwordHash.HashPassword("cvo123");
            //context.Users.AddOrUpdate(u => u.UserName,
            //    new ApplicationUser
            //    {
            //        UserName = "wim.aerts@gmail.com",
            //        PasswordHash = password

            //    });

            //if (!(context.Users.Any(u => u.UserName == "dj@dj.com")))
            //{
            //    var userStore = new UserStore<ApplicationUser>(context);
            //    var userManager = new UserManager<ApplicationUser>(userStore);
            //    var userToInsert = new ApplicationUser { UserName = "wim.aerts@gmail.com", PhoneNumber = "0797697898" };
            //    userManager.Create(userToInsert, "cvo123");
            //}
        }
    }
}
