using ECommerceAPI.Application.Repositories.ProductRep;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController (IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }
        [HttpGet]
        public IEnumerable<Product> GetAll ()
        {
            List<Product> products = _productReadRepository.GetAll().ToList();

            return products;
        }
        [HttpGet("{id}")]
        public async Task <IActionResult>GetById (string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);

            return Ok(product);
        }

        [HttpPost]

        public async Task <IActionResult>Create (Product product)
        {
            bool result =await _productWriteRepository.AddAsync(product);

            await _productWriteRepository.SaveAsync();
            return Ok(result);
        }
        
        [HttpPut]
        public async Task<bool> Update (Product product)
        {
            bool result = _productWriteRepository.Update(product);
           await _productWriteRepository.SaveAsync();
            return result;
        }

        [HttpDelete]
        public async Task<bool> Remove(Product product)
        {
            bool result = _productWriteRepository.Remove(product);
            await _productWriteRepository.SaveAsync();
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<bool> RemoveById(string id)
        {
            bool result = await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return result;
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateRange(List<Product> datas)
        //{
        //    bool result = await _productWriteRepository.AddRangeAsync(datas);


        //    return Ok(result);
        //}
    }
}
