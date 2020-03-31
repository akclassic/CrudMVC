using SessionManagement.Filters;
using SessionManagement.Models;
using System.Configuration;
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
                //EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
                //List<Employee> employees = employeeBusinessLayer.employees.ToList();
                return View();
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
            //if (!ModelState.IsValid)
            //{
            //    ModelState.AddModelError("", "The fields are required");
            //    return View();
            //}
            //else
            //{
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        string query = "INSERT INTO Employee Values( @name, @salary, @designation)";
            //        connection.Open();
            //        using (SqlCommand command = new SqlCommand(query, connection))
            //        {
            //            command.Parameters.AddWithValue("name", employee.Name);
            //            command.Parameters.AddWithValue("salary", employee.Salary);
            //            command.Parameters.AddWithValue("designation", employee.Designation);
            //            command.ExecuteNonQuery();
            //        }
            //    }
            //    ModelState.Clear();
            //    return View();
            //}
            return View("Index");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        //[Route("dashboard/edit/{id}")]
        public ActionResult EditEmployee(int id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ViewResult EditEmployee()
        {
            return View("Index");
        }

        //[Route("dashboard/delete/{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteEmployee(int id)
        {
            return View("Index");
        }
    }
}