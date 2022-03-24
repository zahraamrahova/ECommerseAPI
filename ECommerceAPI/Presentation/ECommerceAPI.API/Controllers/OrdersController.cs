using ECommerceAPI.Application.Repositories.OrderRep;
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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;

        public OrdersController(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderReadRepository = orderReadRepository;
            _orderWriteRepository = orderWriteRepository;
        }

        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            List<Order> orders = _orderReadRepository.GetAll().ToList();

            return orders;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById (string id)
        {
            Order order = await _orderReadRepository.GetByIdAsync(id);

            return Ok(order);
        }

        [HttpGet("description")]
        public async Task<IActionResult> GetByName(string description)
        {
            var item = await _orderReadRepository.GetSingleAsync(m => m.Description.ToLower() == description.ToLower());

            return Ok(item);
        }

        [HttpGet("address")]
        public async Task<IActionResult> GetbyStock(string address)
        {
            var item = await _orderReadRepository.GetSingleAsync(m => m.Address == address);

            return Ok(item);
        }

        [HttpGet("getwheredescription")]
        public IEnumerable<Order> GetWhereByStock(string description)
        {
            List<Order> orders = _orderReadRepository.GetWhere(m => m.Description == description).ToList();

            return orders;
        }
        [HttpGet("getwhereAddress")]
        public IEnumerable<Order> GetWhereByName(string address)
        {
            List<Order> orders = _orderReadRepository.GetWhere(m => m.Address.ToLower() == address.ToLower()).ToList();

            return orders;
        }

        [HttpPost("listorderlist")]
        public async Task<IActionResult> AddOrdertList([FromBody] List<Order> datas)
        {
            bool result = await _orderWriteRepository.AddRangeAsync(datas);
            await _orderWriteRepository.SaveAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create (Order order)
        {
            bool result = await _orderWriteRepository.AddAsync(order);
            await _orderWriteRepository.SaveAsync();

            return Ok(result);
        }
        [HttpPut]
        public async Task<bool> Update(Order order)
        {
            bool result =  _orderWriteRepository.Update(order);
            await _orderWriteRepository.SaveAsync();
            return result;
        } 

        [HttpDelete]
        public async Task<bool> Delete (Order order)
        {
            bool result = _orderWriteRepository.Remove(order);
            await _orderWriteRepository.SaveAsync();
            return result;
        }
        [HttpDelete("deleterange")]
        public async Task<bool> RemoveRange([FromBody] List<Order> datas)
        {
            bool result = _orderWriteRepository.RemoveRange(datas);
            await _orderWriteRepository.SaveAsync();
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteById (string id)
        {
            bool result= await _orderWriteRepository.RemoveAsync(id);
            await _orderWriteRepository.SaveAsync();
            return result;
        }
    }
}
