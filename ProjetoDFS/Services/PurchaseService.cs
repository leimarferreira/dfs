using ProjetoDFS.Domain.Helpers;
using ProjetoDFS.Domain.Models;
using ProjetoDFS.Domain.Repositories;
using ProjetoDFS.Domain.Services;
using ProjetoDFS.Domain.Services.Communication;
using System;
using System.Collections.Generic;
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
        public async Task<IEnumerable<Purchase>> ListAsync()
        {
            return await _purchaseRepository.ListAsync();
        }

        public async Task<Purchase> FindByIdAsync(int id)
        {
            return await _purchaseRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<Purchase>> FindByUserIdAsync(int id)
        {
            return await _purchaseRepository.FindByUserIdAsync(id);
        }

        public async Task<PurchaseResponse> SaveAsync(Purchase purchase)
        {
            try
            {
                purchase.Status = PurchaseStatus.Complete;
                await _purchaseRepository.AddAsync(purchase);
                await _unitOfWork.CompleteAsync();

                return new PurchaseResponse(purchase);
            }
            catch (Exception ex)
            {
                return new PurchaseResponse($"An error has occurred when saving purchase: {ex.Message}");
            }
        }

        public async Task<PurchaseResponse> UpdateAsync(int id, Purchase purchase)
        {
            var existingPurchase = await _purchaseRepository.FindByIdAsync(id);

            if (existingPurchase == null)
            {
                return new PurchaseResponse("Purchase not found.");
            }

            existingPurchase.Value = purchase.Value;
            existingPurchase.Date = purchase.Date;
            existingPurchase.PaymentMethod = purchase.PaymentMethod;
            existingPurchase.Status = purchase.Status;
            existingPurchase.Note = purchase.Note;
            existingPurchase.PostalCode = purchase.PostalCode;
            existingPurchase.Address = purchase.Address;
            existingPurchase.Product = purchase.Product;
            existingPurchase.Buyer = purchase.Buyer;

            try
            {
                _purchaseRepository.Update(existingPurchase);
                await _unitOfWork.CompleteAsync();

                return new PurchaseResponse(existingPurchase);
            }
            catch (Exception ex)
            {
                return new PurchaseResponse($"An error has occurred when updating the purchase: {ex.Message}");
            }
        }

        public async Task<PurchaseResponse> DeleteAsync(int id)
        {
            var existingPurchase = await _purchaseRepository.FindByIdAsync(id);

            if (existingPurchase == null)
            {
                return new PurchaseResponse("Purchase not found.");
            }

            try
            {
                _purchaseRepository.Remove(existingPurchase);
                await _unitOfWork.CompleteAsync();

                return new PurchaseResponse(existingPurchase);
            }
            catch (Exception ex)
            {
                return new PurchaseResponse($"An error has occurred when deleting the purchase: {ex.Message}");
            }
        }
    }
}
