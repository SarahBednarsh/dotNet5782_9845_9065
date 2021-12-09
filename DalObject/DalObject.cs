using System;
using System.Collections.Generic;
using System.Text;
using DO;
using DalApi;
namespace Dal
{
    sealed partial class DalObject : IDal
    {
        #region singleton
        static readonly DalObject instance = new DalObject();
        static DalObject()
        {
            DataSource.Initialize();
        }
        DalObject() { }
        public static DalObject Instance => instance;
        #endregion 
        //public DalObject()
        //{
        //    DataSource.Initialize();
        //}


    }
}
