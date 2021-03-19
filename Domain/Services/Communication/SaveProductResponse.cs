using ProjetoDFS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDFS.Domain.Services.Communication
{
    public class SaveProductResponse : BaseResponse
    {
        public Product Product { get; set; }

        private SaveProductResponse(bool success, string message, Product product) : base(success, message)
        {
            Product = product;
        }

        public SaveProductResponse(Product product) : this(true, String.Empty, product)
        {

        }

        public SaveProductResponse(String message) : this(false, message, null)
        {

        }
    }
}
