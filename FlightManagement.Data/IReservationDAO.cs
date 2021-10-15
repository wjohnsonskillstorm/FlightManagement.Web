using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.Data
{
    public interface IReservationDAO
    {
        public IEnumerable<Reservation> GetAllRecords();
        public Reservation GetRecord(int id);
        public bool AddRecord(Reservation obj);
        public int CountReservations(Flight obj);
        public IEnumerable<Reservation> GetRecordsForFlight(int flightID);
        public Reservation GetRecordForFlightAndPassenger(int flightID, int passengerID);
        public void DeleteRecord(int id);

    }
}
