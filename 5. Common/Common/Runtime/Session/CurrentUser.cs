using System;
using System.Collections.Generic;

namespace Common.Runtime.Session
{
    public class CurrentUser
    {
        public static CurrentUser Current { get; set; }

        public int Id { get; set; }

        public string UserName { get; set; }

        public int FK_EmployeeId { get; set; }

        public int FK_CompanyId { get; set; }

    }
}
