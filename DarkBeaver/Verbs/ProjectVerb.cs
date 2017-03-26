using System.Composition;
using BlackCogs.Interfaces;

namespace DarkBeaver.Verbs
{
    [Export(typeof(IActionVerb)), ExportMetadata("Category", "Navigation")]
    public class ProjectVerb : IActionVerb
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
                return "Handles Projects";
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
                return "Projects";
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
    }
}
