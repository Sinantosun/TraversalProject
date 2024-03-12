using DtoLayer.LoginDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TraversalProject.Areas.Members.Dto.IdentityDtos;

namespace TraversalProject.Areas.Members.Controllers
{
    [Area("Members")]
    [Route("Members/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public ProfileController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UpdateUserDto updateUserDto = new UpdateUserDto()
            {

                email = values.Email,
                imageurl = "test",
                name = values.Name,
                surname = values.Surname,
                phoneNumber = values.PhoneNumber,


            };
            return View(updateUserDto);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UpdateUserDto updateUserDto)
        {

            if (updateUserDto.confirmpassword == updateUserDto.password)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if (updateUserDto.Image != null)
                {
                    var resource = Directory.GetCurrentDirectory();
                    var extension = Path.GetExtension(updateUserDto.Image.FileName);
                    var imageName = Guid.NewGuid() + extension;
                    var saveLocation = resource + "/wwwroot/userimages/" + imageName;
                    var stream = new FileStream(saveLocation, FileMode.Create);
                    await updateUserDto.Image.CopyToAsync(stream);

                    user.ImageUrl = imageName;
                }
                user.Name = updateUserDto.name;
                user.Surname = updateUserDto.surname;
                user.PhoneNumber = updateUserDto.phoneNumber;
                if (updateUserDto.password != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    IdentityResult passwordResult = await _userManager.ResetPasswordAsync(user, token.ToString(), updateUserDto.password);
                    if (!passwordResult.Succeeded)
                    {
                        foreach (var item in passwordResult.Errors)
                        {
                            ViewBag.ResultPassword += item.Description + "<br/>";
                            ModelState.AddModelError("confirmpassword", "lütfen parola hatalarını kontrol edin.");
                        }
                    }

                }
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {


                }
            }
            else
            {
                ModelState.AddModelError("confirmpassword", "Şifreler Uyuşmuyor");
            }
            return View();
        }


        public JsonResult PasswordRequireFormats(string value)
        {

            if (!string.IsNullOrEmpty(value))
            {
                int _upperCaseInt = value.Length - Regex.Replace(value, "[A-Z]", "").Length;
                int _LowerCaseInt = value.Length - Regex.Replace(value, "[a-z]", "").Length;
                int _DigitInt = value.Length - Regex.Replace(value, "[0-9]", "").Length;
                int _valueLenght = value.Length;

                string pattern = "^[!@#$&()\\-`.+,/\"]*$";


             

                bool _RegexChar = Regex.IsMatch(value, pattern);

                var result = new
                {
                    upperCaseInt = _upperCaseInt,
                    LowerCaseInt = _LowerCaseInt,
                    DigitInt = _DigitInt,
                    valueLenght = _valueLenght,
                    RegexChar = _RegexChar,
                };


                return Json(result);
            }
            else
            {
                return Json("err");
            }
        }
    }
}
