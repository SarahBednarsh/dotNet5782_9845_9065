using System;
using System.Collections.Generic;
using System.Text;
using IDAL.DO;
using IDAL;
namespace DalObject
{
    public partial class DalObject : IDal
    {
        public DalObject()
        {
            DataSource.Initialize();
        }
       
        /// <summary>
        /// creates and returns a list of open charge slots
        /// </summary>
        /// <returns></returns>

    }
}
