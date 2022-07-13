using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectPerson.Common.DTOs;
using ProjectPerson.Common.Models;
using ProjectPerson.Service.IService;
using ProjectPerson.WebApi.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProjectPerson.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountSrvice;
        private readonly IAuthenticateService _authenticateService;

        public AccountController(IAccountService accountService, IAuthenticateService authenticateService)
        {
            _accountSrvice = accountService;
            _authenticateService = authenticateService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(AuthenticateRequestDTO dto)
        {
            var response = await _authenticateService.Authenticate(dto);

            if (String.IsNullOrEmpty(response.Token))
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("login-google")]
        public async Task<IActionResult> LoginWithGoogle(RegisterDTO dto)
        {
            try
            {
                bool isRegistred = await _accountSrvice.Regeister(dto, Enums.PersonType.Customer);

                AuthenticateRequestDTO dtoAuth = new AuthenticateRequestDTO
                {
                    Username = dto.UserName,
                    Password = ""
                };
                var response = await _authenticateService.Authenticate(dtoAuth);

                if (String.IsNullOrEmpty(response.Token))
                {
                    return BadRequest(new { message = "Username or password is incorrect" });
                }

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }
        }

        [HttpPost]
        [Route("register-deliverer")]
        public async Task<IActionResult> RegisterDeliverer(RegisterDTO dto)
        {
            try
            {
                bool isRegistred = await _accountSrvice.Regeister(dto, Enums.PersonType.Deliverer);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }

            return Ok(new { message = "User successfully register." });
        }

        [HttpPost]
        [Route("register-customer")]
        public async Task<IActionResult> RegisterCustomer(RegisterDTO dto)
        {
            try
            {
                bool isRegistred = await _accountSrvice.Regeister(dto, Enums.PersonType.Customer);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }

            return Ok(new { message = "User successfully register." });
        }
    }
}
