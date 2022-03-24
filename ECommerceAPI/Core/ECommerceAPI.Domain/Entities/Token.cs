using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Common
{
    public class Token: BaseEntity
    {
        
        public string Tokens { get; set; }
        public string RefreshTokens { get; set; }
    }
}
