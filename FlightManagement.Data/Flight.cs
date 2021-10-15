using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.Data
{
    public class Flight
    {
        public int Id { get; set; }
        [Required]
        public int FlightNumber { get; set; }
        [Required]
        public DateTime DepartureDateTime { get; set; }
        [Required]
        public DateTime ArrivalDateTime { get; set; }
        [Required]
        public string DepartureAirport { get; set; }
        [Required]
        public string ArrivalAirport { get; set; }
        [Required]
        public int PassengerLimit { get; set; }

        public Flight() { }
        public Flight(int FlightNumber, DateTime DepartureDateTime, DateTime ArrivalDateTime, string DepartureAirport, string ArrivalAirport, int PassengerLimit)
        {
            this.FlightNumber = FlightNumber;
            this.DepartureDateTime = DepartureDateTime;
            this.ArrivalDateTime = ArrivalDateTime;
            this.DepartureAirport = DepartureAirport;
            this.ArrivalAirport = ArrivalAirport;
            this.PassengerLimit = PassengerLimit;
        }
    }
}
