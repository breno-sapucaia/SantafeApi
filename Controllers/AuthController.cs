﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SantafeApi.Data;
using SantafeApi.Infraestrucutre.Identity.Data;
using SantafeApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SantafeApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class AuthController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private SantafeApiContext _dbContext;
		private readonly UserManager<SantafeApiUser> _userManager;
		private readonly SignInManager<SantafeApiUser> _signInManager;

		public AuthController(
			IConfiguration configuration, SantafeApiContext dbContext,
			UserManager<SantafeApiUser> userManager, SignInManager<SantafeApiUser> signInManager
			)
		{
			_configuration = configuration;
			_dbContext = dbContext;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[AllowAnonymous]
		[HttpGet("getFruits")]
		public ActionResult GetFruits()
		{
			var list = new string[] { "banana", "apple" };
			return Ok(list);
		}
		[HttpGet("getFruitsAuthenticated")]
		public ActionResult GetFruitsAuthenticated()
		{
			var list = new string[] { "banana", "apple" };
			return Ok(list);
		}

		[AllowAnonymous]
		[HttpPost("getToken")]
		public async Task<ActionResult> GetToken([FromBody] LoginModel loginModel)
		{
			var user = _dbContext.Users.FirstOrDefault(x => x.Email == loginModel.Email);
			if (user != null)
			{
				var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);

				if (signInResult.Succeeded)
				{
					var tokenHandler = new JwtSecurityTokenHandler();
					var key = Encoding.ASCII.GetBytes(_configuration["Identity:Key"]);
					var tokenDescriptor = new SecurityTokenDescriptor
					{
						Subject = new ClaimsIdentity(new Claim[]
						{
						new Claim(ClaimTypes.Name, loginModel.Email),
						new Claim(ClaimTypes.Role, "admin")
						}),
						Expires = DateTime.UtcNow.AddDays(1),
						SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
					};
					var token = tokenHandler.CreateToken(tokenDescriptor);
					var tokenString = tokenHandler.WriteToken(token);
					return Ok(new { Token = tokenString });
				}
				else
				{
					return Ok("senha inválida");
				}

			}
			return Ok("Seu usuário não existe");
		}

		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<ActionResult> Register([FromBody] RegisterModel registerModel)
		{
			SantafeApiUser santafeApiUser = new()
			{
				Email = registerModel.Email,
				UserName = registerModel.Email,
				EmailConfirmed = false
			};

			var result = await _userManager.CreateAsync(santafeApiUser, registerModel.Password);

			if (result.Succeeded)
			{

				return Ok(new { Result = "Usuário criado com sucesso" });
			}
			else
			{
				var stringBuilder = new StringBuilder();
				foreach( var error in result.Errors)
				{
					stringBuilder.Append(error.Description);
					stringBuilder.Append("\r\n");
				}
				return BadRequest(stringBuilder.ToString());
			}
		}

	}
}

