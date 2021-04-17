using System.ComponentModel.DataAnnotations;

namespace ProjetoDFS.Resources
{
    public class AuthUserResource
    {
        [Required]
        [MaxLength(70)]
        public string Email { get; set; }
        [Required]
        [MaxLength(16)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}