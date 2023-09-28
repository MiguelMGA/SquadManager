using SquadManager.Models;
using FluentValidation;

namespace SquadManager.Validator
{
	public class UserValidator : AbstractValidator<UserViewModel>
	{
		public UserValidator() 
		{
			RuleFor(user => user.Email).NotNull().WithMessage("Campo de email vazio!");
			RuleFor(user => user.Email).EmailAddress().WithMessage("Email inválido!");
			RuleFor(user => user.Password).NotNull().WithMessage("Campo de senha vazio!"); ;
		}
	}
}
