using ECommerceAPI.Application.Repositories.ProductRep;
using ECommerceAPI.Application.Repositories.UserRep;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories.UserRep
{
    public class UserWriteRepository : WriteRepository<User>, IUserWriteRepository
    {
        public UserWriteRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
