using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.Data
{
    public class ReservationDAO : IReservationDAO
    {
        private string connString = "Data Source=DESKTOP-E90RCFV;Initial Catalog=FlightManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";
        
        public int CountReservations(Flight flight)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "[dbo].[CountReservations]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FlightId", flight.Id);
                try
                {
                    conn.Open();
                    //cmd.ExecuteNonQuery(); no longer applies because we are returning a single value
                    //id = (int)cmd.ExecuteScalar();
                    count = (int)cmd.ExecuteScalar();
                    
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not add Flight!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return count;
        }
        public bool AddRecord(Reservation reservation)
        {
            int id = 0;
            //if(GetRecord(reservation.Id) != null)
            //{
            //    return false;
            //}

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "[dbo].[AddReservation]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if(reservation.flight.PassengerLimit == CountReservations(reservation.flight))
                {
                    return false;
                }
                cmd.Parameters.AddWithValue("@FlightId", reservation.flight.Id);
                cmd.Parameters.AddWithValue("@PassengerId", reservation.passenger.Id);
                cmd.Parameters.AddWithValue("@DateCreated", DateTime.UtcNow);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;

                try
                {
                    conn.Open();
                    //cmd.ExecuteNonQuery(); no longer applies because we are returning a single value
                    //id = (int)cmd.ExecuteScalar();
                    cmd.ExecuteNonQuery();
                    id = (int)cmd.Parameters["@Id"].Value;
                    reservation.Id = id;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not add reservation!\n{0}", ex.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return true;
        }

        public IEnumerable<Reservation> GetAllRecords()
        {
            List<Reservation> reservationList = new List<Reservation>();
            PassengerDAO passengerDao = new PassengerDAO();
            FlightDAO flightDao = new FlightDAO();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Reservations", conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Reservation temp = new Reservation(flightDao.GetRecord(int.Parse(reader["FlightId"].ToString())), passengerDao.GetRecord(int.Parse(reader["PassengerId"].ToString())));
                        temp.Id = Convert.ToInt32(reader["Id"]);
                        reservationList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all reservations!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return reservationList;
        }

        public Reservation GetRecord(int id)
        {
            Reservation tmp = new Reservation();
            PassengerDAO passengerDao = new PassengerDAO();
            FlightDAO flightDao = new FlightDAO();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetReservation]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tmp = new Reservation(flightDao.GetRecord(int.Parse(reader["FlightId"].ToString())), passengerDao.GetRecord(int.Parse(reader["PassengerId"].ToString())));
                        tmp.Id = id;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get reservation!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return tmp;
        }

        public IEnumerable<Reservation> GetRecordsForFlight(int flightID)
        {
            List<Reservation> reservationList = new List<Reservation>();
            PassengerDAO passengerDao = new PassengerDAO();
            FlightDAO flightDao = new FlightDAO();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Reservations", conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if(int.Parse(reader["FlightId"].ToString()) == flightID)
                        {
                            Reservation temp = new Reservation(flightDao.GetRecord(int.Parse(reader["FlightId"].ToString())), passengerDao.GetRecord(int.Parse(reader["PassengerId"].ToString())));
                            temp.Id = Convert.ToInt32(reader["Id"]);
                            reservationList.Add(temp);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all reservations!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return reservationList;
        }
        public Reservation GetRecordForFlightAndPassenger(int flightID, int passengerID)
        {
            Reservation reservation = new Reservation();
            PassengerDAO passengerDao = new PassengerDAO();
            FlightDAO flightDao = new FlightDAO();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Reservations", conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        if (int.Parse(reader["FlightId"].ToString()) == flightID && int.Parse(reader["PassengerId"].ToString()) == passengerID)
                        {
                            Reservation temp = new Reservation(flightDao.GetRecord(int.Parse(reader["FlightId"].ToString())), passengerDao.GetRecord(int.Parse(reader["PassengerId"].ToString())));
                            temp.Id = Convert.ToInt32(reader["Id"]);
                            reservation = temp;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all reservations!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return reservation;
        }
        public void DeleteRecord(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[DeleteReservation]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if(rows == 0)
                    {
                        Console.WriteLine("Zero rows affected!");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get reservation!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
