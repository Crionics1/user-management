using System;

namespace UserManagement.Domain.Helpers
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message: message)
        {

        }
    }
}
