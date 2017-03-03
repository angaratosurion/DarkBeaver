using System.Data.Entity;
using BlackCogs.Data;
using BlackCogs.Data.Models;

namespace DarkBeaver.Models
{
    // public class PluginUser :IdentityUser
    //{ 
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<PluginUser> manager)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }
    //}


    // public class ApplicationDbContext :IdentityDbContext<ApplicationUser>
    public class ApplicationDbContext : Context

    {

        //public ApplicationDbContext()
        //    : base("DefaultConnection", throwIfV1Schema: false)
        //{
        //}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<DarkBeaver.Models.FileReleases>()
            //    .HasRequired(c => c.UploadedBy)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<DarkBeaver.Models.FileReleases>()
            //   .HasRequired(c => c.UploadedBy)
            //  .WithRequiredDependent()
            //   .WillCascadeOnDelete(false);
            //modelBuilder.Entity<DarkBeaver.Models.FileReleases>()
            //  .HasRequired(c => c.UploadedBy)
            // .WithRequiredPrincipal()
            //  .WillCascadeOnDelete(false);
            //modelBuilder.Entity<DarkBeaver.Models.FileReleases>()
            //  .HasRequired(c => c.Project)
            // .WithRequiredPrincipal()
            //  .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Project>()
            //   .HasOptional(c => c.Releases)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<ProjectFiles>()
            modelBuilder.Entity<Project>()
               .HasOptional(c => c.Releases)
               .WithMany()
               .WillCascadeOnDelete(false);

            //    .HasRequired(c => c.Release)
            //    .WithRequiredDependent()
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<ProjectFiles>()
            //    .HasRequired(c => c.Release)
            //    .WithRequiredPrincipal()
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<ProjectFiles>()
            //  .HasRequired(c => c.Release)
            //  .WithOptional()
            //  .WillCascadeOnDelete(false);










        }
        public static  ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Project> DarkBeaver { get; set; }
        public System.Data.Entity.DbSet<ProjectNews> ProjectNews { get; set; }
        public System.Data.Entity.DbSet<ProjectFiles> ProjectFiles { get; set; }
        public System.Data.Entity.DbSet<FileReleases> FileReleases { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }
        public DbSet<Bugs> Bugs { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        




        //public System.Data.Entity.DbSet<BlackOwl.Core.Data.Models.Plugin> Plugins { get; set; }




    }
}