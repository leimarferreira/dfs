using ProjetoDFS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDFS.Domain.Services.Communication
{
    public class ProductResponse : BaseResponse
    {
        public Product Product { get; set; }

        private ProductResponse(bool success, string message, Product product) : base(success, message)
        {
            Product = product;
        }

        public ProductResponse(Product product) : this(true, String.Empty, product)
        {

        }

        public ProductResponse(String message) : this(false, message, null)
        {

        }
    }
}
