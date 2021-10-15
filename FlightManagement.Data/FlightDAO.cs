using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace FlightManagement.Data
{
    public class FlightDAO : IFlightDAO
    {
        private string connString = "Data Source=DESKTOP-E90RCFV;Initial Catalog=FlightManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";
        public void AddRecord(Flight flight)
        {
            int id = 0;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "[dbo].[AddFlight]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FlightNumber", flight.FlightNumber);
                cmd.Parameters.AddWithValue("@DepartureDateTime", flight.DepartureDateTime.ToUniversalTime());
                cmd.Parameters.AddWithValue("@ArrivalDateTime", flight.ArrivalDateTime.ToUniversalTime());
                cmd.Parameters.AddWithValue("@DepartureAirport", flight.DepartureAirport);
                cmd.Parameters.AddWithValue("@ArrivalAirport", flight.ArrivalAirport);
                cmd.Parameters.AddWithValue("@PassengerLimit", flight.PassengerLimit);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;

                try
                {
                    conn.Open();
                    //cmd.ExecuteNonQuery(); no longer applies because we are returning a single value
                    //id = (int)cmd.ExecuteScalar();
                    cmd.ExecuteNonQuery();
                    id = (int)cmd.Parameters["@Id"].Value;
                    flight.Id = id;
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
        }

        public IEnumerable<Flight> GetAllRecords()
        {
            List<Flight> flightList = new List<Flight>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Flight", conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Flight temp = new Flight(int.Parse(reader["FlightNumber"].ToString()), DateTime.Parse(reader["DepartureDateTime"].ToString()), DateTime.Parse(reader["ArrivalDateTime"].ToString()), reader["DepartureAirport"].ToString(), reader["ArrivalAirport"].ToString(), int.Parse(reader["PassengerLimit"].ToString()));
                        temp.Id = Convert.ToInt32(reader["Id"]);
                        flightList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all flights!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return flightList;
        }

        public Flight GetRecord(int id)
        {
            Flight tmp = new Flight();
            using(SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetFlight]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tmp = new Flight(int.Parse(reader["FlightNumber"].ToString()), DateTime.Parse(reader["DepartureDateTime"].ToString()), DateTime.Parse(reader["ArrivalDateTime"].ToString()), reader["DepartureAirport"].ToString(), reader["ArrivalAirport"].ToString(), int.Parse(reader["PassengerLimit"].ToString()));
                        tmp.Id = id;
                    }
                }
                catch(SqlException ex)
                {
                    Console.WriteLine("Could not get flight!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return tmp;
        }

        public void DeleteRecord(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[DeleteFlight]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        Console.WriteLine("Zero rows affected!");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get flight!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void UpdateRecord(Flight obj)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[UpdateFlight]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@FlightNumber", obj.FlightNumber);
                cmd.Parameters.AddWithValue("@DepartureDateTime", obj.DepartureDateTime);
                cmd.Parameters.AddWithValue("@ArrivalDateTime", obj.ArrivalDateTime);
                cmd.Parameters.AddWithValue("@DepartureAirport", obj.DepartureAirport);
                cmd.Parameters.AddWithValue("@ArrivalAirport", obj.ArrivalAirport);
                cmd.Parameters.AddWithValue("@PassengerLimit", obj.PassengerLimit);

                try
                {
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        Console.WriteLine("Zero rows affected!");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get flight!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
