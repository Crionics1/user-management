using System;

namespace UserManagement.Domain.Helpers
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message):base(message: message)
        {

        }
    }
}
