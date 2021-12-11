using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace DalApi
{
    public static class DalFactory
    {
        public static IDal GetDal(string dalType)
        {
            DalConfig.DalPackage dalPackage;
            try
            {
                dalPackage = DalConfig.DalPackages[dalType];
            }
            catch(KeyNotFoundException exception)
            {
                throw new DalConfigException($"Wrong Dal type: {dalType}", exception);
            }
            string dalPackageName = dalPackage.PackageName;
            string dalNameSpace = dalPackage.NameSpace;
            string dalClassName = dalPackage.ClassName;

            //string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, dalPackageName);
            try
            {
                Assembly.Load(dalPackageName);
            }
            catch (Exception ex)
            {
                throw new DalConfigException($"Failed loading {dalPackage}.dll", ex);
            }
            Type type = Type.GetType($"Dal.{dalPackageName}, {dalPackageName}");
            // If the type is not found - the implementation is not correct - it looks like the class name is wrong...
            if (type == null)
                throw new DalConfigException($"Class name is not the same as Assembly Name: {dalPackage}");
            try
            {
                IDal dal = type.GetProperty("Instance", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null) as IDal;
                // If the instance property is not initialized (i.e. it does not hold a real instance reference)...
                if (dal == null)
                    throw new DalConfigException($"Class {dalPackage} instance is not initialized");
                // now it looks like we have appropriate dal implementation instance :-)
                return dal;
            }
            catch (NullReferenceException ex)
            {
                throw new DalConfigException($"Class {dalPackage} is not a singleton", ex);
            }
        }
    }
}
