using System;
using System.Collections.Generic;
using System.Text;
using DO;
using DalApi;
namespace Dal
{
    internal sealed partial class DalObject : IDal
    {

        #region singleton
        private static DalObject instance = null;
        static DalObject() { }
        internal static DalObject Instance
        {
            get
            {
                DalObject localRef = instance;
                if (localRef == null)
                {
                    object LOCK = new object();
                    lock (LOCK)
                    {
                        if (instance == null)
                            instance = new DalObject();
                    }
                }
                return instance;
            }
        }
        #endregion
        public DalObject()
        {
            DataSource.Initialize();
        }


    }
}
