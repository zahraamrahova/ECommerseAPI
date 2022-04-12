using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Application.Repositories.UserRep;
using ECommerceAPI.Domain.Entities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
		private readonly IJWTManagerRepository _jWTManager;
		private readonly IUserWriteRepository _userWriteRepository;
		private readonly IUserReadRepository _userReadRepository;
		public UsersController(IJWTManagerRepository jWTManager, IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository)
		{
			this._jWTManager = jWTManager;
			_userReadRepository = userReadRepository;
			_userWriteRepository = userWriteRepository;
		}

		
		[HttpGet]
		public IEnumerable<User> GetAll()
		{
			List<User> users = _userReadRepository.GetAll().ToList();

			return users;
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Create(User user)
		{
			if (_userReadRepository.GetAll().Any(x => x.Name == user.Name && x.Password == user.Password))
			{
				return Ok("this user already registered");
			}

			bool result = await _userWriteRepository.AddAsync(user);
			await _userWriteRepository.SaveAsync();

			return Ok(result);
		}


		[AllowAnonymous]
		[HttpPost]
		[Route("authenticate")]
		public IActionResult Authenticate(User usersdata)
		{
			var token = _jWTManager.Authenticate(usersdata);

			if (token == null)
			{
				return Unauthorized();
			}

			return Ok(token);
		}
	}
}
