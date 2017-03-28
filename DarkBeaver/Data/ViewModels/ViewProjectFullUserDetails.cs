using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkBeaver.Data.Models;

namespace DarkBeaver.Data.ViewModels
{
    public class ViewProjectFullUserDetails:MultiPlex.Core.Data.ViewModels.ViewFullUserDetails
    {
        [Display(Name = "DarkBeaver.Data.Data.' Which he is Administrator")]
        public List<Project> ProjectAsAdmin { get; set; }
        [Display(Name = "DarkBeaver.Data.Data.' Which he is Member")]
        public List<Project> ProjectAsMember { get; set; }
    }
}
