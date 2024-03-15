using BussinessLayer.Abstract.AbstractUow;
using DtoLayer.AccountDtos;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServiceUow _accountService;

        public AccountController(IAccountServiceUow accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult List()
        {
            List<SelectListItem> liste2 = (from x in _accountService.TListAccounts()
                                           select new SelectListItem
                                           {
                                               Text = x.Name,
                                               Value = x.AccountID.ToString(),
                                           }).ToList();
            return Ok(liste2);
        }


        [HttpPut]
        public IActionResult UpdateAccount(UpdateAccountDto updateAccountDto)
        {
            if (updateAccountDto.SenderID != updateAccountDto.ReciverID)
            {
                var valueSender = _accountService.TgetByID(updateAccountDto.SenderID);
                if (valueSender.Balance >= updateAccountDto.Amount)
                {
                    var valueReciver = _accountService.TgetByID(updateAccountDto.ReciverID);

                    valueSender.Balance -= updateAccountDto.Amount;
                    valueReciver.Balance += updateAccountDto.Amount;
                    List<Account> motifedDatas = new List<Account>()
                    {
                       valueReciver,
                       valueSender
                     };
                    _accountService.TMultiUpdate(motifedDatas);
                    return Ok();

                }
                else
                {
                    return BadRequest("Gönderinin Bakiyesi Yetersiz.");
                }
            }
            else
            {
                return BadRequest("Alıcı Ve Gönderen Aynı Kışi Olamaz.");
            }



        }
    }
}
