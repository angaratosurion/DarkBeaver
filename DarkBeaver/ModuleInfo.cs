﻿using System.Composition;
using System.Reflection;
using BlackCogs.Interfaces;

namespace DarkBeaver
{
    [Export(typeof(IModuleInfo))]
    public class Info : IModuleInfo
    {
        public string Description
        {
            get
            {
                return "";
            }
        }

        public string Name
        {
            get
            {
                return "DarkBeaver";
            }
        }

        public string SourceCode
        {
            get
            {
                return "https://github.com/angaratosurion/DarkBeaver";
            }
        }

        public string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string WebSite
        {
            get
            {
                return "http://pariskoutsioukis.net/blog/";
            }
        }
    }
}