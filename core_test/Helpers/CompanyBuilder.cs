using core.Domain.Models;

namespace core_test.Helpers
{
    public class CompanyBuilder
    {
        private string _tradeName { get; set; }
        private string _legalName { get; set; }
        private string _cnpj { get; set; }

        public CompanyBuilder()
        {

        }

        public CompanyBuilder WithTradeName(string name)
        {
            _tradeName = name;
            return this;
        }

        public CompanyBuilder WithLegalName(string name)
        {
            _legalName = name;
            return this;
        }

        public CompanyBuilder WithCnpj(string cnpj)
        {
            _cnpj = cnpj;
            return this;
        }

        public CompanyBuilder DefaultCompany()
        {
            _tradeName = "Company";
            _legalName = "Company S.A.";
            _cnpj = "18370377000106";

            return this;
        }

        public Company Build()
        {
            return new Company
            {
                TradeName = _tradeName,
                LegalName = _legalName,
                Cnpj = _cnpj
            };
        }
    }
}