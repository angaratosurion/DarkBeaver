using System;
using System.ComponentModel.DataAnnotations;
//using BlackCogs;
using BlackCogs.Data.Models;
using DarkBeaver.Models;

namespace DarkBeaver.ViewModels
{
    public class ViewBugs
    {
       
        [Required]
        public int Id { get; set; }
        //   public int revision { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ReporedAt { get; set; }
         [DataType(DataType.DateTime)]
        public DateTime EditedAt { get; set; }
        [Timestamp]
        public Byte []  RowVersion { get; set; }
        [Required]
        public virtual ApplicationUser ReportedBy { get; set; }

        public virtual  ApplicationUser EditedBy { get; set; }
        [Required]
        public virtual Project Project { get; set; }
        public void ImportFromModel(Bugs md)
        {
            try
            {
                if (md != null && CommonTools.isEmpty(md.ReportedBy) == false
                    && CommonTools.isEmpty(md.EditedBy)==false)
                {
                    ApplicationUser user = CommonTools.usrmng.GetUserbyID(md.ReportedBy);
                    ApplicationUser eduser = CommonTools.usrmng.GetUserbyID(md.EditedBy);
                    if (user != null && eduser!=null)
                    {
                        this.Id = md.Id;
                        this.Name = md.Name;
                        if ( md.Project!=null)
                        {
                            this.Project = md.Project;

                        }
                        this.ReporedAt = md.ReporedAt;
                        this.ReportedBy = user;
                        this.RowVersion = md.RowVersion;
                        this.EditedBy = eduser;
                        this.Description = md.Description;
                        

                    }
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public Bugs ExportTomodel()
        {
            try
            {
                Bugs ap = new Bugs();
                ap.Description = Description;
                ap.EditedAt = EditedAt;
                ap.Id = Id;
                ap.Name = Name;
                ap.ReporedAt = ReporedAt;
                if ( EditedBy!=null)
                {
                    ap.EditedBy = EditedBy.Id;
                }
                if ( ReportedBy!=null)
                {
                    ap.ReportedBy = ReportedBy.Id;
                }
                if (Project !=null)
                {
                    ap.Project = Project;

                }
                this.RowVersion = RowVersion;




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