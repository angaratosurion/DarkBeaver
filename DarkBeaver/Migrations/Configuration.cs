namespace DarkBeaver.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BlackCogs.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<DarkBeaver.Data.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;



        }

        protected override void Seed(DarkBeaver.Data.Models.ApplicationDbContext context)
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
            IdentityRole role = new IdentityRole("Administrators");
            context.Roles.AddOrUpdate(r => r.Name, role);

            //var passwordHash = new PasswordHasher();
            //string password = passwordHash.HashPassword("Adm!n0");
            //context.Users.AddOrUpdate(u => u.UserName,
            //    new ApplicationUser
            //    {
            //        UserName = "admin@localhost.com",
            //        PasswordHash = password,
            //        Email= "admin@localhost.com"
            //        //   PhoneNumber = "08869879"

            //    });




            context.SaveChanges();
            ApplicationUser adm = new ApplicationUser();

            adm.Email = "admin@localhost.com";
            adm.UserName = adm.Email;
            adm.DisplayName = "Admin";
            mngr.Create(adm, "Adm!n0");
            IdentityRole adrol = context.Roles.First(x => x.Name == "Administrators");
            adm = mngr.FindByEmail("admin@localhost.com");
            if (adm != null)
            {
                mngr.AddToRole(adm.Id, adrol.Name);
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