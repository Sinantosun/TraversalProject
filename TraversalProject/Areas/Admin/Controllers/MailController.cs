using BussinessLayer.Abstract;
using DtoLayer.LoginDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TraversalProject.Dtos.MailDtos;

namespace TraversalProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class MailController : Controller
    {
        private readonly IMailService _mailService;
        private readonly UserManager<AppUser> _userManager;

        public MailController(IMailService mailService, UserManager<AppUser> userManager)
        {
            _mailService = mailService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(MailRequest mailRequest)
        {



            ResultDto resultDto = _mailService.SendMail2(mailRequest.Subject, mailRequest.ContentBody, mailRequest.ReciverMail);

            if (resultDto.status)
            {
                ViewBag.Err = "Başarıyla Gönderildi";
                ViewBag.Icon = "info";
            }
            else
            {
                ViewBag.Icon = "error";
                ViewBag.Err = "Mail Göndermede Bir Hata Meydana geldi";

            }


            return View();


        }
    }
}
