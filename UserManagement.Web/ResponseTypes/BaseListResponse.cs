using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Web.ResponseTypes
{
    public class BaseListResponse<T>
    {
        public IEnumerable<T> Records { get; set; }
    }
}
