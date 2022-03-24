using ECommerceAPI.Application.Repositories.CustomerRep;
using ECommerceAPI.Domain.Entities;
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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly ICustomerWriteRepository _customerWriteRepository;
        public CustomerController(ICustomerReadRepository customerReadRepository, ICustomerWriteRepository customerWriteRepository)
        {
            _customerReadRepository = customerReadRepository;
            _customerWriteRepository = customerWriteRepository;
        }

        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {
            List<Customer> customers = _customerReadRepository.GetAll().ToList();

            return customers;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Customer customer = await _customerReadRepository.GetByIdAsync(id);

            return Ok(customer);
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetByName(string name)
        {
            var item = await _customerReadRepository.GetSingleAsync(m => m.Name.ToLower() == name.ToLower());

            return Ok(item);
        }
 
        [HttpGet("getwherename")]
        public IEnumerable<Customer> GetWhereByName(string name)
        {
            List<Customer> customers = _customerReadRepository.GetWhere(m => m.Name.ToLower() == name.ToLower()).ToList();

            return customers;
        }
        [HttpPost]

        public async Task<IActionResult> Create(Customer customer)
        {
            bool result = await _customerWriteRepository.AddAsync(customer);

            await _customerWriteRepository.SaveAsync();
            return Ok(result);
        }
        [HttpPost("listcustomer")]
        public async Task<IActionResult> AddCustomerList([FromBody] List<Customer> datas)
        {
            bool result = await _customerWriteRepository.AddRangeAsync(datas);
            await _customerWriteRepository.SaveAsync();
            return Ok(result);
        }

        [HttpPut]
        public async Task<bool> Update(Customer customer)
        {
            bool result = _customerWriteRepository.Update(customer);
            await _customerWriteRepository.SaveAsync();
            return result;
        }

        [HttpDelete]
        public async Task<bool> Remove(Customer customer)
        {
            bool result = _customerWriteRepository.Remove(customer);
            await _customerWriteRepository.SaveAsync();
            return result;
        }

        [HttpDelete("deleterange")]
        public async Task<bool> RemoveRange([FromBody] List<Customer> datas)
        {
            bool result = _customerWriteRepository.RemoveRange(datas);
            await _customerWriteRepository.SaveAsync();
            return result;
        }
        [HttpDelete("{id}")]
        public async Task<bool> RemoveById(string id)
        {
            bool result = await _customerWriteRepository.RemoveAsync(id);
            await _customerWriteRepository.SaveAsync();
            return result;
        }


    }
}
