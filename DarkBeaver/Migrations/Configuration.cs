namespace DarkBeaver.Migrations
{
    using System.Data.Entity.Migrations;
    using BlackCogs.Data;
    using BlackCogs.Data.Models;
    using DarkBeaver.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<DarkBeaver.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;



        }

        protected override void Seed(DarkBeaver.Models.ApplicationDbContext context)
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


            //IdentityRole Adminrole = new IdentityRole();
            //Adminrole.Name = "Administrators";

            var userStore = new UserStore<ApplicationUser>(context);
            var mngr = new UserManager<ApplicationUser>(userStore);

            //     if (!context.Users.Any(u => u.UserName == "admin"))
            {

                var admin = new ApplicationUser { UserName = "admin", Email = "admin@loclahost" };



                mngr.Create(admin, "Adm!n0");

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole { Name = "Administrators" });
                //context.Users.AddOrUpdate(admin);

                context.SaveChanges();

            }

            context.Configuration.AutoDetectChangesEnabled = true;
            FileType image = new FileType();
            image.Name = "Images";
            image.Extention = "jpg;png";
            context.FileTypes.AddOrUpdate(image);
            FileType AppType = new FileType();
            AppType.Name = "Executable";
            AppType.Extention = "exe";
            context.FileTypes.AddOrUpdate(AppType);
            context.SaveChanges();






        }

    }
}