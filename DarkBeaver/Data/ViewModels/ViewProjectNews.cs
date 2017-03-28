using BlackCogs.Data.ViewModels;
using DarkBeaver.Data.Models;
using DarkBeaver.Managers;
using System;
using System.ComponentModel.DataAnnotations;

namespace DarkBeaver.Data.ViewModels
{
    public class ViewProjectNews:ViewNews
    {
        [Required]
        public  Project Project { get; set; }
       /* [Timestamp]
        public Byte[] RowVersion { get; set; }*/
        public void ImportFromModel(ProjectNews md)
        {
            try
            {
                if (md != null && CommonTools.isEmpty(md.Author) == false
                    )
                {

                    base.ImportFromModel(md);
                    ProjectsManager mng = new ProjectsManager();
                    Project = mng.GetProjectById(md.Project);


                    
                }
            }
            catch (Exception ex)
            {

                CommonTools.ErrorReporting(ex);

            }
        }
        public ProjectNews ExportToModel()
        {
            try
            {
                ProjectNews ap = new ProjectNews();

                ap =(ProjectNews) base.ExportToModel();
                 if ( Project !=null)
                {
                    ap.Project = Project.Id;
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