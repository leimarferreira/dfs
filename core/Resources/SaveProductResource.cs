using System.ComponentModel.DataAnnotations;

namespace core.Resources
{
    public class SaveProductResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public float Value { get; set; }
        [MaxLength(100)]
        public string Note { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public string ImageDataURL { get; set; }
    }
}
