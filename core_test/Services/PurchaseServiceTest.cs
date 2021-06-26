using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using core.Domain.Helpers;
using core.Persistence.Contexts;
using core.Persistence.Repositories;
using core.Services;
using core_test.Helpers;
using System;
using System.Threading.Tasks;

namespace core_test.Services
{
    [TestClass]
    public class PurchaseServiceTest
    {
        private static PurchaseService _purchaseService { get; set; }
        private static AppDbContext _dbContext { get; set; }

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            var dbContext = DbContextManager.CreateContext();
            var unitOfWork = new UnitOfWork(dbContext);
            var purchaseRepository = new PurchaseRepository(dbContext);
            _purchaseService = new PurchaseService(purchaseRepository, unitOfWork);
        }

        [TestCleanup]
        public void CleanTests()
        {
            try
            {
                var purchases = _dbContext.Purchases.ToArrayAsync().Result;
                _dbContext.Purchases.RemoveRange(purchases);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        [TestMethod]
        public async Task SavePurchase_ShouldSucceed()
        {
            var purchase = new PurchaseBuilder().DefaultPurchase().Build();
            var response = await _purchaseService.SaveAsync(purchase);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task SavePurchase_ShouldFailIfRequiredFieldsAreMissing()
        {
            var purchase = new PurchaseBuilder().Build();

            var response = await _purchaseService.SaveAsync(purchase);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public async Task UpdatePurchase_ShouldSucceed()
        {
            var purchase = new PurchaseBuilder().DefaultPurchase().Build();
            int id = (await _purchaseService.SaveAsync(purchase)).Purchase.Id;

            purchase = new PurchaseBuilder()
                .DefaultPurchase()
                .WithStatus(PurchaseStatus.Denied)
                .Build();

            var response = await _purchaseService.UpdateAsync(id, purchase);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task UpdatePurchase_ShouldFailIfRequiredFieldsAreMissing()
        {
            var purchase = new PurchaseBuilder().DefaultPurchase().Build();
            int id = (await _purchaseService.SaveAsync(purchase)).Purchase.Id;

            purchase = new PurchaseBuilder().Build();

            var response = await _purchaseService.UpdateAsync(id, purchase);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public async Task UpdatePurchase_ShouldFailIfPurchaseDoesntExist()
        {
            var purchase = new PurchaseBuilder().DefaultPurchase().Build();
            var response = await _purchaseService.UpdateAsync(10000, purchase);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Purchase not found.", response.Message);
        }

        [TestMethod]
        public async Task DeletePurchase_ShouldSucceed()
        {
            var purchase = new PurchaseBuilder().DefaultPurchase().Build();
            int id = (await _purchaseService.SaveAsync(purchase)).Purchase.Id;

            var response = await _purchaseService.DeleteAsync(id);
            Assert.IsTrue(response.Success);
        }

        [TestMethod]
        public async Task DeletePurchase_ShouldFailIfPurchaseDoesntExist()
        {
            var response = await _purchaseService.DeleteAsync(10000);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Purchase not found.", response.Message);
        }
    }
}