using System.Composition;
using BlackCogs.Interfaces;

namespace DarkBeaver.Verbs
{
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "AdminNavigation")]
    public class AdminUsersVerb : IActionVerb
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
                return "ProjectUser";
            }
        }

        public string Description
        {
            get
            {
                return "Here you Administrage the Existing Users on the DarkBeaver ";
            }
        }

        public bool isAdminPalnel
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return "Project User Administration";
            }
        }
        public string Moduledescription
        {
            get
            {
                Info inf = new Info();
                return inf.Description;
            }
        }

        public string ModuleName
        {
            get
            {
                Info inf = new Info();
                return inf.Name;
            }
        }
    }
}
