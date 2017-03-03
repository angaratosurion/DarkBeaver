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
                return "DarkBeaver";
            }
        }

        public string Description
        {
            get
            {
                return "Handles DarkBeaver";
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
                return "DarkBeaver";
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
