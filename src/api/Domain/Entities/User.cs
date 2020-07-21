using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Entities
{
    public class User : EntityBase
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}
