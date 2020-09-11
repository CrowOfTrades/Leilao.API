using System.Collections.Generic;
using Leilao.Infrastructure.Storage.Storage.Models;
using Leilao.Infrastructure.Storage.Storage.Services;
using Microsoft.AspNetCore.Mvc;

namespace Leilao.API.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : Controller
    {
        private UsersStorageService _storageService = new UsersStorageService();

        [HttpGet("{size}")]
        public List<User> Select([FromRoute] int size)
        {
            return _storageService.SelectMany(size);
        }

        [HttpPost]
        public bool Insert([FromBody] User user)
        {
            _storageService.Insert(user);
            return true;
        }
    }
}
