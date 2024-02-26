using DtoLayer.LoginDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TraversalProject.Areas.Members.Dto.IdentityDtos;

namespace TraversalProject.Areas.Members.Controllers
{
    [Area("Members")]
    [Route("Member/[controller]/[action]")]
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
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, updateUserDto.password);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("SignIn", "Login");
            }

            return View();
        }
    }
}
