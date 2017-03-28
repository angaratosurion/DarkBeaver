using System.Data.Entity;
using BlackCogs.Data;
using BlackCogs.Data.Models;
using MultiPlex.Core.Data.Models;

namespace DarkBeaver.Data.Models
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
            //modelBuilder.Entity<DarkBeaver.Data.Models.FileReleases>()
            //    .HasRequired(c => c.UploadedBy)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<DarkBeaver.Data.Models.FileReleases>()
            //   .HasRequired(c => c.UploadedBy)
            //  .WithRequiredDependent()
            //   .WillCascadeOnDelete(false);
            //modelBuilder.Entity<DarkBeaver.Data.Models.FileReleases>()
            //  .HasRequired(c => c.UploadedBy)
            // .WithRequiredPrincipal()
            //  .WillCascadeOnDelete(false);
            //modelBuilder.Entity<DarkBeaver.Data.Models.FileReleases>()
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

            modelBuilder.Entity<WikiTitle>()
             .HasRequired(t => t.Wiki)
             .WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<WikiTitle>()
                .HasKey(p => p.TitleId);


            modelBuilder.Entity<WikiFile>()
                .HasRequired(f => f.Wiki)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<WikiModInvitations>()
               .HasRequired(f => f.Wiki)
               .WithMany()
               .WillCascadeOnDelete(false);








        }
        public static  ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Project> Projects { get; set; }
        public System.Data.Entity.DbSet<ProjectNews> ProjectNews { get; set; }
        public System.Data.Entity.DbSet<ProjectFiles> ProjectFiles { get; set; }
        public System.Data.Entity.DbSet<FileReleases> FileReleases { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }
        public DbSet<Bugs> Bugs { get; set; }
        public DbSet<ProjectMember> ProjectMembers { get; set; }
        public IDbSet<WikiContent> WikiContent { get; set; }
        public IDbSet<WikiTitle> WikiTitle { get; set; }
        public IDbSet<Wiki> Wikis { get; set; }
        public IDbSet<WikiCategory> Categories { get; set; }
        public IDbSet<WikiFile> WikiFiles { get; set; }
        public IDbSet<WikiModInvitations> ModInvites { get; set; }




        //public System.Data.Entity.DbSet<BlackOwl.Core.Data.Models.Plugin> Plugins { get; set; }




    }
}