using System;

namespace core.Resources
{
    public class PurchaseResource
    {
        public int Id { get; set; }
        public float Value { get; set; }
        public DateTime Date { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public ProductResource Product { get; set; }
        public UserResource Buyer { get; set; }
    }
}
