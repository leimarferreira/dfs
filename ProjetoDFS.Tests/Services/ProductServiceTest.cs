using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjetoDFS.Domain.Models;
using ProjetoDFS.Persistence.Contexts;
using ProjetoDFS.Persistence.Repositories;
using ProjetoDFS.Services;
using ProjetoDFS.Tests.Helpers;

namespace ProjetoDFS.Tests.Services
{
    [TestClass]
    public class ProductServiceTest
    {
        private static ProductService _productService { get; set; }
        private static Company _company { get; set; }
        private static AppDbContext _dbContext { get; set; }

        [ClassInitialize]
        public static async Task InitializeClass(TestContext context)
        {
            _dbContext = DbContextManager.CreateContext();
            var unitOfWork = new UnitOfWork(_dbContext);
            var productRepository = new ProductRepository(_dbContext);
            _productService = new ProductService(productRepository, unitOfWork);

            var companyRepository = new CompanyRepository(_dbContext);
            var companyService = new CompanyService(companyRepository, unitOfWork);
            
            var company = new CompanyBuilder().DefaultCompany().Build();

            _company = (await companyService.SaveAsync(company)).Company;
            await unitOfWork.CompleteAsync();
        }

        [TestCleanup]
        public void CleanTests()
        {
            try
            {
                var companies = _dbContext.Companies.ToArrayAsync().Result;
                _dbContext.Companies.RemoveRange(companies);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        [TestMethod]
        public async Task SaveProduct_ShouldSucceed()
        {
            var product = new ProductBuilder().DefaultProduct().Build();
            var response = await _productService.SaveAsync(product);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task SaveProduct_ShouldFailIfRequiredFieldsAreMissing()
        {
            var product = new ProductBuilder().Build();

            var response = await _productService.SaveAsync(product);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public async Task UpdateProduct_ShouldSucceed()
        {
            var product = new ProductBuilder().DefaultProduct().Build();
            int id = (await _productService.SaveAsync(product)).Product.Id;

            product = new ProductBuilder()
                .DefaultProduct()
                .WithName("Updated name")
                .WithValue(100.0F)
                .WithDescription("Updated description")
                .WithNote("Updated note")
                .Build();

            var response = await _productService.UpdateAsync(id, product);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task UpdateProduct_ShouldFailIfProductDoesntExist()
        {
            var product = new ProductBuilder().DefaultProduct().Build();
            var response = await _productService.UpdateAsync(1000, product);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Product not found.", response.Message);
        }

        [TestMethod]
        public async Task UpdateProduct_ShouldFailIfRequiredFieldsAreMissing()
        {
            var product = new ProductBuilder().DefaultProduct().Build();
            int id = (await _productService.SaveAsync(product)).Product.Id;

            product = new ProductBuilder().Build();

            var response = await _productService.UpdateAsync(id, product);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public async Task DeleteProduct_ShouldSucceed()
        {
            var product = new ProductBuilder()
                .DefaultProduct()
                .Build();
            int id = (await _productService.SaveAsync(product)).Product.Id;
            var response = await _productService.DeleteAsync(id);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task DeleteProduct_ShouldFailIfProductDoesntExist()
        {
            var response = await _productService.DeleteAsync(10000);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Product not found.", response.Message);
        }
    }
}
