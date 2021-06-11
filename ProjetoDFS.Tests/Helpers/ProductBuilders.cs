using ProjetoDFS.Domain.Models;
using ProjetoDFS.Tests.Helpers;

namespace ProjetoDFS.Tests
{
    public class ProductBuilder
    {
        private string _name { get; set; }
        private string _description { get; set; }
        private float _value { get; set; }
        private string _note { get; set; }
        private Company _company { get; set; }

        public ProductBuilder()
        {

        }

        public ProductBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ProductBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public ProductBuilder WithValue(float value)
        {
            _value = value;
            return this;
        }

        public ProductBuilder WithNote(string note)
        {
            _note = note;
            return this;
        }

        public ProductBuilder WithCompany(Company company)
        {
            _company = company;
            return this;
        }

        public ProductBuilder DefaultProduct()
        {
            _name = "Product";
            _description = "Description of the product";
            _value = 500.0F;
            _note = "Random note";
            _company = new CompanyBuilder().DefaultCompany().Build();

            return this;
        }

        public Product Build()
        {
            return new Product
            {
                Name = _name,
                Description = _description,
                Value = _value,
                Note = _note,
                Company = _company
            };
        }
    }
}