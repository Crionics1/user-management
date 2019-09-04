using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UserManagement.Domain.Helpers
{
    class ComplexPasswordAttribute : ValidationAttribute 
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (value.ToString().Any(char.IsDigit))
                if (value.ToString().Any(char.IsUpper))
                    if (value.ToString().Any(char.IsLower))
                    {
                        return true;
                    }
            ErrorMessage = "Password should contain at least: 1 uppercase, 1 lowercase and 1 digit!";
            return false;
        }
    }
}
