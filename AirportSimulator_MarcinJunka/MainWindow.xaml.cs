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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirportSimulator_MarcinJunka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ControlTower tower = new ControlTower();
        public MainWindow()
        {
            InitializeComponent();
            tower = new ControlTower(); // add args
            InitializeGUI();
        }

        private void InitializeGUI()
        {
            this.Title = "Airport Simulator by Marcin Junka";
            txtName.Text = "Boeing 737";
            txtDestination.Text = "New York";
            txtFlightTime.Text = "2";
            txtflightId.Text = "ISP206";
        }

        private void UpdateGUI()
        {

        }

        private void btnAddPlane_Click(object sender, RoutedEventArgs e)
        {
            Airplane airplane = new Airplane();
            airplane.Name = txtName.Text;
            airplane.Destination = txtDestination.Text;
            airplane.FlightId = txtflightId.Text;
            double parseResult;
            bool parseOk = double.TryParse(txtFlightTime.Text, out parseResult);

            if (parseOk)
            {
                airplane.FlightTime = parseResult;
                tower.AddPlane(airplane);
                UpdatePlanesList();
            }
            else
            {
                MessageBox.Show("Not valid flight time");
            }
        }

        private void btnTakeOff_Click(object sender, RoutedEventArgs e)
        {
            Airplane plane = (Airplane)lstPlanes.SelectedItem; // selecting the plane
            if (plane != null)
            {
                plane.TakeOff += Plane_TakeOff;
                tower.orderTakeOff(lstPlanes.SelectedIndex);
                // UNSUBSCRIBE IMPORTANT! without this you get into loop
                plane.TakeOff -= Plane_TakeOff; 
            }
            else
            {
                MessageBox.Show("You didnt pick the plane!");
            }
            //plane.OnTakeOff(new AirplaneEventArgs("Taking off",plane.Name));

        }

        private void Plane_TakeOff(object? sender, AirplaneEventArgs e)
        {
            Airplane sndr = (Airplane)sender;
            
            // SETTING AIRPLANE'S TIME
            sndr.LocalTime = DateTime.Now;

            lstEvents.Items.Add($"{sndr.Name} is taking off destination: {sndr.Destination}, {sndr.LocalTime}");

            sndr.Landed += Sndr_Landed;
        }

        private void Sndr_Landed(object? sender, AirplaneEventArgs e)
        {
            Airplane plane = (Airplane)sender;
            lstEvents.Items.Add($"{plane.Name} has landed in {plane.Destination}, {e.Message}");

            // UNSUBSCRIBE IMPORTANT! without this you get into loop
            plane.Landed -= Sndr_Landed;
        }

        private void UpdatePlanesList()
        {
            lstPlanes.ItemsSource = null;
            lstPlanes.ItemsSource = tower.GetPlanes();
        }


    }
}
