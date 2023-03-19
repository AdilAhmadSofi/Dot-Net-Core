using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApp.Models;


namespace WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly string connString;
        public EmployeeController()
        {
            connString = @"server=MERLIN\SQLEXPRESS;integrated security=true;database=CoreBatchDB;";
        }
        public async Task<IActionResult> Index()
        {
            using SqlConnection conn = new(connString);
            SqlCommand cmd = new("spGetEmployees", conn);

            conn.Open();
            SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
            List<Employee> employees = new List<Employee>();
            Employee employee = null!;
            while (dataReader.Read())
            {
                employee = new Employee();
                employee.Id = Convert.ToInt32(dataReader["Id"]);
                employee.Name = dataReader["Name"].ToString();
                employee.Gender = await dataReader.IsDBNullAsync("Gender") ? Gender.Male : (Gender)(Convert.ToUInt16(dataReader["gender"]));
                employee.Email = dataReader["Email"].ToString();
                employee.Salary = dataReader.IsDBNull("Salary") ? 0 : Convert.ToInt32(dataReader["Salary"]);
                employees.Add(employee);
            }
            return View(employees);
        }
        public async Task<IActionResult> Details(int id)
        {
            using SqlConnection conn = new(connString);
            SqlCommand cmd = new("spGetEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
            Employee employee = null!;
            if (await dataReader.ReadAsync())
            {
                employee = new Employee();
                employee.Id = Convert.ToInt32(dataReader["Id"]);
                employee.Name = dataReader["Name"].ToString();
                employee.Gender = await dataReader.IsDBNullAsync("Gender") ? Gender.Male : (Gender)(Convert.ToInt16(dataReader["gender"]));
                employee.Email = dataReader["Email"].ToString();
                employee.Salary = dataReader.IsDBNull("Salary") ? 0 : Convert.ToInt32(dataReader["Salary"]);
            }
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            using SqlConnection conn = new(connString);
            SqlCommand cmd = new("spGetEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
            Employee employee = null!;
            if (await dataReader.ReadAsync())
            {
                employee = new Employee();
                employee.Id = Convert.ToInt32(dataReader["Id"]);
                employee.Name = dataReader["Name"].ToString();
                employee.Gender = await dataReader.IsDBNullAsync("Gender") ? Gender.Male : (Gender)(Convert.ToInt16(dataReader["gender"]));
                employee.Email = dataReader["Email"].ToString();
                employee.Salary = dataReader.IsDBNull("Salary") ? 0 : Convert.ToInt32(dataReader["Salary"]);
            }
            return View(employee);
        }

        public async Task<IActionResult> Update(Employee employee)
        {
            using SqlConnection conn = new(connString);
            SqlCommand cmd = new("spUpdateEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", employee.Id);
            cmd.Parameters.AddWithValue("@name", employee.Name);
            cmd.Parameters.AddWithValue("@gender", employee.Gender);
            cmd.Parameters.AddWithValue("@email", employee.Email);
            cmd.Parameters.AddWithValue("@salary", employee.Salary);
            await conn.OpenAsync();
            int returnValue = await cmd.ExecuteNonQueryAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            using SqlConnection conn = new(connString);
            string query = "spSetEmployee";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@name", employee.Name);
            cmd.Parameters.AddWithValue("@gender", employee.Gender);
            cmd.Parameters.AddWithValue("@email", employee.Email);
            cmd.Parameters.AddWithValue("@salary", employee.Salary);
            ViewBag.ReturnValue = await cmd.ExecuteNonQueryAsync();

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            using SqlConnection conn = new(connString);
            SqlCommand cmd = new("spGetEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
            Employee employee = null!;
            if (await dataReader.ReadAsync())
            {
                employee = new Employee();
                employee.Id = Convert.ToInt32(dataReader["Id"]);
                employee.Name = dataReader["Name"].ToString();
                employee.Gender = await dataReader.IsDBNullAsync("Gender") ? Gender.Male : (Gender)(Convert.ToInt16(dataReader["gender"]));
                employee.Email = dataReader["Email"].ToString();
                employee.Salary = dataReader.IsDBNull("Salary") ? 0 : Convert.ToInt32(dataReader["Salary"]);
            }
            return View(employee);
        }
        public async Task<IActionResult> Delete(Employee employee)
        {
            using SqlConnection conn = new(connString);
            string query = "spDeleteEmployee";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@id", employee.Id);

            ViewBag.ReturnValue = await cmd.ExecuteNonQueryAsync();
            return View();
        }
    }


}
