using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Assignment_2_ApuAnimalPark.Objects.ListManager;

namespace AirportSimulator_MarcinJunka
{
    public class ControlTower
    {
        private ListManager<Airplane> flights;

        public List<Airplane> Flights { get; }

        public ControlTower()
        {
            flights = new ListManager<Airplane>();
        }

        public void AddPlane(Airplane airplane)
        {
            if (airplane != null)
            {
                flights.Add(airplane);
            }
        }

        public List<Airplane> GetPlanes()
        {
            List<Airplane> airplanes = new List<Airplane>();

            for (int i = 0; i < flights.Count; i++)
            {
                airplanes.Add(flights.GetAt(i));
            }

            return airplanes;
        }

        public void orderTakeOff(int index)
        {
            Airplane airplane = flights.GetAt(index);

            if (index >= 0 && airplane != null)
            {
                airplane.OnTakeOff(new AirplaneEventArgs("Taking off", airplane.LocalTime.ToString()));
            }
        }


    }
}
