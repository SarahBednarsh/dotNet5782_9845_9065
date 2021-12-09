using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DalApi
{
    public static class DalFactory
    {
        public static IDal GetDal()
        {
            string dalType = DalConfig.DalName;
            DalConfig.DalPackage dalPackage;
            try
            {
                dalPackage = DalConfig.DalPackages[dalType];
            }
            catch(KeyNotFoundException exception)
            {
                throw new DalConfigExcpeption($"Wrong Dal type: {dalType}", exception);
            }
            string dalPackageName = dalPackage.PackageName;
            string dalNameSpace = dalPackage.NameSpace;
            string dalClassName = dalPackage.ClassName;
            
        }
    }
}
