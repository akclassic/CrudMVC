using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SessionManagement.Models
{
    public class UserRolesBusinessLayer
    {
        public IEnumerable<UserRoles> userRoles
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["sampledbconnection"].ConnectionString;
                List<UserRoles> userRoles = new List<UserRoles>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("Select * from Userrole", con))
                    {
                        con.Open();
                        SqlDataReader sqlDataReader = command.ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                            UserRoles userrole = new UserRoles();
                            userrole.Id = Convert.ToInt32(sqlDataReader["id"]);
                            userrole.UserRole = sqlDataReader["userrole"].ToString();
                            userrole.UserId = Convert.ToInt32(sqlDataReader["userid"]);
                            userRoles.Add(userrole);
                        }
                    }
                }
                return userRoles;
            }
        }
    }
}