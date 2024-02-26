using BussinessLayer.ValidationRules;
using DtoLayer.LoginDtos;
using DtoLayer.RegisterDtos;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TraversalProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _usermanger;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> usermanger, SignInManager<AppUser> signInManager)
        {
            _usermanger = usermanger;
            _signInManager = signInManager;
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
                        Gender = "test",
                        ImageUrl = "test",
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
        public IActionResult SignIn(string? ReturnUrl)
        {
            TempData["returnUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginDto loginDto)
        {
            LoginValidator validationRules = new LoginValidator();
            ValidationResult valide = validationRules.Validate(loginDto);
            if (valide.IsValid)
            {
                await _signInManager.SignOutAsync();

                var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, true);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(TempData["returnUrl"] != null ? TempData["returnUrl"].ToString() : ""))
                        return RedirectToAction("Index", "Profile", new { area = "Members" });
                    return Redirect(TempData["returnUrl"].ToString());
                }
                else
                {

                    ModelState.AddModelError("", "Hatalı Kullanıcı Adı Veya Şifre");
                    return View();
                }
            }
            else
            {

                foreach (var item in valide.Errors)
                {
                    ModelState.AddModelError(item.ErrorCode, item.ErrorMessage);
                }

                return View();
            }


        }



    }
}
