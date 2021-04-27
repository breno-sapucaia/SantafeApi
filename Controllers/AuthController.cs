using Microsoft.AspNetCore.Authorization;
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
		/// <summary>
		///  Efetua o login no sistema e gerar um token do tipo - Bearer JWT
		/// </summary>
		/// <param name="loginModel">loginModel</param>
		/// <returns>Retorna um token token JWT</returns>
		/// <response code="200">Retorna o JWT</response>
		/// <response code="400">Senha passada é inválida</response>
		/// <response code="404">Usuário não existe</response>
		[AllowAnonymous]
		[HttpPost("login")]
		[Produces("application/json")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(400)]
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
					return BadRequest(new ErrorModel("Senha inválida") { FieldName = "Password" });
				}

			}
			return NotFound(new ErrorModel("Usuário não existe"));
		}
		/// <summary>
		///  Efetua o registro de um novo usuário.
		/// </summary>
		/// <param name="registerModel"></param>
		/// <returns>Retorna uma mensagem de usuário cadastrado.</returns>
		/// <response code="200"> retorna uma mensagem de sucesso.</response>
		/// <response code="400"> retorna os possíveis erros do esquema de Model,</response>
		[AllowAnonymous]
		[Produces("application/json")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200)]
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
				//TODO: enviar email com token.
				return Ok(new { Message = "Usuário criado com sucesso" });
			}
			else
			{
				var stringBuilder = new StringBuilder();
				foreach (var error in result.Errors)
				{
					stringBuilder.Append(error.Description);
					stringBuilder.Append("\r\n");
				}
				return BadRequest(new ErrorModel(stringBuilder.ToString()));

			};
		}

		/// <summary>
		/// Solicita a geração de um token através do Email do usuário registrado.
		/// </summary>
		/// <param name="resetPasswordModel">Usado para receber o Email do usuário</param>
		/// <returns>Retorna um token válido usado para resetar a senha do usuário.</returns>
		/// <response code="200"> Retorna um JWT</response>
		/// <response code="404"> Retorna mensagem de erro de usuário não encontrado</response>
		[AllowAnonymous]
		[Produces("application/json")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[HttpPost("password-reset")]
		public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
		{
			var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
			if (user == null)
				return NotFound(new ErrorModel
				{
					Message = "O usuário não existe",
					FieldName = "Email"
				});

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);
			return Ok(new { Token = token });
		}
		/// <summary>
		///		Valida token passado e reseta a senha se for válido.
		/// </summary>
		/// <param name="resetPasswordTokenModel"></param>
		/// <returns> Mensagem de sucesso</returns>
		/// <response code="200">Mesagem de sucesso</response>
		/// <response code="400">Erros gerados pelo UserManager</response>
		[AllowAnonymous]
		[Produces("application/json")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[HttpPut("password-reset-with-token")]
		public async Task<ActionResult> ResetPasswordWithToken([FromBody] ResetPasswordTokenModel resetPasswordTokenModel)
		{
			var user = await _userManager.FindByEmailAsync(resetPasswordTokenModel.Email);

			if (user == null)
				return NotFound(new ErrorModel("user not found"));

			var result = await _userManager.ResetPasswordAsync(user, resetPasswordTokenModel.Token, resetPasswordTokenModel.Password);
			if (result.Succeeded)
			{
				return Ok(new { Message = "Senha alterada com sucesso" });
			}
			else
			{
				var stringBuilder = new StringBuilder();
				foreach (var error in result.Errors)
				{
					stringBuilder.Append(error.Description);
					stringBuilder.Append("\r\n");
				}
				return BadRequest(new ErrorModel(stringBuilder.ToString()));
			}
		}

	}
}

