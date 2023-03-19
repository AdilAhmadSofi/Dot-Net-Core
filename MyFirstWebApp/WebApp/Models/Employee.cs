using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Employee
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.Currency)]
        public int? Salary { get; set; }
    }
}
