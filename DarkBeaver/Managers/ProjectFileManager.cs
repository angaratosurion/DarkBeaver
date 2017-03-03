using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
using DarkBeaver.Models;
using System.IO;
 
using System.Data.Entity;

namespace DarkBeaver.Managers
{

    
    public class ProjectFileManager
    {
         private ApplicationDbContext db =Statics.db;
        FileManager FileManager = Statics.FileManager;// = new FileManager();
        PluginManager plugmanger=Statics.plugmanger;
        ReleasesManager relmngr = Statics.relmngr;
        public ProjectFileManager ( )
        {
           
        }
        public void Create(ProjectFiles file,HttpPostedFileBase filcnt)
        {
            try
            {
                if ( file!=null && filcnt!=null)
                {
                    int relid = file.ReleaseId;

                    string release = relmngr.GetDetailsById(relid).Version;

                    string path = Path.Combine(plugmanger.GetPluginFilesPthysicalDir("DarkBeaver"),file.Project.Name,
                        release,filcnt.FileName);
                  Boolean ap=  FileManager.CreateFile(path, filcnt);
                    file.Path = path;

                    

                    db.ProjectFiles.Add(file);
                    db.SaveChanges();
                    
                    
                    
                    
                }
            }
              catch (Exception ex){CommonTools.ErrorReporting(ex);  }
        }
        public ProjectFiles DetailsById(int ?id)
        {
            try
            {
                return db.ProjectFiles.Find(id);
            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public List<ProjectFiles> DetailsByReleaseId(int? id)
        {
            try
            {
                return db.ProjectFiles.Where(s => s.ReleaseId == id).ToList();
            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public List<ProjectFiles> DetailsByProjectId(int? id)
        {
            try
            {
                return db.ProjectFiles.Where(s => s.Project.Id == id).ToList();
            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public void Edit(ProjectFiles projectFiles)
        {
            try
            {
                if (projectFiles !=null)
                {
                    db.Entry(projectFiles).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                //return null;
            }
        }
        public void Delete(ProjectFiles projectFiles)
        {
            try
            {
                if (projectFiles != null)
                {
                    if (FileManager.FileExists(projectFiles.Path))
                    {
                        FileManager.DeleteFile(projectFiles.Path);
                      
                    }
                    db.ProjectFiles.Remove(projectFiles);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                //return null;
            }
        }
    }
}