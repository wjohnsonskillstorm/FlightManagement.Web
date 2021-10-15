using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.Data
{
    public class Reservation
    {
        public int Id { get; set; }
        public Flight flight { get; set; }
        public Passenger passenger { get; set; }
        public Reservation() { }
        public Reservation(Flight flight, Passenger passenger)
        {
            this.flight = flight;
            this.passenger = passenger;
        }

    }
}
