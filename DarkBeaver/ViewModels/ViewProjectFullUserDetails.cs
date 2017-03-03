using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkBeaver.Models;

namespace DarkBeaver.ViewModels
{
    public class ViewProjectFullUserDetails:MultiPlex.Core.Data.ViewModels.ViewFullUserDetails
    {
        [Display(Name = "DarkBeaver' Which he is Administrator")]
        public List<Project> ProjectAsAdmin { get; set; }
        [Display(Name = "DarkBeaver' Which he is Member")]
        public List<Project> ProjectAsMember { get; set; }
    }
}
