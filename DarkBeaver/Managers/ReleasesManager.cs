using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DarkBeaver.Data.Models;

namespace DarkBeaver.Managers
{
    public class ReleasesManager
    {
        private ApplicationDbContext db = Statics.db;
        public ReleasesManager()
        {
            if (Statics.db==null)
            {
                db = new ApplicationDbContext();
            }
        }

        ProjectFileManager projfilmngr = Statics.projfilmngr;
        public  void CreateNew (FileReleases release)
        {
            try
            {
                if (release !=null )
                {
                    db.FileReleases.Add(release);
                    db.SaveChanges();
                }

            }
              catch (Exception ex){
                Statics.db = new ApplicationDbContext();
                CommonTools.ErrorReporting(ex);  }
        }
      
        public void Edit(FileReleases release)
        {
            try
            {
                if ( release !=null)
                {
                    db.Entry(release).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
              catch (Exception ex){CommonTools.ErrorReporting(ex);  }
        }
        public void Delete(FileReleases fileReleases)
        {
            try
            {
                if ( fileReleases !=null)
                {
                    List<ProjectFiles> files = this.projfilmngr.DetailsByReleaseId(fileReleases.Id);
                    foreach ( var f in files)
                    {
                        projfilmngr.Delete(f);
                    }
                    db.FileReleases.Remove(fileReleases);
                    db.SaveChanges();
                }

            }
              catch (Exception ex){CommonTools.ErrorReporting(ex);  }
        }
        public void DeleteByProjectId(int? id)
        {
            try
            {
                if (id != null)
                {
                    List<FileReleases> files = this.GetAllReleasesByProjectId(id);
                    foreach (var f in files)
                    {
                        this.Delete(f);
                    }
                }

            }
            catch (Exception ex) { CommonTools.ErrorReporting(ex); }
        }

        public FileReleases GetDetailsById(int?  id)
        {
            try
            {
                FileReleases ap = null;
                if ( id>0)
                {
                    ap = db.FileReleases.Find(id);
                }

                return ap;

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public List<FileReleases> GetAllReleases()
        {
            try
            {
                List<FileReleases> ap = null;
                ap = db.FileReleases.ToList();


                return ap;

            }
            catch (Exception)
            {
                
                throw;
                return null;
            }
        }
        public List<FileReleases> GetAllReleasesByProjectId(int? id)
        {
            try
            {
                List<FileReleases> ap = null;

                //  var q = db.FileReleases.Include(f => f.ChangeLog).ToList();
                var q = this.GetAllReleases();
                if (q != null)
                {
                    
                    ap = q.FindAll(s => s.Project.Id == id);
                }



                return ap; 
            }
            catch (Exception)
            {
                
                throw;
                return null;
            }
        }
    }
}