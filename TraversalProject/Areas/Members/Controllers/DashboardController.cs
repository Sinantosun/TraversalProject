using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TraversalProject.Areas.Members.Controllers
{
    [Area("Members")]

    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public DashboardController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            var control = await _userManager.CheckPasswordAsync(user, pwd);
            if (control)
            {
                user.TwoFactorEnabled = true;
                await _userManager.UpdateAsync(user);
                await _signInManager.RefreshSignInAsync(user);
                return Json("01");
            }
            else
            {
                return Json("02");
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
                return Json("01");
            }
            else
            {
                return Json("02");
            }

        }


    }
}
