using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Food_Donation_App.Pages
{
    public class clientModel : PageModel
    {
        public List<ClientInfo> ListClient = new List<ClientInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\DESKTOP-B20I9OD;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        Console.WriteLine("Connection successful! work");

                        // Continue with your database operations
                        String sql = "SELECT * FROM clienttb";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ClientInfo clientInfo = new ClientInfo
                                    {
                                        Id = reader.GetInt32(0),
                                        Name = reader.GetString(1),
                                        email = reader.GetString(2),
                                        phone = reader.GetString(3),
                                        username = reader.GetString(4),
                                        password = reader.GetString(5)
                                    };

                                    ListClient.Add(clientInfo);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Connection failed!");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (log or show an error message)
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    public class ClientInfo
    {
        public int Id;
        public  string Name;
        public string email;
        public string phone;
        public string username;
        public string password;
    }
}
