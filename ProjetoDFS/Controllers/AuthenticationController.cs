using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjetoDFS.Domain.Models;
using ProjetoDFS.Domain.Services;
using ProjetoDFS.Extensions;
using ProjetoDFS.Resources;
using ProjetoDFS.Utils;
using System;
using System.Threading.Tasks;

namespace ProjetoDFS.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("/api/[controller]")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> VerifyLogin([FromBody] AuthUserResource resource)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.GetErrorMessages());
                }

                var user = _mapper.Map<AuthUserResource, User>(resource);
                var response = await _userService.FindByCredentials(user.Email, user.Password);

                if (!response.Success)
                {
                    return BadRequest(response.Message);
                }

                var token = CryptoFunctions.GenerateToken(_configuration, user);

                return Ok(new
                {
                    error = false,
                    result = new
                    {
                        token,
                        user = new { response.User.Id, response.User.Email }
                    }
                });
            }
            catch (Exception ex)
            {
                var message = $"Error during login: {ex}";
                return BadRequest(new { error = true, result = new { message } });
            }
        }
    }
}
