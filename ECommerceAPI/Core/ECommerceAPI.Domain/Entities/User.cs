﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Common
{
   public class User:BaseEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
        //public Guid RoleId { get; set; }
       // public ICollection<Role> Roles { get; set; }
    }
}
