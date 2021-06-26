using System;
using System.ComponentModel.DataAnnotations;

namespace core.Resources
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
