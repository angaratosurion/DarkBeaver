using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using BlackCogs.Data.Models;
using MultiPlex.Core.Data.Models;
//using MultiPlex.Core.Managers;
using DarkBeaver.Data.Models;

namespace DarkBeaver.Managers
{
    public class ProjectsManager
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ProjectUserManager usrmng = Statics.usrmng;
        MultiPlex.Core.Managers.WikiManager wkmngr = new MultiPlex.Core.Managers.WikiManager();
        PluginManager plugmanger = Statics.plugmanger;
        FileManager filemngr = new FileManager();
        ReleasesManager relmngr = Statics.relmngr;
        //ProjectFileManager projfilemngr = new ProjectFileManager();
        BugManager bugmngr = Statics.bugmngr;
        ChangeLogManager chgMngr = Statics.chgMngr;
        ProjectNewsManager newMngr = Statics.newMngr;

        public List<Project> ListProjectByAdmUser(string username)
        {
            try
            {
                List<Project> ap = null;
                if (!CommonTools.isEmpty(username) && Statics.usrmng.UserExists(username))
                {
                    ApplicationUser adm = Statics.usrmng.GetUser(username);
                    if (adm != null)
                    {
                        ap = this.db.Projects.Where(x => x.Admininstrator == adm.Id).ToList();
                    }

                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public List<Project> ListWikiByUser(string username)
        {
            try
            {
                List<Project> ap = null;
                if (!CommonTools.isEmpty(username) && Statics.usrmng.UserExists(username))
                {
                    ApplicationUser usr = Statics.usrmng.GetUser(username);
                    List<Project> projs = this.List();
                    if (projs != null)
                    {
                        ap = new List<Project>();
                        foreach (Project p in projs)
                        {
                            if (p.Members != null && p.Members.Exists(s => s.Member == usr.Id))
                            {
                                ap.Add(p);
                            }
                        }

                    }
                }
                return ap;

            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);
                return null;
            }
        }
        public List<Project> List()
        {
            try
            {
                List<Project> ap = null;

                ap = db.Projects.ToList();
                return ap;

            }
            catch (Exception ex)
            {

                return null;

            }

        }
        public void Create(Project project,string  user)
        {
            try
            {
                if (usrmng == null)
                {
                    usrmng = new ProjectUserManager();
                }

                if (project != null && CommonTools.isEmpty(user) == false
                   && usrmng.UserExists(user) == true)
                {
                   
                    ApplicationUser admin = usrmng.GetUser(user);
                    if (admin != null)
                    {


                        Wiki wk = new Wiki();
                        wk.Name = project.Name;
                        wk.WikiTitle = project.Name;
                        wk.Administrator = admin.Id;
                        wk.Moderators = new List<WikiMods>();
                        WikiMods wkm = new WikiMods();
                        wkm.Moderator = admin.Id;
                        wkm.Wiki = wk;
                        wk.Moderators.Add(wkm);
                        wkmngr.CreateWiki(wk,user);

                        //project.Admininstrator = admin;
                        //project.AdmininstratorId = admin.Id;
                        ApplicationUser owner = new ApplicationUser();

                        //owner.Claims = admin.Claims;
                        owner = admin.Clone();



                        project.WikiName = project.Name;
                       project.News = new List<ProjectNews>();
                        //List<FileReleases> filelst= new List<FileReleases>();
                        //project.Releases = filelst;
                        project.Members = new List<ProjectMember>();
                        if (db == null)
                        {
                            db = new ApplicationDbContext();
                        }
                        //ProjectUser projusr = new ProjectUser();
                        //   project.Admininstrator = owner;
                       
                       project.Admininstrator = admin.Id;
                       //   db.Configuration.ValidateOnSaveEnabled = false;
                       // db.Configuration.LazyLoadingEnabled = true;
                        //Statics.usersprojmngr.AddNewProjectToUser(admin, project);
                        db.Projects.Add(project);
                      
                        
                        db.SaveChanges();
                        string plugrelpath= plugmanger.GetPluginFilesRelativeDir("DarkBeaver");
                        string path = Path.Combine(plugrelpath, project.Name);
                        FileManager.CreateDirectory(path);

                    }
                }
            

                

               

            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);

            }



        }
        public Project GetProjectById(int  id)
        {
            try
            {
                Project ap = null;

                 if ( id >=0)
                {
                  ap = db.Projects.Find(id);
                }


                return ap;

            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);
                return null;

            }
        }
        public void Edit(Project project)
        {
            try
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                CommonTools.ErrorReporting(ex);

            }
        }
        public void Delete(int id,string user)
        {
            try
            {
                Project proj;
                Wiki wk;
                if ( id >=0 )
                {
                    proj = this.GetProjectById(id);
                    if ( proj !=null && usrmng.UserHasAccessToProject(usrmng.GetUser(user), proj, true) == true)
                    {
                        string path = Path.Combine(plugmanger.GetPluginFilesRelativeDir("DarkBeaver"), proj.Name);
                        if ( proj.WikiName !=null )
                        {
                            this.wkmngr.DeleteWiki(proj.WikiName);
                        }
                        this.relmngr.DeleteByProjectId(id);
                        this.bugmngr.DeleteByProjectId(id);
                        this.chgMngr.DeleteByProjectId(id);
                        this.newMngr.DeleteByProjectId(id);

                        db.Projects.Remove(proj);
                        db.SaveChanges();
                        FileManager.DeleteDirectory(path);
                    }

                }

            }
            catch (Exception ex) { CommonTools.ErrorReporting(ex); }
        }
        public void DeletebyAdm(string user,string adm)
        {
            try
            {
                List<Project> proj;
                Wiki wk;
                if (CommonTools.isEmpty(user) == false && CommonTools.isEmpty(adm)==false)
                {
                    proj = this.ListProjectByAdmUser(user);
                    if ( proj !=null)
                    {
                        foreach(Project p in proj)
                        {
                            this.Delete(p.Id, adm);
                        }
                    }

                }

            }
            catch (Exception ex) { CommonTools.ErrorReporting(ex); }
        }

    }
}
