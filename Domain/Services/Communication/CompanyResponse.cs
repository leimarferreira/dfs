using ProjetoDFS.Domain.Models;

namespace ProjetoDFS.Domain.Services.Communication
{
    public class CompanyResponse : BaseResponse
    {
        public Company Company { get; private set; }

        private CompanyResponse(bool success, string message, Company company) : base(success, message)
        {
            Company = company;
        }

        public CompanyResponse(Company company) : this(true, string.Empty, company)
        {
        }

        public CompanyResponse(string message) : this(false, message, null)
        {

        }
    }
}
