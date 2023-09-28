using Common;
using FluentValidation;

namespace API.Validator
{
	public class PersonValidator : AbstractValidator<PersonModel>
	{
		public PersonValidator() {
			RuleFor(person => person.Name).NotNull().WithMessage("campo de email vazio!");
			RuleFor(person => person.Email).NotNull().WithMessage("campo de email vazio!");
			RuleFor(person => person.Email).EmailAddress().WithMessage("Email inválido!");
		}
	}
}
