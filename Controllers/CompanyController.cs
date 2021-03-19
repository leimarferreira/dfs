﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoDFS.Domain.Models;
using ProjetoDFS.Domain.Services;
using ProjetoDFS.Extensions;
using ProjetoDFS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDFS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
