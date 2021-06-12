using System;
using ProjetoDFS.Domain.Helpers;
using ProjetoDFS.Domain.Models;

namespace ProjetoDFS.Tests.Helpers
{
    public class PurchaseBuilder
    {
        private float _value { get; set; }
        private DateTime _date { get; set; }
        private PaymentMethod _paymentMethod { get; set; }
        private PurchaseStatus _status { get; set; }
        private string _note { get; set; }
        private string _postalCode { get; set; }
        private string _address { get; set; }
        private Product _product { get; set; }
        private User _buyer { get; set; }

        public PurchaseBuilder()
        {

        }

        public PurchaseBuilder WithValue(float value)
        {
            _value = value;
            return this;
        }

        public PurchaseBuilder WithDate(DateTime date)
        {
            _date = date;
            return this;
        }

        public PurchaseBuilder WithPaymentMethod(PaymentMethod method)
        {
            _paymentMethod = method;
            return this;
        }

        public PurchaseBuilder WithStatus(PurchaseStatus status)
        {
            _status = status;
            return this;
        }

        public PurchaseBuilder WithNote(string note)
        {
            _note = note;
            return this;
        }

        public PurchaseBuilder WithPostalCode(string code)
        {
            _postalCode = code;
            return this;
        }

        public PurchaseBuilder WithAddress(string address)
        {
            _address = address;
            return this;
        }

        public PurchaseBuilder WithProduct(Product product)
        {
            _product = product;
            return this;
        }

        public PurchaseBuilder WithBuyer(User buyer)
        {
            _buyer = buyer;
            return this;
        }

        public PurchaseBuilder DefaultPurchase()
        {
            _product = new ProductBuilder().DefaultProduct().Build();
            _value = _product.Value;
            _date = DateTime.Now;
            _paymentMethod = PaymentMethod.Credit;
            _status = PurchaseStatus.Complete;
            _note = "Random note";
            _postalCode = "00000-000";
            _address = "St. Some Street, 123";
            _buyer = new UserBuilder().DefaultUser().Build();

            return this;
        }

        public Purchase Build()
        {
            return new Purchase
            {
                Value = _value,
                Date = _date,
                PaymentMethod = _paymentMethod,
                Status = _status,
                Note = _note,
                PostalCode = _postalCode,
                Address = _address,
                Product = _product,
                Buyer = _buyer
            };
        }
    }
}