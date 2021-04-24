using System;
using ProjetoDFS.Domain.Helpers;

namespace ProjetoDFS.Domain.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public float Value { get; set; }
        public DateTime Date { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PurchaseStatus Status { get; set; }
        public string Note { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int UserId { get; set; }
        public User Buyer { get; set; }
    }
}