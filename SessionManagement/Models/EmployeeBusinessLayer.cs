using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SessionManagement.Models
{
    public class EmployeeBusinessLayer
    {
        public IEnumerable<Employee> employees
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["sampledbconnection"].ConnectionString;
                List<Employee> employees = new List<Employee>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("Select * from Employee", con))
                    {
                        con.Open();
                        SqlDataReader sqlDataReader = command.ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                            Employee employee = new Employee();
                            employee.Id = Convert.ToInt32(sqlDataReader["id"]);
                            employee.Name = sqlDataReader["name"].ToString();
                            employee.Salary = Convert.ToDecimal(sqlDataReader["salary"]);
                            employee.Designation = sqlDataReader["designation"].ToString();
                            employees.Add(employee);
                        }
                    }
                }
                return employees;
            }
        }
    }
}