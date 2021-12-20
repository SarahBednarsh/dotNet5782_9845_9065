﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {
        private readonly IBL bl;
        public StationWindow(IBL bl, ObservableCollection<Station> stations)
        {
            InitializeComponent();
        }
        public StationWindow(IBL bl, ObservableCollection<Station> stations, int stationId)
        {
            InitializeComponent();
        }
    }
}