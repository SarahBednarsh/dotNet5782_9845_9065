using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalApi
{
    internal static class DalConfig
    {
        public class DalPackage
        {
            //not sure if should be in caps, that is what it's like in ppt
            public string Name;
            public string PackageName;
            public string NameSpace;
            public string ClassName;
        }
        internal static string DalName;
        internal static Dictionary<string, DalPackage> DalPackages;
        static DalConfig()
        {


            //string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "dal-config.xml");
            XElement dalConfig = XElement.Load(@"dal-config.xml"/*startupPath*/);
            DalName = dalConfig.Element("dal").Value;
            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
                           let tmp1 = pkg.Attribute("namespace")
                           let nameSpace = tmp1 == null ? "Dal" : tmp1.Value
                           let tmp2 = pkg.Attribute("class")
                           let className = tmp2 == null ? pkg.Value : tmp2.Value
                           select new DalPackage()
                           {
                               Name = "" + pkg.Name,
                               PackageName = pkg.Value,
                               NameSpace = nameSpace,
                               ClassName = className
                           })
                          .ToDictionary(p => p.Name, p => p);
        }
    }
}
