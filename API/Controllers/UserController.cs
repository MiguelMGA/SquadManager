using Common;
using Microsoft.AspNetCore.Mvc;
using System;
using API.Validator;
using ValidationResult = FluentValidation.Results.ValidationResult;
using API.Services;

namespace API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IPersonService _personService;
		private readonly IUserService _userService;
		public UserController(IPersonService personService,
			IUserService userService) 
		{
			_personService = personService;
			_userService = userService;
		}

		/// <summary>
		/// Autentica o usuário
		/// </summary>
		/// <param name="user">Username e Senha do usuário</param>
		/// <returns>OK</returns>
		[HttpPost]
		public IActionResult Login(UserModel user)
		{
			var result = _userService.Login(user);
			
			if(result != null)
			{
				return Ok(new
				{
					UserID = result.Id,
					personId = result.PersonId,
					Email = result.Person.Email,
					Username = result.Username
				});
			} else
			{
				return Ok(new {response = "ERROR"});
			}
		}

		/// <summary>
		/// API para criação de usuário
		/// </summary>
		/// <param name="user">Recebe o Username, Email, Senha e confirmação de senha do usuário</param>
		/// <returns></returns>
		[HttpPost("Create")]
		public IActionResult Create(UserModel user)
		{
			UserValidator validator = new UserValidator();

			ValidationResult results = validator.Validate(user);

			if (!results.IsValid)
			{
				foreach (var failure in results.Errors)
				{
					Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
				}

				return Ok(new { response = "ERROR" });
			}

			var personId = _personService.AddPerson(new PersonModel()
			{
				Email = user.Person.Email,
				Name = user.Person.Name,
			});

			_userService.AddUser(new UserModel()
			{
				PersonId = personId,
				Password = user.Password,
				Username = user.Username
			});
			return Ok(new { response = "OK"});
		}

		/// <summary>
		/// API para edição de usuário
		/// </summary>
		/// <param name="user"></param>
		/// <returns>Retorna OK</returns>
		[HttpPatch("update")]
		public IActionResult Update(UserModel user)
		{
			_userService.UpdateUser(user);
			_personService.UpdatePerson(user.Person);

			return Ok(new { response = "OK" });
		}

		/// <summary>
		/// API para recuperação de senha do usuário
		/// </summary>
		/// <param name="email">Recebe o email do usuário</param>
		/// <returns></returns>
		[HttpPost("Forgot")]
		public IActionResult Forgot([FromBody]string email) 
		{
			return Ok(new { response = "Ok" });
		}

		/// <summary>
		/// API para resetar a senha do usuário
		/// </summary>
		/// <param name="user">Senha e confirmação da nova senha</param>
		/// <returns></returns>
		[HttpPost("Reset")]
		public IActionResult Reset(UserModel user)
		{
			return Ok(new { response = "Ok" });
		}
	}
}
