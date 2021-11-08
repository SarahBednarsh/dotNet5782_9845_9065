using System;
using System.Collections.Generic;
using System.Text;
using DalObject;
using IDAL;

namespace IBL
{
    //should this be in namespace BO?
    public partial class BL : IBL
    {
        private IDal dalAP; // DAL access point

        public BL()
        {
            dalAP = new DalObject.DalObject();
        }

    }
}
