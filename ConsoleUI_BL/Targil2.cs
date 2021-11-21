using System;
using IBL.BO;
using IBL;
namespace ConsoleUI_BL
{
    public partial class Targil2
    {
        private enum Actions { Exit, Add, Update, View, List, Calc }
        private enum Data { Station = 1, Drone, Customer, Parcel, ParcelNotAttributed, StationsWithAvailableChargers }
        private enum UpdateOption { DroneName = 1, Station, Customer, SendToCharge, EndCharge, Attribute, Pickup, Deliver }
        public static void Main(string[] args)
        {
            IBL.BO.IBL bl = new IBL.BO.BL();
            Actions option;
            do
            {
                Console.WriteLine("Enter 1 for adding an entity\n" +
                    "Enter 2 for updating an entity\n" +
                    "Enter 3 for displaying an entity\n" +
                    "Enter 4 for displaying a list of entities\n" +
                    "Enter 5 for calculating ditance from an entity\n" +
                    "Enter 0 for Exit");
                Int32.TryParse(Console.ReadLine(), out int input);
                option = (Actions)input;
                switch (option)
                {
                    case Actions.Exit:
                        Console.WriteLine("Bye bye!");
                        break;
                    case Actions.Add:
                        SwitchAdd(bl);
                        break;
                    case Actions.Update:
                        SwitchUpdate(bl);
                        break;
                    case Actions.View:
                        SwitchView(bl);
                        break;
                    case Actions.List:
                        SwitchList(bl);
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;
                }

            } while (option != 0);
        }
    }
}
