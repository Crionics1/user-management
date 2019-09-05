using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserManagement.Domain.Helpers
{
    class NameAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (Regex.IsMatch(value.ToString(), @"^[a-zA-Z]+$"))
            {
                return true;
            }
            ErrorMessage = "Name must contain only characters";
            return false;
        }
    }
}
