using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BlackCogs.Data.Models;
 

namespace DarkBeaver.Models
{
    //[Table("ProjectRleases")]
    public class FileReleases
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


        public string UploadedBy { get; set; }
        //
        // [Required]
        public virtual ChangeLog ChangeLog { get; set; }
    }
}