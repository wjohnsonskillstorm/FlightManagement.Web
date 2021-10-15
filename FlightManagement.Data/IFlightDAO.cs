using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.Data
{
    public interface IFlightDAO
    {
        public IEnumerable<Flight> GetAllRecords();
        public Flight GetRecord(int id);
        public void AddRecord(Flight obj);
        public void DeleteRecord(int id);
        public void UpdateRecord(Flight obj);
    }
}
