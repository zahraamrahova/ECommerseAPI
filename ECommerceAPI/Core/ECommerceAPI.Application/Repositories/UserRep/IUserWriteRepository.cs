using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Domain.Entities;
using ECommerceAPI.Domain.Entities.Common;

namespace ECommerceAPI.Application.Repositories.UserRep
{
    public interface IUserWriteRepository: IWriteRepository<User>
    {
    }
}
