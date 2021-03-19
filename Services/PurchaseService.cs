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
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(IPurchaseRepository purchaseRepository, IUnitOfWork unitOfWork)
        {
            _purchaseRepository = purchaseRepository;
            _unitOfWork = unitOfWork;
        }
        public Task<IEnumerable<Purchase>> ListAsync()
        {
            return _purchaseRepository.ListAsync();
        }

        public async Task<SavePurchaseResponse> SaveAsync(Purchase purchase)
        {
            try
            {
                await _purchaseRepository.AddAsync(purchase);
                await _unitOfWork.CompleteAsync();

                return new SavePurchaseResponse(purchase);
            }
            catch (Exception ex)
            {
                return new SavePurchaseResponse($"An error has occurred when saving purchase: {ex.Message}");
            }
        }
    }
}
