using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using BlackCogs.Data.Models;

namespace DarkBeaver.Data.Models
{
    
    public class ProjectNews:News
    {
        [Required]
        public int Project { get; set; }
        [Timestamp]
        public Byte[] RowVersion { get; set; }
        // [Required]
        //   public int ProjectId { get; set; }
    }
}