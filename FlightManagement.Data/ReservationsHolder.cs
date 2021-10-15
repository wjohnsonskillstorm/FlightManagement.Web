using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.Data
{
    public class ReservationsHolder
    {
        public List<Reservation> reservations { get; set; } = new List<Reservation>();
        public Flight flight { get; set; }
        public int SelectedPassenger { get; set; }
        public List<Passenger> passengers { get; set; }
        public ReservationsHolder() { }
        public ReservationsHolder(Flight flight, List<Reservation> reservations)
        {
            this.flight = flight;
            this.reservations = reservations;
        }
    }
}
