using System;

namespace Leilao.Infrastructure.Storage.Storage.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthdate { get; set; }

        public DateTime CreateOn { get; set; }

        public int Age
        {
            get
            {
                DateTime today = DateTime.Now;
                int age = today.Year - Birthdate.Year;
                if (Birthdate.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
    }
}
