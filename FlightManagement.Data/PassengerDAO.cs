using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagement.Data
{
    public class PassengerDAO : IPassengerDAO
    {
        private string connString = "Data Source=DESKTOP-E90RCFV;Initial Catalog=FlightManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";
        public void AddRecord(Passenger passenger)
        {
            int id = 0;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "[dbo].[AddPassenger]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FirstName", passenger.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", passenger.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", passenger.LastName);
                cmd.Parameters.AddWithValue("@Birthday", passenger.Birthday);
                cmd.Parameters.AddWithValue("@Email", passenger.Email);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Direction = ParameterDirection.Output;

                try
                {
                    conn.Open();
                    //cmd.ExecuteNonQuery(); no longer applies because we are returning a single value
                    //id = (int)cmd.ExecuteScalar();
                    cmd.ExecuteNonQuery();
                    id = (int)cmd.Parameters["@Id"].Value;
                    passenger.Id = id;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not add Passenger!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IEnumerable<Passenger> GetAllRecords()
        {
            List<Passenger> passengerList = new List<Passenger>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Passenger", conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Passenger temp = new Passenger(reader["FirstName"].ToString(), reader["MiddleName"].ToString(), reader["LastName"].ToString(), DateTime.Parse(reader["Birthday"].ToString()), reader["Email"].ToString());
                        temp.Id = Convert.ToInt32(reader["Id"]);
                        passengerList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all passengers!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return passengerList;
        }

        public Passenger GetRecord(int id)
        {
            Passenger tmp = new Passenger();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[GetPassenger]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tmp = new Passenger(reader["FirstName"].ToString(), reader["MiddleName"].ToString(), reader["LastName"].ToString(), DateTime.Parse(reader["Birthday"].ToString()), reader["Email"].ToString());
                        tmp.Id = id;
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get passenger!\n{0}", ex.Message);
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
                SqlCommand cmd = new SqlCommand("[dbo].[DeletePassenger]", conn);
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
                    Console.WriteLine("Could not get passenger!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void UpdateRecord(Passenger obj)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[UpdatePassenger]", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@FirstName", obj.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", obj.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", obj.LastName);
                cmd.Parameters.AddWithValue("@Birthday", obj.Birthday);
                cmd.Parameters.AddWithValue("@Email", obj.Email);
             

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
                    Console.WriteLine("Could not get passenger!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
