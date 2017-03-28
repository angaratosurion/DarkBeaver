using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Data.Models;

namespace DarkBeaver.Data.ViewModels
{
    public class ViewProjectUsers
    {
        public ApplicationUser Administrator { get; set; }
        public List<ApplicationUser> Members { get; set; }
        public ViewProject Project { get; set; }
    }
}
