using Common;
using FluentValidation;

namespace API.Validator
{
	public class UserValidator : AbstractValidator<UserModel>
	{
		public UserValidator()
		{
			RuleFor(user => user.Username).NotNull().WithMessage("campo de username vazio!");
			RuleFor(user => user.Person.Email).NotNull().WithMessage("campo de email vazio!");
			RuleFor(user => user.Person.Email).EmailAddress().WithMessage("Email inválido!");
			RuleFor(user => user.Password).NotNull().WithMessage("Campo de senha vazio!");
			RuleFor(user => user.Password).Equal(o => o.ConfirmPassword).WithMessage("Campo de senha vazio!");
		}
	}
}
