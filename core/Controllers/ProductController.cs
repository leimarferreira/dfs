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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResource>> GetAllAsync()
        {
            var products = await _productService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);

            return resources;
        }

        [HttpGet("{id}")]
        public async Task<ProductResource> GetByIdAsync(int id)
        {
            var product = await _productService.FindByIdAsync(id);
            var resource = _mapper.Map<Product, ProductResource>(product);

            return resource;
        }

        [HttpGet("bycompany/{id}")]
        public async Task<IEnumerable<ProductResource>> GetByCompanyIdAsync(int id)
        {
            var products = await _productService.FindByCompanyIdAsync(id);
            var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);

            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveProductResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var product = _mapper.Map<SaveProductResource, Product>(resource);
            var result = await _productService.SaveAsync(product);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var productResource = _mapper.Map<Product, ProductResource>(result.Product);

            return Ok(productResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveProductResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var product = _mapper.Map<SaveProductResource, Product>(resource);
            var result = await _productService.UpdateAsync(id, product);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var productResource = _mapper.Map<Product, ProductResource>(result.Product);
            return Ok(productResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _productService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var productResource = _mapper.Map<Product, ProductResource>(result.Product);
            return Ok(productResource);
        }
    }
}
