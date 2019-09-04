using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Domain.Helpers
{
    class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value,ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Minimum Age is 16!");
            }
            var foo = DateTime.TryParse(value.ToString(), out DateTime date);
            if (date.AddYears(_minimumAge) < DateTime.Now)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"Minimum Age is {_minimumAge}!");
        }
    }
}
