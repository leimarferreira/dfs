using ProjetoDFS.Domain.Helpers;
using ProjetoDFS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDFS.Resources
{
    public class SavePurchaseResource
    {
        [Required]
        public float Value { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public string Status { get; set; }
        public string Note { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
