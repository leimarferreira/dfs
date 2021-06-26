using core.Domain.Models;
using System;

namespace core.Domain.Services.Communication
{
    public class PurchaseResponse : BaseResponse
    {
        public Purchase Purchase { get; set; }

        private PurchaseResponse(bool success, string message, Purchase purchase) : base(success, message)
        {
            Purchase = purchase;
        }

        public PurchaseResponse(Purchase purchase) : this(true, String.Empty, purchase)
        {

        }

        public PurchaseResponse(string message) : this(false, message, null)
        {

        }
    }
}
