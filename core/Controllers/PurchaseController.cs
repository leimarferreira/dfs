using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using core.Domain.Models;
using core.Domain.Services;
using core.Extensions;
using core.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace core.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IMapper _mapper;

        public PurchaseController(IPurchaseService purchaseService, IMapper mapper)
        {
            _purchaseService = purchaseService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PurchaseResource>> GetAllAsync()
        {
            var purchases = await _purchaseService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Purchase>, IEnumerable<PurchaseResource>>(purchases);

            return resources;
        }

        [HttpGet("{id}")]
        public async Task<PurchaseResource> GetByIdAsync(int id)
        {
            var purchase = await _purchaseService.FindByIdAsync(id);
            var resource = _mapper.Map<Purchase, PurchaseResource>(purchase);

            return resource;
        }

        [HttpGet("byuser/{id}")]
        public async Task<IEnumerable<PurchaseResource>> GetByUserIdAsync(int id)
        {
            var purchases = await _purchaseService.FindByUserIdAsync(id);
            var resources = _mapper.Map<IEnumerable<Purchase>, IEnumerable<PurchaseResource>>(purchases);

            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SavePurchaseResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var purchase = _mapper.Map<SavePurchaseResource, Purchase>(resource);
            var result = await _purchaseService.SaveAsync(purchase);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var purchaseResource = _mapper.Map<Purchase, PurchaseResource>(result.Purchase);

            return Ok(purchaseResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SavePurchaseResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var purchase = _mapper.Map<SavePurchaseResource, Purchase>(resource);
            var result = await _purchaseService.UpdateAsync(id, purchase);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var purchaseResource = _mapper.Map<Purchase, PurchaseResource>(result.Purchase);
            return Ok(purchaseResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _purchaseService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var purchaseResource = _mapper.Map<Purchase, PurchaseResource>(result.Purchase);
            return Ok(purchaseResource);
        }
    }
}
