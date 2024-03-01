
using DtoLayer.LoginDtos;

namespace BussinessLayer.Abstract
{
    public interface IMailService
    {
        
        ResultDto SendMail2(string Subject, string Content, string Mail);


    }
}
