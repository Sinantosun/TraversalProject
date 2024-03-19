using BussinessLayer.Abstract;
using BussinessLayer.ValidationRules.LoginRegisterValidatiors;
using DocumentFormat.OpenXml.Spreadsheet;
using DtoLayer.LoginDtos;
using DtoLayer.RegisterDtos;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NuGet.Common;
using System.Web;

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
                        ChangePasswordEveryThreeMonthsIsActive = false,
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
                if (user != null)
                {
                    var userCheckPWD = await _usermanger.CheckPasswordAsync(user, loginDto.Password);

                    DateTime UserLastChangePasswordDate = Convert.ToDateTime(user.ThreeMonthsLaterPasswordDate);
                    DateTime DateNow = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    TimeSpan Fark = UserLastChangePasswordDate - DateNow;
                    if (user.ChangePasswordEveryThreeMonthsIsActive == true && Fark.TotalDays <= 0)
                    {
                        if (userCheckPWD)
                        {
                            status = false;
                            ModelState.AddModelError("", "Hesabınızın Şifre Süresi Dolmuştur. Lütfen Mail Adresinize Gönderilen Şifre Yenileme Mailini Kontrol Ediniz.");
                            var generateResetToken = await _usermanger.GeneratePasswordResetTokenAsync(user);
                            _mailService.SendMail2("Şifre Sıfırlama", $"Merhaba, Şifrenizin Süresi (90 Gün) Dolmuştur Şifrenizi Aşağıdaki Linkden Yeniliyebilirsiniz. <br><br> Eğer bu özelliği kapatmak isterseniz (önerilmez)  Hesap Aylarınızdan her 3 ayda bir şifre yenileme özelliğini kapatarak işlemleri gerçekleşirebilirsiniz. <br><br>Bu işlem size ait değilse lütfen bizimle iletişime geçiniz. <br><br> <a target=\"blank\" style=\"appearance: none; text-decoration: none; height:35px; width:200px; background-color: #2ea44f; border: 1px solid rgba(27, 31, 35, .15);  border-radius: 6px;  box-shadow: rgba(27, 31, 35, .1) 0 1px 0;  box-sizing: border-box;  color: #fff; cursor: pointer; text-align:center;  display: inline-block; font-family: -apple-system,system-ui,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji;  font-size: 14px;  font-weight: 600; line-height: 20px;  padding: 6px 16px;  position: relative; text-align: center;  text-decoration: none;  user-select: none;  -webkit-user-select: none; touch-action: manipulation;  vertical-align: middle; white-space: nowrap;\"  target=\"_blank\" href=\"https://localhost:7100{Url.Action("ResetPassword", "Login", new { userId = user.Id, token = HttpUtility.UrlEncode(generateResetToken) })}\">Şifre Güncelle</a><br><br> Admin", user.Email);
                        }
                        else
                        {
                            status = false;
                            ModelState.AddModelError("", "Hatalı Kullanıcı Adı Veya Şifre");
                        }
                    }
                    else
                    {
                        status = true;
                    }
                    if (status)
                    {
                        var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, true);
                        if (!result.IsLockedOut)
                        {
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
                        else
                        {
                            ModelState.AddModelError("", "Hatalı Girişler sebebiyle hesabınız kitlendi lütfen şu tarihte tekrar deneyin: " + user.LockoutEnd);
                        }


                    }
                    return View();
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

        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string mail)
        {
            var getUser = await _usermanger.FindByEmailAsync(mail);
            if (getUser != null)
            {
                var generateResetToken = await _usermanger.GeneratePasswordResetTokenAsync(getUser);
                ResultDto resultDto = _mailService.SendMail2("Şifre Sıfırlama", $"Merhaba, Şifre Sıfırlama Talebini Aldık, Butona Tıklayarak Şifreni Yenileyebilirsin. <br><br>Bu işlem size ait değilse lütfen bizimle iletişime geçiniz. <br><br> <a target=\"blank\" style=\"appearance: none; text-decoration: none; height:35px; width:200px; background-color: #2ea44f; border: 1px solid rgba(27, 31, 35, .15);  border-radius: 6px;  box-shadow: rgba(27, 31, 35, .1) 0 1px 0;  box-sizing: border-box;  color: #fff; cursor: pointer; text-align:center;  display: inline-block; font-family: -apple-system,system-ui,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji;  font-size: 14px;  font-weight: 600; line-height: 20px;  padding: 6px 16px;  position: relative; text-align: center;  text-decoration: none;  user-select: none;  -webkit-user-select: none; touch-action: manipulation;  vertical-align: middle; white-space: nowrap;\"  target=\"_blank\" href=\"https://localhost:7100{Url.Action("ResetPassword", "Login", new { userId = getUser.Id, token = HttpUtility.UrlEncode(generateResetToken) })}\">Şifre Güncelle</a><br><br> Admin", mail);

                if (resultDto.status)
                {
                    ViewBag.Err = "Mail Sıfırlama İsteği Gönderildi.";
                }


            }
            else
            {
                ViewBag.Err = "Mail Bulunamadı.";
            }

            return View();
        }
        [HttpGet("[action]/{userId}/{token}")]
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            return View();
        }

        [HttpPost("[action]/{userId}/{token}")]
        public async Task<IActionResult> ResetPassword(string userId, string token, string pwd, string cnfrmpwd)
        {
            if (pwd == cnfrmpwd)
            {
                AppUser user = await _usermanger.FindByIdAsync(userId);
                IdentityResult identityResult = await _usermanger.ResetPasswordAsync(user, HttpUtility.UrlDecode(token), pwd);
                if (identityResult.Succeeded)
                {
                    user.LastChangePasswordDate = Convert.ToDateTime(DateTime.Now.ToLongDateString());
                    user.ThreeMonthsLaterPasswordDate = Convert.ToDateTime(DateTime.Now.AddDays(90).ToShortDateString());
                    var x = await _usermanger.UpdateAsync(user);
                    if (x.Succeeded)
                    {
                        _mailService.SendMail2("Şifre Başarıyla Sıfırlandı", "Merhaba, <h3 style=text-transform:capitalize>" + user.Name + " " + user.Surname + "</h3> birkaç dakika önce şifreniz sıfırlandı bu işlem size ait değilse lütfen iletişime geçiniz. <br> Admin", user.Email);
                        return RedirectToAction("SignIn", "Login");
                    }
                    ViewBag.Err = "Bir Hata Oluştu Şifre Sıfırlanamadı";
                    return View();
                }
                else
                {
                    foreach (var item in identityResult.Errors)
                    {

                        ViewBag.Err = item.Description;
                    }
                }
            }

            return View();
        }
    }
}
