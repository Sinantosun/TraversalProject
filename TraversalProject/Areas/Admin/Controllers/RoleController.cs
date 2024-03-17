using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TraversalProject.Dtos.IdentityDtos;
using TraversalProject.Dtos.RoleDtos;

namespace TraversalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/Role")]
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }
        [Route("CreateRole")]
        [HttpGet]
        public IActionResult CreateRole()
        {

            return View();
        }
        [Route("CreateRole")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto appRole)
        {
            AppRole Role = new AppRole()
            {
                Name = appRole.RoleName,
            };
            var result = await _roleManager.CreateAsync(Role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [Route("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            if (value != null)
            {
                await _roleManager.DeleteAsync(value);
                return RedirectToAction("Index");
            }
            else
            {
                //ModelState.AddModelError("RoleName", "Rol Adı Bulunamadı");
                return View("Index");
            }
        }

        [Route("UpdateRole/{id}")]
        [HttpGet]
        public IActionResult UpdateRole(int id)
        {
            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == id);

            UpdateRoleDto updateRoleDto = new UpdateRoleDto()
            {
                Id = value.Id,
                RoleName = value.Name,
            };

            return View(updateRoleDto);
        }
        [Route("UpdateRole/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto updateRoleDto)
        {

            var value = _roleManager.Roles.FirstOrDefault(x => x.Id == updateRoleDto.Id);
            value.Name = updateRoleDto.RoleName;
            await _roleManager.UpdateAsync(value);

            return RedirectToAction("Index");
        }

        [Route("UserList")]
        public IActionResult UserList()
        {
            var values = _userManager.Users.ToList();
            return View(values);
        }
        [HttpGet]
        [Route("AssignRole/{id}")]
        public async Task<IActionResult> AssignRole(int id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            ViewBag.UserNick = user.UserName;
            TempData["userId"] = user.Id;
            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            List<RoleAssignDto> roleAssignDtos = new List<RoleAssignDto>();
            foreach (var item in roles)
            {
                RoleAssignDto roleAssignDto = new RoleAssignDto();
                roleAssignDto.RoleID = item.Id;
                roleAssignDto.RoleName = item.Name;
                roleAssignDto.RoleExist = userRoles.Contains(item.Name);
                roleAssignDtos.Add(roleAssignDto);

            }
            return View(roleAssignDtos);
        }
        [HttpPost]
        [Route("AssignRole/{id}")]
        public async Task<IActionResult> AssignRole(List<RoleAssignDto> model)
        {
            var userId = (int)TempData["userId"];
            var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
            foreach (var item in model)
            {
                if (item.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, item.RoleName);  
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return RedirectToAction("UserList");

        }
    }
}
