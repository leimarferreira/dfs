using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoDFS.Persistence.Contexts;
using ProjetoDFS.Persistence.Repositories;
using ProjetoDFS.Services;
using ProjetoDFS.Tests.Helpers;

namespace ProjetoDFS.Tests.Services
{
    [TestClass]
    public class CompanyServiceTest
    {
        private static CompanyService _companyService { get; set; }
        private static AppDbContext _dbContext { get; set; }

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _dbContext = DbContextManager.CreateContext();
            var unitOfWork = new UnitOfWork(_dbContext);
            var companyRepository = new CompanyRepository(_dbContext);
            _companyService = new CompanyService(companyRepository, unitOfWork);
        }

        [TestCleanup]
        public void Clean()
        {
            try
            {
                var companies = _dbContext.Companies.ToArrayAsync().Result;
                _dbContext.Companies.RemoveRange(companies);
                _dbContext.SaveChanges();
            }
            catch (Exception) {

            }
        }

        [TestMethod]
        public async Task SaveCompany_ShouldSucceed()
        {
            var company = new CompanyBuilder().DefaultCompany().Build();
            var response = await _companyService.SaveAsync(company);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task SaveCompany_ShouldFailIfRequiredFieldsAreMissing()
        {
            var company = new CompanyBuilder().Build();
            var response = await _companyService.SaveAsync(company);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public async Task SaveCompany_ShouldFailIfCnpjIsInvalid()
        {
            var company = new CompanyBuilder()
                .DefaultCompany()
                .WithCnpj("00000000000000")
                .Build();

            var response = await _companyService.SaveAsync(company);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Invalid CNPJ.", response.Message);
        }

        [TestMethod]
        public async Task SaveCompany_ShouldFailIfCnpjIsAlreadyInUse()
        {
            var company = new CompanyBuilder().DefaultCompany().Build();
            await _companyService.SaveAsync(company);

            company = new CompanyBuilder().DefaultCompany().Build();

            var response = await _companyService.SaveAsync(company);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("CNPJ already in use.", response.Message);
        }

        [TestMethod]
        public async Task UpdateCompany_ShouldSucceed()
        {
            var company = new CompanyBuilder().DefaultCompany().Build();
            int id = (await _companyService.SaveAsync(company)).Company.Id;

            company = new CompanyBuilder()
                .WithTradeName("Updated Company")
                .WithLegalName("Updated Company LLC.")
                .WithCnpj("99322038000148")
                .Build();

            var response = await _companyService.UpdateAsync(id, company);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task UpdateCompany_ShouldFailIfRequiredFieldsAreMissing()
        {
            var company = new CompanyBuilder().DefaultCompany().Build();
            int id =  (await _companyService.SaveAsync(company)).Company.Id;

            company = new CompanyBuilder().Build();

            var response = await _companyService.UpdateAsync(id, company);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public async Task UpdateCompany_ShouldFailIfCnpjIsInvalid()
        {
            var company = new CompanyBuilder().DefaultCompany().Build();
            int id = (await _companyService.SaveAsync(company)).Company.Id;

            company = new CompanyBuilder()
                .DefaultCompany()
                .WithCnpj("00000000000000")
                .Build();

            var response = await _companyService.UpdateAsync(id, company);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Invalid CNPJ.", response.Message);
        }

        [TestMethod]
        public async Task UpdateCompany_ShouldFailIfCnpjIsAlreadyInUse()
        {
            var company = new CompanyBuilder().DefaultCompany().Build();
            int id = (await _companyService.SaveAsync(company)).Company.Id;

            var anotherCompany = new CompanyBuilder()
                .DefaultCompany()
                .WithCnpj("13579933000127")
                .Build();
            
            await _companyService.SaveAsync(anotherCompany);

            company = new CompanyBuilder()
                .DefaultCompany()
                .WithCnpj("13579933000127")
                .Build();

            var response = await _companyService.UpdateAsync(id, company);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("CNPJ already in use by another company.", response.Message);
        }

        [TestMethod]
        public async Task UpdateCompany_ShouldFailIfCompanyDoesntExist()
        {
            var company = new CompanyBuilder().DefaultCompany().Build();
            var response = await _companyService.UpdateAsync(10000, company);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Company not found.", response.Message);
        }

        [TestMethod]
        public async Task DeleteCompany_ShouldSucceed()
        {
            var company = new CompanyBuilder().DefaultCompany().Build();
            int id = (await _companyService.SaveAsync(company)).Company.Id;

            var response = await _companyService.DeleteAsync(id);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task DeleteCompany_ShouldFailIfCompanyDoesntExist()
        {
            var response = await _companyService.DeleteAsync(10000);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Company not found.", response.Message);
        }

        [TestMethod]
        public async Task DeleteCompany_ShouldDeleteAllCompanyProducts()
        {
            var company = new CompanyBuilder().DefaultCompany().Build();
            int id = (await _companyService.SaveAsync(company)).Company.Id;

            var productRepository = new ProductRepository(_dbContext);
            await productRepository.AddAsync(
                new ProductBuilder()
                .DefaultProduct()
                .WithCompany(company)
                .Build());
            await productRepository.AddAsync(
                new ProductBuilder()
                .DefaultProduct()
                .WithCompany(company)
                .Build());
            await productRepository.AddAsync(
                new ProductBuilder()
                .DefaultProduct()
                .WithCompany(company)
                .Build());
            _dbContext.SaveChanges();
            Assert.AreNotEqual(0, (await _dbContext.Products.ToArrayAsync()).Length);
            
            var response = await _companyService.DeleteAsync(id);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(0, (await _dbContext.Products.ToArrayAsync()).Length);
        }
    }
}
