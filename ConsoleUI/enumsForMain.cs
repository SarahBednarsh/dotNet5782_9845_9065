using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    public enum Actions { Exit, Add, Update, View, List }
    public enum Data { Station = 1, Drone, Customer, Parcel, ParcelNotAttributed, StationsWithAvailableChargers }
    public enum UpdateOption { Attribute = 1, Pickup, Ship, SendToCharge }
}
