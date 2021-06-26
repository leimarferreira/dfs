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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CompanyResource>> GetAllAsync()
        {
            var companies = await _companyService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Company>, IEnumerable<CompanyResource>>(companies);

            return resources;
        }

        [HttpGet("{id}")]
        public async Task<CompanyResource> GetByIdAsync(int id)
        {
            var company = await _companyService.FindByIdAsync(id);
            var resource = _mapper.Map<Company, CompanyResource>(company);

            return resource;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCompanyResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var company = _mapper.Map<SaveCompanyResource, Company>(resource);
            var result = await _companyService.SaveAsync(company);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var companyResource = _mapper.Map<Company, CompanyResource>(result.Company);

            return Ok(companyResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCompanyResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var company = _mapper.Map<SaveCompanyResource, Company>(resource);
            var result = await _companyService.UpdateAsync(id, company);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var companyResouce = _mapper.Map<Company, CompanyResource>(result.Company);

            return Ok(companyResouce);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _companyService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var companyResource = _mapper.Map<Company, CompanyResource>(result.Company);
            return Ok(companyResource);
        }
    }
}
