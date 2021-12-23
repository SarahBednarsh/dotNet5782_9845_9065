using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PL
{
    public class CustomerToList : BO.CustomerToList, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
