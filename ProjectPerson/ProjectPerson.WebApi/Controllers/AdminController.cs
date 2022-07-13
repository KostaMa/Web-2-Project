using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProjectPerson.Service.IService;
using ProjectPerson.WebApi.Authorization;
using ProjectPerson.Common.DTOs;

namespace ProjectPerson.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize("Admin")]
        [HttpPost]
        [Route("accept")]
        public async Task<IActionResult> AcceptAccount(AcceptPersonDTO dto)
        {
            try
            {
                await _adminService.AcceptAccount(dto.idPerson);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentNullException ne)
            {
                return BadRequest(new { message = ne.Message });
            }
            catch (FormatException fe)
            {
                return BadRequest(new { message = fe.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }

            //send on notif on email about acc
            return Ok();
        }

        [Authorize("Admin")]
        [HttpPost]
        [Route("denied")]
        public async Task<IActionResult> DeniedAccount(AcceptPersonDTO dto)
        {
            try
            {
                await _adminService.DeniedAccount(dto.idPerson);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentNullException ne)
            {
                return BadRequest(new { message = ne.Message });
            }
            catch (FormatException fe)
            {
                return BadRequest(new { message = fe.Message });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Something went wrong." });
            }

            //send on notif on email about acc
            return Ok();
        }

    }
}
