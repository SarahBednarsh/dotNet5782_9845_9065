using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        public enum DroneStatuses { Available = 1, InMaintenance, Delivering }
        public enum WeightCategories { Light = 1, Medium, Heavy }
        public enum Priorities { Normal = 1, Quick, Emergency }
        public enum States { Created = 1, Attributed, PickedUp, Delivered}
    }
}
