using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkBeaver.Models
{
    public class ProjectMember
    {
        
            [Required]
            // [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [Required]

            public virtual Project Project { get; set; }
            [Required]
            //  [Key]
            public string Member { get; set; }
       
    }
}
