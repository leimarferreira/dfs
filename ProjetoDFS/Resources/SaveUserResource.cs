using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDFS.Resources
{
    public class SaveUserResource
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(16)]
        public string Password { get; set; }
        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string Cpf { get; set; }
    }
}
