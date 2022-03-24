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
			if (!_context.Users.Any(x => x.Name == user.Name && x.Password == user.Password))
			{
				return null;
			}

			// Else we generate JSON Web Token
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
			  {
			 new Claim(ClaimTypes.Name, user.Name)
			  }),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Token { Tokens = tokenHandler.WriteToken(token) };

		}
	}
    
}
