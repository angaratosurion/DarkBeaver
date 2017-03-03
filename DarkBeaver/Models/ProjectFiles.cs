using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BlackCogs.Data.Models;

namespace DarkBeaver.Models
{
    
    public class ProjectFiles:Files
    {
        [Required]
        public virtual Project Project { get; set; }
      [Required]
        public int ReleaseId { get; set; }
        [Timestamp]
        public Byte[] RowVersion { get; set; }

    }
}