using BussinessLayer.Abstract;
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
        private readonly IMailService _mailService;

        public LoginController(UserManager<AppUser> usermanger, SignInManager<AppUser> signInManager, IMailService mailService)
        {
            _usermanger = usermanger;
            _signInManager = signInManager;
            _mailService = mailService;
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
                        ChangePasswordEveryThreeMonthsIsActive = true,
                        LastChangePasswordDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()),
                        ThreeMonthsLaterPasswordDate = Convert.ToDateTime(DateTime.Now.AddDays(90).ToShortDateString()),
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
            bool status = false;
            LoginValidator validationRules = new LoginValidator();
            ValidationResult valide = validationRules.Validate(loginDto);
            if (valide.IsValid)
            {
                await _signInManager.SignOutAsync();
                var user = await _usermanger.FindByNameAsync(loginDto.UserName);
                var userCheckPWD = await _usermanger.CheckPasswordAsync(user, loginDto.Password);
                DateTime UserLastChangePasswordDate = Convert.ToDateTime(user.ThreeMonthsLaterPasswordDate);
                DateTime DateNow = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                TimeSpan Fark = UserLastChangePasswordDate - DateNow;
                if (user.ChangePasswordEveryThreeMonthsIsActive == true)
                {
                    if (userCheckPWD)
                    {
                        status = false;
                        ModelState.AddModelError("", "Hesabınızın Şifre Süresi Dolmuştur. Lütfen Mail Adresinize Gönderilen Şifre Yenileme Mailini Kontrol Ediniz.");
                    }
                    else
                    {
                        status = false;
                        ModelState.AddModelError("", "Hatalı Kullanıcı Adı Veya Şifre");
                    }
                }
                if (status)
                {
                    var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, true);
                    if (result.RequiresTwoFactor)
                    {
                        var genareteTwoFactorToken = await _usermanger.GenerateTwoFactorTokenAsync(user, "Email");
                        ResultDto MailResult2 = _mailService.SendMail2("İki Adımlı Doğrulama Kodu", "Doğrulama Kodunuz: " + genareteTwoFactorToken, user.Email);

                        if (MailResult2.status == true)
                        {
                            return RedirectToAction("TwoFactorAuth", "Login");
                        }
                        else
                        {
                            ModelState.AddModelError("", MailResult2.description);
                            return View();
                        }
                    }
                    else if (result.Succeeded)
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
                return View();

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
        [HttpGet("[action]")]
        public async Task<IActionResult> TwoFactorAuth()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> TwoFactorAuth(string token)
        {
            var SignInTwoFactor = await _signInManager.TwoFactorSignInAsync("Email", token, false, false);
            if (SignInTwoFactor.Succeeded)
            {
                return RedirectToAction("Index", "Profile", new { area = "Members" });
            }
            else
            {
                ViewBag.Err = "Doğrulama Kodunuz Hatalı.";
            }
            return RedirectToAction("TwoFactorAuth", "Login");
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> ResetPassword()
        {
            return View();
        }
    }
}
