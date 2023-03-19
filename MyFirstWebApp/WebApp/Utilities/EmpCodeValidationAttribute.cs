using System.ComponentModel.DataAnnotations;

namespace WebApp.Utilities
{
    public class EmpCodeValidationAttribute : ValidationAttribute
    {
        private string _code { get; set; }
        public EmpCodeValidationAttribute(string code)
        {
            _code = code;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ?? "Default Message");
        }
    }
}
