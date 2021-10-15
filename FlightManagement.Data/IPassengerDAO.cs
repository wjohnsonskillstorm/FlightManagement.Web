using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.Data
{
    public interface IPassengerDAO
    {
        public IEnumerable<Passenger> GetAllRecords();
        public Passenger GetRecord(int id);
        public void AddRecord(Passenger obj);
        public void DeleteRecord(int id);
        public void UpdateRecord(Passenger obj);
    }
}
