using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DarkBeaver.Data.Models
{
    
    public class ChangeLog
    {
     [Required]
        public int Id{ get; set; }
        [DataType(DataType.Text)]
     public string Title { get; set; }
        [DataType(DataType.Html)]
     public string Content { get; set; }
                [DataType(DataType.DateTime)]
        public DateTime Published { get; set; }
        [Timestamp]
        public Byte []  RowVersion { get; set; }
        [Required]

        public virtual  Project Project { get; set; }
      [Required]
        public virtual FileReleases Releases { get; set; }
      

    }
}