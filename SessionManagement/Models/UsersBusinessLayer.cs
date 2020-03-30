using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SessionManagement.Models
{
    public class UsersBusinessLayer
    {
        public IEnumerable<Users> users
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["sampledbconnection"].ConnectionString;
                List<Users> users = new List<Users>();
                using(SqlConnection con = new SqlConnection(connectionString))
                {
                    using(SqlCommand command = new SqlCommand("Select * from Users", con))
                    {
                        con.Open();
                        SqlDataReader sqlDataReader = command.ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                            Users user = new Users();
                            user.Id = Convert.ToInt32(sqlDataReader["id"]);
                            user.Email = sqlDataReader["email"].ToString();
                            user.Username = sqlDataReader["username"].ToString();
                            user.Password = sqlDataReader["password"].ToString();
                            users.Add(user);
                        }
                    }
                }
                return users;
            }
        }
    }
}