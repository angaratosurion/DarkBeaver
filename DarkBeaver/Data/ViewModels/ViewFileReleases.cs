using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BlackCogs;
using BlackCogs.Data.Models;
using DarkBeaver.Data.Models;

namespace DarkBeaver.Data.ViewModels
{
    //[Table("ProjectRleases")]
    public class ViewFileReleases
    {
       [Required]
        
        public int Id { get; set; }
        public string Tittle { get; set; }
         [Required]
        public string Version { get; set; }
        public DateTime Published { get; set; }
        [DataType(DataType.Html)]
        public string content { get; set; }
        [Timestamp]
        public Byte[] RowVersion { get; set; }

        [Required]
        public virtual List<ProjectFiles> Files { get; set; }
        [Required]
        public virtual Project Project { get; set; }


        public virtual  ApplicationUser UploadedBy { get; set; }
        //
        // [Required]
        public virtual ChangeLog ChangeLog { get; set; }
        public void ImportFromModel(FileReleases md)
        {
            try
            {
                if (md != null && CommonTools.isEmpty(md.UploadedBy) == false
                    )
                {
                    ApplicationUser user = CommonTools.usrmng.GetUserbyID(md.UploadedBy);
                   
                    if (user != null )
                    {
                        this.Id = md.Id;
                        this.Tittle = md.Tittle;
                        if (md.Project != null)
                        {
                            this.Project = md.Project;

                        }
                        this.Version = md.Version;
                        this.UploadedBy = user;
                        this.RowVersion = md.RowVersion;
                       
                        this.content = md.content;
                        this.ChangeLog = md.ChangeLog;
                        this.Files = md.Files;
                        this.Published = md.Published;


                    }
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public FileReleases ExportTomodel()
        {
            try
            {
                FileReleases ap = new FileReleases();
                ap.Id = Id;
                ap.Tittle = Tittle;
                if (Project != null)
                {
                    ap.Project = Project;

                }
                ap.Version = Version;
                if (UploadedBy != null)
                {
                    ap.UploadedBy = UploadedBy.Id;
                }
                ap.RowVersion = RowVersion;

                ap.content = content;
                ap.ChangeLog = ChangeLog;
                ap.Files = Files;
                ap.Published = Published;




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