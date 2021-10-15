using FlightManagement.Data;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace FlightManagement.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            FlightDAO flightDao = new FlightDAO();
            PassengerDAO passengerDao = new PassengerDAO();
            ReservationDAO reservationDao = new ReservationDAO();
            //List<Flight> flights = (List<Flight>)flightDao.GetAllRecords();

            //foreach(Flight flight in flights)
            //{
            //    Console.WriteLine(flight.FlightNumber);
            //    Console.WriteLine(flight.ArrivalAirport);
            //    Console.WriteLine(flight.DepartureAirport);
            //    Console.WriteLine(flight.ArrivalDateTime);
            //    Console.WriteLine(flight.DepartureDateTime);
            //}
            var cultureInfo = new CultureInfo("en-US");
            //flightDao.AddRecord(new Flight(4057, DateTime.Now, new DateTime(2021, 10, 5, 13, 35, 26), "TPA", "MCO", 107));

            //passengerDao.AddRecord(new Passenger("Dan","Stan","Pickles", new DateTime(1989, 5, 26), "danpickles@email.com"));
            //passengerDao.AddRecord(new Passenger("Stan", null, "Pickles", new DateTime(1956, 8, 5), "danpickles@email.com"));
            //List<Passenger> passengers = (List<Passenger>)passengerDao.GetAllRecords();
            //foreach (Passenger passenger in passengers)
            //{
            //    Console.WriteLine(passenger.FirstName);
            //}

            //Passenger passenger = passengerDao.GetRecord(1);

            //Console.WriteLine(passenger.FirstName);

            //flightDao.AddRecord(new Flight(506, new DateTime(2021, 11, 25, 5, 6, 30), new DateTime(2021, 11, 25, 9, 30, 23), "TPA", "MIA", 1));
            Reservation reservation = new Reservation(flightDao.GetRecord(1003), passengerDao.GetRecord(1));
            if (reservationDao.AddRecord(reservation))
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Invalid reservation, this flight is already full!");
            }
            //List<Reservation> reservations = (List<Reservation>)reservationDao.GetAllRecords();
            //foreach(Reservation reservation in reservations)
            //{
            //    Console.WriteLine("Flight ID:"+reservation.flight.Id);
            //    Console.WriteLine("Passenger ID:"+reservation.passenger.Id);
            //}
            //Reservation reservation = reservationDao.GetRecord(1);

            //Console.WriteLine(reservation.flight.Id);
            //Console.WriteLine(reservation.passenger.Id);

        }
    }
}
