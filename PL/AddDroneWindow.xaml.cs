using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IBL.BO;
namespace PL
{
    /// <summary>
    /// Interaction logic for AddDroneWindow.xaml
    /// </summary>
    public partial class AddDroneWindow : Window
    {
        private IBL.BO.IBL bl;
        public AddDroneWindow(IBL.BO.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            StationIdSelector.ItemsSource = (from station in bl.ListStation()
                                             select station.Id).ToArray();
        }

        private void IdBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!ValidateId(IdBox.Text))
                MakeTextBoxRed(sender as TextBox);
            else
                MakeTextBoxWhite(sender as TextBox);
        }
        private bool ValidateId(string text)
        {
            if (!int.TryParse(text, out int id))
                return false;
            try
            {
                bl.SearchDrone(id);
                return false;
            }
            catch
            { return true; }
        }
        private void ModelBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!ValidateModel(ModelBox.Text))
                MakeTextBoxRed(sender as TextBox);
            else
                MakeTextBoxWhite(sender as TextBox);
        }
        private bool ValidateModel(string text)
        {
            return text != null && text != "";
        }
        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void StationIdSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MakeTextBoxRed(TextBox textBox)
        {
            textBox.Background = Brushes.Red;
            textBox.BorderBrush = Brushes.Red;
        }
        private void MakeTextBoxWhite(TextBox textBox)
        {
            textBox.Background = Brushes.White;
            textBox.BorderBrush = Brushes.Blue;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateId(IdBox.Text) && ValidateModel(ModelBox.Text) && WeightSelector.SelectedIndex != -1 && StationIdSelector.SelectedIndex != -1) 
            {
                int.TryParse(IdBox.Text, out int id);
                try
                {
                    bl.AddDrone(id, ModelBox.Text, (WeightCategories)WeightSelector.SelectedItem, (int)StationIdSelector.SelectedItem);
                    MessageBox.Show("Success");
                    this.Close();
                    return;
                }
                catch
                {  }
            }
            MessageBox.Show("Failure");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
