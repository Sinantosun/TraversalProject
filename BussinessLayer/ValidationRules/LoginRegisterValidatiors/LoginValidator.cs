using DtoLayer.LoginDtos;
using FluentValidation;

namespace BussinessLayer.ValidationRules.LoginRegisterValidatiors
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı Adı Giriniz..");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifrenizi Giriniz.");
        }
    }
}
