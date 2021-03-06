using System.Collections.Generic;

namespace core.Domain.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string TradeName { get; set; }
        public string LegalName { get; set; }
        public string Cnpj { get; set; }
        public string ImageDataURL { get; set; }
        public IList<Product> Products { get; set; } = new List<Product>();
    }
}
