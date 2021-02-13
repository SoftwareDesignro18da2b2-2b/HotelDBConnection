using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace HotelDBConnection
{
    class DBClient
    {
        string connectionString = "Data Source = (localdb)\\ProjectsV13;Initial Catalog = HotelDB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        public void Start()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryStringAllHotels = "SELECT * FROM DemoHotel";
                SqlCommand command = new SqlCommand(queryStringAllHotels, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Listing all hotels:");
                while (reader.Read())
                {
                    int id = reader.GetInt32(0); //læser int fra første søjle
                    string name = reader.GetString(1); //læser string fra anden søjle
                    string address = reader.GetString(2);

                    Console.WriteLine($"ID: {id}, Name: {name}, Address: {address}"); ;
                }
                reader.Close();
                Console.WriteLine();
                
                int HotelNo = 3;
                string queryStringOneHotel = $"SELECT * FROM DemoHotel WHERE Hotel_No = {HotelNo}";
                Console.WriteLine($"Finding hotel #{HotelNo}");
                
                command = new SqlCommand(queryStringOneHotel, connection);
                reader = command.ExecuteReader();
                
                if (reader.Read())
                {
                    int id = reader.GetInt32(0); //læser int fra første søjle
                    string name = reader.GetString(1); //læser string fra anden søjle
                    string address = reader.GetString(2);

                    Console.WriteLine($"ID: {id}, Name: {name}, Address: {address}"); ;
                }
                reader.Close();
                Console.WriteLine();

                int hotelNo = 8;
                string hotelName = "New Hotel";
                string hotelAddress = "Maglegaardsvej 2, 4000 Roskilde";
                string insertCommandString = $"INSERT INTO DemoHotel VALUES({hotelNo}, '{hotelName}', '{hotelAddress}')";
                Console.WriteLine($"Create hotel #{hotelNo}");
                command = new SqlCommand(insertCommandString, connection);
                int numberOfRowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
                Console.WriteLine();
                
                Console.WriteLine($"Updating hotel #{hotelNo}");
                hotelName += " (updated)";
                hotelAddress += " (updated)";
                string updateCommandString = $"UPDATE DemoHotel SET Name='{hotelName}', Address='{hotelAddress}' WHERE Hotel_No = {hotelNo}" ;
                command = new SqlCommand(updateCommandString, connection);
                numberOfRowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
                Console.WriteLine();

                Console.WriteLine($"Deleting hotel #{hotelNo}");
                string deleteCommandString = $"DELETE FROM DemoHotel  WHERE Hotel_No = {hotelNo}";
                command = new SqlCommand(deleteCommandString, connection);
                numberOfRowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Number of rows affected: {numberOfRowsAffected}");
                Console.WriteLine();
            }
        }
    }
}
