using System.Composition;
using BlackCogs.Interfaces;

namespace DarkBeaver.Verbs
{
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "Navigation")]
    public class DarkBeaverAdmVerb : IActionVerb
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
                return "Projects";
            }
        }

        public string Description
        {
            get
            {
                return "Here you Administrage the Existing Projects";
            }
        }

        public bool isAdminPalnel
        {
            get
            {
                return false;
            }
        }

        public string Name
        {
            get
            {
                return "DarkBeaver  Administration";
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
