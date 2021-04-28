﻿using ProjetoDFS.Domain.Models;
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

        public async Task<Company> FindByIdAsync(int id)
        {
            return await _companyRepository.FindByIdAsync(id);
        }

        public async Task<CompanyResponse> SaveAsync(Company company)
        {
            try
            {
                await _companyRepository.AddAsync(company);
                await _unitOfWork.CompleteAsync();

                return new CompanyResponse(company);
            }
            catch (Exception ex)
            {
                return new CompanyResponse($"An error has ocurred when saving the company: {ex.Message}");
            }
        }

        public async Task<CompanyResponse> UpdateAsync(int id, Company company)
        {
            var existingCompany = await _companyRepository.FindByIdAsync(id);

            if (existingCompany == null)
            {
                return new CompanyResponse("Company not found.");
            }

            existingCompany.TradeName = company.TradeName;
            existingCompany.LegalName = company.LegalName;
            existingCompany.Cnpj = company.Cnpj;
            existingCompany.Products = company.Products;

            try
            {
                _companyRepository.Update(existingCompany);
                await _unitOfWork.CompleteAsync();

                return new CompanyResponse(existingCompany);
            }
            catch (Exception ex)
            {
                return new CompanyResponse($"An error has occurred when updating the company: {ex.Message}");
            }
        }

        public async Task<CompanyResponse> DeleteAsync(int id)
        {
            var existingCompany = await _companyRepository.FindByIdAsync(id);

            if (existingCompany == null)
            {
                return new CompanyResponse("Company not found.");
            }

            try
            {
                _companyRepository.Remove(existingCompany.Id);
                await _unitOfWork.CompleteAsync();

                return new CompanyResponse(existingCompany);
            }
            catch (Exception ex)
            {
                return new CompanyResponse($"An error has occurred when deleting the company: {ex.Message}");
            }
        }
    }
}
