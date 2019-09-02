using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Domain.Helpers
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message: message)
        {

        }
    }
}
