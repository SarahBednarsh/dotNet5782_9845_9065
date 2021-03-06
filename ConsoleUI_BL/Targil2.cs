using System;
using BO;
using BlApi;
namespace ConsoleUI_BL
{
    public partial class Targil2
    {
        private enum Actions { Exit, Add, Update, View, List, Calc }
        private enum Data { Station = 1, Drone, Customer, Parcel, ParcelNotAttributed, StationsWithAvailableChargers }
        private enum UpdateOption { DroneName = 1, Station, Customer, SendToCharge, EndCharge, Attribute, Pickup, Deliver }
        public static void Main(string[] args)
        {
            IBL bl = BlFactory.GetBL();

            Actions option;
            do
            {
                Console.WriteLine("Enter 1 for adding an entity\n" +
                    "Enter 2 for updating an entity\n" +
                    "Enter 3 for displaying an entity\n" +
                    "Enter 4 for displaying a list of entities\n" +
                    "Enter 0 for Exit");
                Int32.TryParse(Console.ReadLine(), out int input);
                option = (Actions)input;
                try
                {
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
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

            } while (option != 0);
        }
    }
}
