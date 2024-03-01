using BussinessLayer.Abstract;
using DtoLayer.LoginDtos;
using MailKit.Net.Smtp;
using MimeKit;

namespace BussinessLayer.Concrete
{
    public class MailManger : IMailService
    {

        public ResultDto SendMail2(string Subject, string Content, string Mail)
        {
            try
            {
                MimeMessage mimeMessage = new MimeMessage();

                MailboxAddress mailAddresFrom = new MailboxAddress("Traversal", "aspnetcoreprojeler@gmail.com");
                mimeMessage.From.Add(mailAddresFrom);

                MailboxAddress mailAdressTo = new MailboxAddress("Üye", Mail);
                mimeMessage.To.Add(mailAdressTo);

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = Content;

                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = Subject;


                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("aspnetcoreprojeler@gmail.com", "wpyahgokfqqxmgll");
                client.Send(mimeMessage);
                client.Disconnect(true);

                return new ResultDto() { status = true, description = "success" };
            }
            catch (Exception hata)
            {
                return new ResultDto() { status = false, description = "Bir Hata Oluştu Hata Detayı" + hata.Message };
            }
        }
    }
}
