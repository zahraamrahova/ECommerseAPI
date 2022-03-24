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
			List<User> orders = _userReadRepository.GetAll().ToList();

			return orders;
		}

		[HttpPost]
		public async Task<IActionResult> Create(User order)
		{
			bool result = await _userWriteRepository.AddAsync(order);
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
