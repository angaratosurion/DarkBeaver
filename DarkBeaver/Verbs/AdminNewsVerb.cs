using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackCogs.Interfaces;
 

namespace DarkBeaver .Verbs
{
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "AdminNavigation")]
    public class AdminNewsVerb : IActionVerb
    {
        public string Action
        {
            get
            {
                return "Index";
            }
        }

        public string Controller
        {
            get
            {
                return "ProjectNews";
            }
        }

        public string Description
        {
            get
            {
                return "Here you Administrage the Existing News on the DarkBeaver ";
            }
        }

        public bool isAdminPalnel
        {
            get
            {
                return true;
            }
        }

        public string Moduledescription
        {
            get
            {
                DarkBeaverInfo inf = new DarkBeaverInfo();
                return inf.Description;
            }
        }

        public string ModuleName
        {
            get
            {
                DarkBeaverInfo inf = new DarkBeaverInfo();
                return inf.Name;
            }
        }

        public string Name
        {
            get
            {
                return "News Administration";
            }
        }
    }
}
