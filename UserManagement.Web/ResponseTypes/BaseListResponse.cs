using System.Collections.Generic;

namespace UserManagement.Web.ResponseTypes
{
    public class BaseListResponse<T>
    {
        public IEnumerable<T> Records { get; set; }
    }
}
