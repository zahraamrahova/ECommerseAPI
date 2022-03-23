using ECommerceAPI.Application.Repositories.ProductRep;
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
        public async void Get ()
        {
           await _productWriteRepository.AddRangeAsync(new()
            {
               new() { Id=Guid.NewGuid(), Name= "Product1", CreatedDate=DateTime.Now, Price=100, Stock=10},
                new() { Id = Guid.NewGuid(), Name = "Product2", CreatedDate = DateTime.Now, Price = 1010, Stock = 40 },
                 new() { Id = Guid.NewGuid(), Name = "Product3", CreatedDate = DateTime.Now, Price = 10, Stock = 20 },
                  new() { Id = Guid.NewGuid(), Name = "Product4", CreatedDate = DateTime.Now, Price = 130, Stock = 30 }
           });
            await _productWriteRepository.SaveAsync();
        }

    }
}
