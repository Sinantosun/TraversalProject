using BussinessLayer.Abstract;
using DtoLayer.LoginDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using NuGet.Common;
using Org.BouncyCastle.Asn1.X509;
using System.Web;

namespace TraversalProject.Areas.Members.Controllers
{
    [Area("Members")]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailService _mailService;
        private readonly SignInManager<AppUser> _signInManager;

        public DashboardController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.UserName = user.Name + " " + user.Surname;
                ViewBag.UserImage = user.ImageUrl;
                ViewBag.PhoneNumber = user.PhoneNumber;
                ViewBag.MailAdress = user.Email;
                ViewBag.PhoneConfirm = user.PhoneNumberConfirmed;
                ViewBag.EmailConfirm = user.EmailConfirmed;
                ViewBag.twoFactorConfirm = user.TwoFactorEnabled;


            }
            catch (ArgumentNullException ex)
            {

                return RedirectToAction("SignIn?ReturnUrl=/Members/Dasboard/Index", "Login");
                throw;
            }



            return View();
        }

        public async Task<JsonResult> ChangeTwoFactorEnabledToTrue(string pwd)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var getValids = await _userManager.GetValidTwoFactorProvidersAsync(user);
            if (getValids.Contains("Email") || getValids.Contains("Phone"))
            {
                var control = await _userManager.CheckPasswordAsync(user, pwd);
                if (control)
                {
                    user.TwoFactorEnabled = true;
                    await _userManager.UpdateAsync(user);
                    await _signInManager.RefreshSignInAsync(user);

                    var result = new
                    {
                        title="Başarılı",
                        err = "None",
                        icon = "success",
                        Descr = "İki Aşamalı Doğrulama Başarıyla Açıldı.",
                        isReolad = false,
                    };

                    return Json(result);
                }
                else
                {
                    var result = new
                    {
                        title = "Hata",
                        icon = "info",
                        err = "True",
                        Descr = "Lütfen Şifrenizi Kontrol Edin.",
                        isReolad = true,
                    };
                    return Json(result);
                }
            }
            else
            {
                var result = new
                {
                    title = "Aktivasyon Kodu Gönderildi",
                    icon = "warning",
                    err = "True",
                    Descr = "İki Aşamalı Doğrulama İçin Mail Adresinizi Onaylamanız Gereklidir. Lütfen E Posta Adresinize Gelen Maili Kontrol Ediniz.",
                    isReolad = false,
                };
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                _mailService.SendMail2("Mail Onaylama İşlemi", $"Merhaba, <br><br>Bu işlem size ait değilse lütfen bizimle iletişime geçiniz. <br><br> <a target=\"blank\" style=\"appearance: none; text-decoration: none; height:35px; width:150px; background-color: #2ea44f; border: 1px solid rgba(27, 31, 35, .15);  border-radius: 6px;  box-shadow: rgba(27, 31, 35, .1) 0 1px 0;  box-sizing: border-box;  color: #fff; cursor: pointer;  display: inline-block; font-family: -apple-system,system-ui,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji;  font-size: 14px;  font-weight: 600; line-height: 20px;  padding: 6px 16px;  position: relative; text-align: center;  text-decoration: none;  user-select: none;  -webkit-user-select: none; touch-action: manipulation;  vertical-align: middle; white-space: nowrap;\"  target=\"_blank\" href=\"https://localhost:7100{Url.Action("ConfirmMail", "Dashboard", new { userId = user.Id, token = HttpUtility.UrlEncode(token) })}\">Hesabımı Onayla</a><br><br> Admin", user.Email);


                return Json(result);
            }


        }

        public async Task<JsonResult> ChangeTwoFactorEnabledToFalse(string pwd)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var control = await _userManager.CheckPasswordAsync(user, pwd);
            if (control)
            {

                user.TwoFactorEnabled = false;
                await _userManager.UpdateAsync(user);
                await _signInManager.RefreshSignInAsync(user);

                var result = new
                {
                    title = "Başarılı",
                    icon = "success",
                    err = "True",
                    Descr = "İki Aşamalı Doğrulama Kapatıldı.",
                    isReolad = true,
                };
                return Json(result);
            }
            else
            {
                var result = new
                {
                    title = "Hata",
                    icon = "info",
                    err = "True",
                    Descr = "Lütfen Şifrenizi Kontrol Edin.",
                    isReolad = true,
                };
                return Json(result);
            }

        }
        [HttpGet("[action]/{userId}/{token}")]
        public async Task<IActionResult> ConfirmMail(string userId, string token)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(token));
            if (result.Succeeded)
            {
                await _userManager.UpdateAsync(user);
                return View();
            }
            else
            {
                return View();
            }
        }

        public async Task<JsonResult> ChangePasswordEveryThreeMonthSetActive()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.ChangePasswordEveryThreeMonthsIsActive = true;
            var IdentiyResult = await _userManager.UpdateAsync(user);
            if (IdentiyResult.Succeeded)
            {
                var result = new
                {
                    title = "Başarılı",
                    icon = "info",
                    err = "None",
                    Descr = "3 Ayda Bir Şifreniz Güncelleme İşlemi Açıldı.",
                };
                return Json(result);
            }
            else
            {
                var result = new
                {
                    title = "Hata",
                    icon = "error",
                    err = "True",
                    Descr = "Bir Hatayla Karşılaşıldı.",
                };
                return Json(result);
            }
        

        }
        public async Task<JsonResult> ChangePasswordEveryThreeMonthSetFalse()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.ChangePasswordEveryThreeMonthsIsActive = false;
            var IdentiyResult = await _userManager.UpdateAsync(user);
            if (IdentiyResult.Succeeded)
            {
                var result = new
                {
                    title = "Hata",
                    icon = "info",
                    err = "True",
                    Descr = "3 Ayda Bir Şifreniz Güncelleme İşlemi Kapatıldı.",
                };
                return Json(result);
            }
            else
            {
                var result = new
                {
                    title = "Hata",
                    icon = "error",
                    err = "True",
                    Descr = "Bir Hatayla Karşılaşıldı.",
                };
                return Json(result);
            }
          

        }
    }
}
