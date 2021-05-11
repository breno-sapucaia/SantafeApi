using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SantafeApi.Models;

namespace SantafeApi.Models.Validator
{
	public class RegisterValidator : AbstractValidator<RegisterModel>
	{
		public RegisterValidator()
		{
			RuleFor(x => x.Email)
				.EmailAddress()
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.Password)
				.MinimumLength(8)
				.NotEmpty()
				.NotNull()
				.Matches("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$")
				.WithMessage("A senha deve conter 8 caracteres sendo 1 maiúsculo, 1 numérico e 1 caractere especial");
		}
	}
}
