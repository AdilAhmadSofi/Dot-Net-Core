using System.Collections;
using WebApp.Utilities;

namespace WebApp.Models
{
    public class Student
    {
        public int? Id { get; set; }
        public string Name { get; set; } = null!;
        public Gender Gender { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; } = null!;
    }

    public class Examination : Student
    {
        public (String, String) Result(Grade Grade)
        {
            string result = "";
            string percentage = "";
            switch (Grade)
            {
                case Grade.A:
                    result = "Distinction";
                    percentage = "> 90%";
                    break;

                case Grade.B:
                    result = "Pass";
                    percentage = "> 80%";
                    break;

                case Grade.C:
                    result = "Pass";
                    percentage = "> 70%";
                    break;

                case Grade.D:
                    result = "Fail";
                    percentage = "< 50%";
                    break;

                case Grade.E:
                    result = "Fail";
                    percentage = "< 40%";
                    break;
            }
            return (result, percentage);
        }
    }
}

