using System.ComponentModel.DataAnnotations;

namespace core.Resources
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
        [MinLength(14)]
        public string Cnpj { get; set; }
        public string ImageDataURL { get; set; }
    }
}
