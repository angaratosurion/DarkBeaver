using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlackCogs;
using BlackCogs.Data.Models;
using DarkBeaver.Data.Models;

namespace DarkBeaver.Data.ViewModels
{

    public class ViewProject
    {
        [Required]
        public int Id { get; set; }
        //   public int revision { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string WikiName { get; set; }

        [Timestamp]
        public Byte[] RowVersion { get; set; }
        [Required]

        public virtual ApplicationUser Admininstrator { get; set; }
        public virtual List<ApplicationUser> Members { get; set; }
        public virtual List<ProjectNews> News { get; set; }
        public virtual List<FileReleases> Releases { get; set; }
        public virtual List<ChangeLog> ChangeLogs { get; set; }
        public virtual List<Bugs> Bugs { get; set; }

        public void ImportFromModel(Project md)
        {
            try
            {
                if (md != null && CommonTools.isEmpty(md.Admininstrator) == false
                    )
                {
                    ApplicationUser user = CommonTools.usrmng.GetUserbyID(md.Admininstrator);

                    if (user != null)
                    {
                        this.Id = md.Id;
                        this.Bugs = md.Bugs;
                        this.ChangeLogs = md.ChangeLogs;
                        this.Name = md.Name;
                        this.News = md.News;
                        this.Releases = md.Releases;
                        this.RowVersion = md.RowVersion;
                        this.WikiName = md.WikiName;


                        this.Description = md.Description;
                        this.Admininstrator = user;
                        if (md.Members != null)
                        {
                            List<ApplicationUser> mem = new List<ApplicationUser>();
                            foreach (var m in md.Members)
                            {
                                ApplicationUser ms = CommonTools.usrmng.GetUserbyID(m.Member);
                                if (ms != null)
                                {
                                    mem.Add(ms);

                                }

                            }
                            this.Members = mem;
                        }



                    }
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public Project ExportToModel()
        {
            try
            {
                Project ap = new Project();

                ap.Id = Id;
                ap.Bugs = Bugs;
                ap.ChangeLogs = ChangeLogs;
                ap.Name = Name;
                ap.News = News;
                ap.Releases = Releases;
                ap.RowVersion = RowVersion;
                ap.WikiName = WikiName;


                ap.Description = Description;
                if (Admininstrator != null)
                {
                    ap.Admininstrator = Admininstrator.Id;
                }
                if (Members != null)
                {
                    ap.Members = new List<ProjectMember>();
                    foreach (var m in this.Members)
                    {
                        ProjectMember mem = new ProjectMember();
                        mem.Member = m.Id;
                        mem.Project = ap;
                        ap.Members.Add(mem);

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
    }
}