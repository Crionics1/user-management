using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserManagement.Domain.Attributes
{
    class NameAttribute: ValidationAttribute
    {
        private int _length;
        public NameAttribute(int length)
        {
            _length = length;
        }

        public override bool IsValid(object value)
        {
            if (Regex.IsMatch(value.ToString(), @"^[a-zA-Z]+$") && _length >= 2)
            {
                return true;
            }
            ErrorMessage = "Name must be more than 2 characters long and must contain only characters";
            return false;
        }
    }
}
