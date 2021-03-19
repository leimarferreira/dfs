using ProjetoDFS.Domain.Helpers;
using ProjetoDFS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDFS.Resources
{
    public class SavePurchaseResource
    {
        public float Value { get; set; }
        public DateTime Date { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PurchaseStatus Status { get; set; }
        public string Note { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public Product Product { get; set; }
        public User Buyer { get; set; }
    }
}
