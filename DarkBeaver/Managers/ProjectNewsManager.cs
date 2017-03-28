using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BlackCogs.Data.Models;
using DarkBeaver.Data.Models;

namespace DarkBeaver.Managers
{
    public class ProjectNewsManager
    {
        private  ApplicationDbContext db =Statics.db;

        public List<ProjectNews> List()
        {
            try
            {
                List<ProjectNews> ap = null;

                ap = db.ProjectNews.ToList();
                return ap;

            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public List<ProjectNews> ListByProjectId(int id)
        {
            try
            {
                List<ProjectNews> ap = null,news;
                if (id != null)
                { ap = new List<ProjectNews>();
                    news = this.db.ProjectNews.Where(x => x.Project == id).ToList();
                     if ( news !=null)
                    {
                        ap = news;
                    }

                    

                }
               
                return ap;

            }
            catch (Exception ex)
            {
                return null;

            }

        }
        public ProjectNews Create(ProjectNews projectNews,string user)
        {
            try
            {
                ProjectNews ap = null;

            if( projectNews !=null && user!=null)
            {
                ApplicationUser usr=db.Users.First(m => m.UserName == user);
                if (usr != null)
                {
                    projectNews.Author = usr.Id;
                    db.ProjectNews.Add(projectNews);
                    db.SaveChanges();
                    ap = projectNews;
                }
            }
                return ap;
                               
            }
             catch (Exception ex){CommonTools.ErrorReporting(ex);return null;  }
        }

        public ProjectNews Details(int? id)
        {
            try
            {
                ProjectNews ap = null;

                if ( id!=null)
                {
                    ap = db.ProjectNews.Find(id);

                }

                return ap;
            }
             catch (Exception ex){CommonTools.ErrorReporting(ex);return null;  }

        }

        public ProjectNews Edit( ProjectNews projectNews)
        {
            try
            {
                if (projectNews!=null)
                {
                    db.Entry(projectNews).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return projectNews;
            }
             catch (Exception ex){CommonTools.ErrorReporting(ex);return null;  }

        }
        public void Delete(int ?id)
        {
            try
            {
                if (id != null)
                {
                    ProjectNews projectNews = db.ProjectNews.Find(id);
                    db.ProjectNews.Remove(projectNews);
                    db.SaveChanges();  
                }
                
            }
              catch (Exception ex){CommonTools.ErrorReporting(ex);  }

        }
        public void DeleteByProjectId(int id)
        {
            try
            {
                if (id != null)
                {
                    List<ProjectNews> news = this.ListByProjectId(id);
                     if ( news !=null)
                    {
                        foreach( var n in news )
                        {
                            this.Delete(n.Id);
                        }
                    }
                }

            }
            catch (Exception ex) { CommonTools.ErrorReporting(ex); }

        }
    }
}