using System.ComponentModel.DataAnnotations;

namespace ProjetoDFS.Resources
{
    public class SaveCompanyResource
    {
        [Required]
        [MaxLength(70)]
        public string TradeName { get; set; }
        [Required]
        [MaxLength(70)]
        public string LegalName { get; set; }
        [Required]
        [MaxLength(14)]
        public string Cnpj { get; set; }
    }
}
