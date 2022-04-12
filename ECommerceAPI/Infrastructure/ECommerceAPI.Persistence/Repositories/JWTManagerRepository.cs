using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
		private readonly ECommerceAPIDbContext _context;
		private readonly IConfiguration iconfiguration;
		public JWTManagerRepository(IConfiguration iconfiguration, ECommerceAPIDbContext context)
		{
			this.iconfiguration = iconfiguration;
			_context = context;
		}

		
		public Token Authenticate(User user)
		{
			
			if (_context.Users.Any(x => x.Name == user.Name && x.Password == user.Password))
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);

				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
					new Claim(ClaimTypes.Name, user.Name),
				};
				User findeduser = _context.Users.SingleOrDefault(u => u.Password == user.Password);
				if (findeduser.Role!=null)
				{
					claims.Add(new Claim(ClaimTypes.Role, findeduser.Role));
                }
				var tokenDescriptor = new SecurityTokenDescriptor
				{
					Subject = new ClaimsIdentity(claims),
					Expires = DateTime.UtcNow.AddMinutes(10),
					SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
				return new Token { Tokens = tokenHandler.WriteToken(token) };
			}

			else
            {
				return null;
			}

		}
	}
    
}
