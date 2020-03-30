using SessionManagement.Filters;
using SessionManagement.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace SessionManagement.Controllers
{

    public class DashboardController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sampledbconnection"].ConnectionString;

        // GET: Dashboard
        [LoginAuthentication]
        [Route("dashboard")]
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
                List<Employee> employees = employeeBusinessLayer.employees.ToList();
                return View(employees);
            }
        }

        [HttpGet]
        //[Route("dashboard/add")]
        [Authorize(Roles = "admin")]
        public ActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "The fields are required");
                return View();
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Employee Values( @name, @salary, @designation)";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("name", employee.Name);
                        command.Parameters.AddWithValue("salary", employee.Salary);
                        command.Parameters.AddWithValue("designation", employee.Designation);
                        command.ExecuteNonQuery();
                    }
                }
                ModelState.Clear();
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        //[Route("dashboard/edit/{id}")]
        public ActionResult EditEmployee(int id)
        {
            Employee employee = new Employee();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "Select name, salary, designation from Employee WHERE id=@id";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        employee.Name = sqlDataReader["name"].ToString();
                        employee.Designation = sqlDataReader["designation"].ToString();
                        employee.Salary = Convert.ToDecimal(sqlDataReader["salary"]);
                    }
                }
            }
            return View(employee);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditEmployee(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Employee SET name = @name, salary = @salary, designation = @designation WHERE id=@id";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", employee.Id) ;
                    command.Parameters.AddWithValue("name", employee.Name);
                    command.Parameters.AddWithValue("salary", employee.Salary);
                    command.Parameters.AddWithValue("designation", employee.Designation);
                    command.ExecuteNonQuery();
                }
            }
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            List<Employee> employees = employeeBusinessLayer.employees.ToList();
            return View("Index", employees);
        }

        //[Route("dashboard/delete/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteEmployee(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM EMPLOYEE WHERE id=@id";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.ExecuteNonQuery();
                }
            }
            EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            List<Employee> employees = employeeBusinessLayer.employees.ToList();
            return View("Index", employees);
        }
    }
}