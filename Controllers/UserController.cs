using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SantafeApi.Infraestrucutre.Data;
using SantafeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SantafeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {

        private readonly UserManager<SantafeApiUser> _userManager;

        public UserController(UserManager<SantafeApiUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPut("manage-access")]
        public async Task<IActionResult> ManageAccess(string userId, bool hasAccess)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.HasAccess = hasAccess;
                await _userManager.UpdateAsync(user);
                if (hasAccess)
                    return Ok($"Acesso do usuário: {user.UserName} liberado.");

                return Ok($"Acesso do usuário: {user.UserName} revogado.");
            }
            return BadRequest(new ErrorModel { Message = "Usuário não encontrado." });
        }

    }
}
