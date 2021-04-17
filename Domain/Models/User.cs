using System.Collections.Generic;

namespace ProjetoDFS.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cpf { get; set; }
        IList<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}