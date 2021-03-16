using ProjetoDFS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDFS.Domain.Services
{
    public interface PurchaseService
    {
        Task<IEnumerable<Purchase>> ListAsync();
    }
}
