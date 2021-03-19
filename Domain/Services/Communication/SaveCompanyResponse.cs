using ProjetoDFS.Domain.Models;

namespace ProjetoDFS.Domain.Services.Communication
{
    public class SaveCompanyResponse : BaseResponse
    {
        public Company Company { get; private set; }

        private SaveCompanyResponse(bool success, string message, Company company) : base(success, message)
        {
            Company = company;
        }

        public SaveCompanyResponse(Company company) : this(true, string.Empty, company)
        {
        }

        public SaveCompanyResponse(string message) : this(false, message, null)
        {

        }
    }
}
