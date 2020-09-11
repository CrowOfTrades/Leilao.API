using System.Collections.Generic;
using Leilao.Infrastructure.Storage.Storage.Models;
using Leilao.Infrastructure.Storage.Storage.Services;
using Microsoft.AspNetCore.Mvc;

namespace Leilao.API.Controllers
{
    [ApiController]
    [Route("Product")]
    public class ProductController : Controller
    {
        private ProductsStorageService _storageService = new ProductsStorageService();

        [HttpGet("{size}")]
        public List<Product> Select([FromRoute] int size)
        {
            return _storageService.SelectMany(size);
        }

        [HttpPost]
        public bool Insert([FromBody] Product product)
        {
            _storageService.Insert(product);
            return true;
        }
    }
}
