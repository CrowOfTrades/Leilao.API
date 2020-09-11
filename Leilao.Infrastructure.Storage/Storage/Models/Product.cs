using System;

namespace Leilao.Infrastructure.Storage.Storage.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
