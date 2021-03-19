using ProjetoDFS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDFS.Domain.Services.Communication
{
    public class SavePurchaseResponse : BaseResponse
    {
        public Purchase Purchase { get; set; }

        private SavePurchaseResponse(bool success, string message, Purchase purchase) : base(success, message)
        {
            Purchase = purchase;
        }

        public SavePurchaseResponse(Purchase purchase) : this(true, String.Empty, purchase)
        {

        }

        public SavePurchaseResponse(string message) : this(false, message, null)
        {

        }
    }
}
