using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly string connString = @"server=MERLIN\SQLEXPRESS;integrated security=true;database=CoreBatchDB;";
        public async Task<IActionResult> Index()
        {
            using SqlConnection conn = new(connString);
            SqlCommand cmd = new("spGetStudents", conn);
            await conn.OpenAsync();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
            List<Student> students = new List<Student>();
            Student student = null!;
            while (dataReader.Read())
            {
                student = new Student();
                student.Id = Convert.ToInt32(dataReader["Id"]);
                student.Name = (string)dataReader["Name"];
                student.Gender = (Gender)(Convert.ToInt32(dataReader["Gender"]));
                student.Contact = (string)dataReader["Contact"];
                student.Email = (string)dataReader["Email"];
                students.Add(student);
            };
            return View(students);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {

            SqlConnection conn = new(connString);
            SqlCommand cmd = new("spSetStudent", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@gender", student.Gender);
            cmd.Parameters.AddWithValue("@contact", student.Contact);
            cmd.Parameters.AddWithValue("@email", student.Email);

            await conn.OpenAsync();
            int returnValue = await cmd.ExecuteNonQueryAsync();
            if (returnValue > 0)
                ViewBag.ReturnStatus = SqlStatus.Success;
            else
                ViewBag.ReturnStatus = SqlStatus.Failure;
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            using SqlConnection conn = new(connString);
            SqlCommand cmd = new("spGetStudent", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandType = CommandType.StoredProcedure;
            await conn.OpenAsync();
            SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
            Student student = null!;
            if (await dataReader.ReadAsync())
            {
                student = new()
                {
                    Id = Convert.ToInt32(dataReader["Id"]),
                    Name = (string)(dataReader["Name"]),
                    Gender = (Gender)Convert.ToInt32(dataReader["Gender"]),
                    Contact = (string)(dataReader["Contact"]),
                    Email = (string)(dataReader["Email"]),
                };
            }
            return View(student);
        }
        public async Task<IActionResult> Edit(int id)
        {
            using SqlConnection conn = new(connString);
            SqlCommand cmd = new("spGetStudent", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandType = CommandType.StoredProcedure;
            await conn.OpenAsync();
            SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
            Student student = null!;
            if (await dataReader.ReadAsync())
            {
                student = new()
                {
                    Id = Convert.ToInt32(dataReader["Id"]),
                    Name = (string)(dataReader["Name"]),
                    Gender = (Gender)Convert.ToInt32(dataReader["Gender"]),
                    Contact = (string)(dataReader["Contact"]),
                    Email = (string)(dataReader["Email"]),
                };
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Student student)
        {
            using SqlConnection conn = new(connString);
            SqlCommand cmd = new("spUpdateStudent", conn);
            cmd.Parameters.AddWithValue("@id", student.Id);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@gender", student.Gender);
            cmd.Parameters.AddWithValue("@contact", student.Contact);
            cmd.Parameters.AddWithValue("@email", student.Email);
            await conn.OpenAsync();
            cmd.CommandType = CommandType.StoredProcedure;
            int returnValue = await cmd.ExecuteNonQueryAsync();
            if (returnValue > 0)
                return RedirectToAction(nameof(Index));
            ViewBag.ReturnStatus = SqlStatus.Failure;
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            using SqlConnection conn = new(connString);
            SqlCommand cmd = new("spGetStudent", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandType = CommandType.StoredProcedure;
            await conn.OpenAsync();
            SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
            Student student = null!;
            if (await dataReader.ReadAsync())
            {
                student = new()
                {
                    Id = Convert.ToInt32(dataReader["Id"]),
                    Name = (string)(dataReader["Name"]),
                    Gender = (Gender)Convert.ToInt32(dataReader["Gender"]),
                    Contact = (string)(dataReader["Contact"]),
                    Email = (string)(dataReader["Email"]),
                };
            }
            return View(student);

        }
        public async Task<IActionResult> Delete(Student student)
        {
            using SqlConnection conn = new(connString);
            SqlCommand cmd = new SqlCommand("spDeleteStudent", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", student.Id);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
