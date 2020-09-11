using System;
using System.Collections.Generic;
using Leilao.Infrastructure.Storage.Storage.Models;
using Leilao.Infrastructure.Storage.Storage.Services;
using Microsoft.AspNetCore.Mvc;

namespace Leilao.API.Controllers
{
    [ApiController]
    [Route("Bid")]
    public class BidController : Controller
    {
        private BidsStorageService _storageService = new BidsStorageService();
        private ProductsStorageService _productsStorageService = new ProductsStorageService();

        [HttpGet("{size}")]
        public List<Bid> Select([FromRoute] int size)
        {
            return _storageService.SelectMany(size);
        }

        [HttpGet("{size}/user/{userId}")]
        public List<Bid> Select([FromRoute] int size, [FromRoute] Guid userId)
        {
            return _storageService.SelectByUser(size, userId);
        }

        [HttpPost]
        public bool Insert([FromBody] Bid bid)
        {
            _storageService.Insert(bid);

            _productsStorageService.UpdatePrice(bid.ProductId, bid.Price) ;
            return true;
        }
    }
}
