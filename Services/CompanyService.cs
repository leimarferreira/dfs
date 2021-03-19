using ProjetoDFS.Domain.Models;
using ProjetoDFS.Domain.Repositories;
using ProjetoDFS.Domain.Services;
using ProjetoDFS.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDFS.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyService(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Company>> ListAsync()
        {
            return await _companyRepository.ListAsync();
        }

        public async Task<SaveCompanyResponse> SaveAsync(Company company)
        {
            try
            {
                await _companyRepository.AddAsync(company);
                await _unitOfWork.CompleteAsync();

                return new SaveCompanyResponse(company);
            }
            catch (Exception ex)
            {
                return new SaveCompanyResponse($"An error has ocurred when saving the company: {ex.Message}");
            }
        }
    }
}
