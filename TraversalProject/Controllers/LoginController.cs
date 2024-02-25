using BussinessLayer.ValidationRules;
using DtoLayer.RegisterDtos;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		private readonly UserManager<AppUser> _usermanger;

		public LoginController(UserManager<AppUser> usermanger)
		{
			_usermanger = usermanger;
		}

		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(CreateUserDto createUserDto)
		{
			RegisterValidator validationRules = new RegisterValidator();
			ValidationResult validationResult = validationRules.Validate(createUserDto);
			if (validationResult.IsValid)
			{

                if (createUserDto.Password == createUserDto.ConfirmPassword)
                {
                    AppUser newUser = new AppUser()
                    {
                        Name = createUserDto.Name,
                        Surname = createUserDto.Surname,
                        Email = createUserDto.MailAdress,
                        UserName = createUserDto.UserName,
						Gender="test",
						ImageUrl="test",
                    };
                    var result = await _usermanger.CreateAsync(newUser, createUserDto.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("SignIn", "Login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
				else
				{
                    ModelState.AddModelError("ConfirmPassword", "Girilen Şifreler Eşleşmiyor.");
                }
            }
			else
			{
				foreach (var item in validationResult.Errors)
				{
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
			}

			return View();
		}

		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}
	}
}
