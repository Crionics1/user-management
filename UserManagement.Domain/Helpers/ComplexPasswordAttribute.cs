using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UserManagement.Domain.Attributes
{
    class ComplexPasswordAttribute : ValidationAttribute 
    {
        public override bool IsValid(object value)
        {
            if (value.ToString().Any(char.IsDigit))
                if (value.ToString().Any(char.IsUpper))
                    if (value.ToString().Any(char.IsLower))
                    {
                        return true;
                    }
            return false;
        }
    }
}
