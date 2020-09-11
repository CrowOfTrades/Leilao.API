using System;
using System.Collections.Generic;
using System.Text;

namespace Leilao.Infrastructure.Storage.Storage.Models
{
    public class Bid
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public decimal Price { get; set; }

        public DateTime CreateOn { get; set; }

        public string UserName { get; set; }

        public string ProductName { get; set; }
    }
}
