using DtoLayer.RegisterDtos;
using EntityLayer.Concrete;
using FluentValidation;

namespace BussinessLayer.ValidationRules
{
    public class RegisterValidator : AbstractValidator<CreateUserDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu Alan Boş Geçilemez.");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Minumum 3 harfden oluşmalı.");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Maximum 50 harfden oluşmalı.");

            RuleFor(x => x.Surname).NotEmpty().WithMessage("Bu Alan Boş Geçilemez.");
            RuleFor(x => x.Surname).MinimumLength(2).WithMessage("Minumum 2 harfden oluşmalı.");
            RuleFor(x => x.Surname).MaximumLength(50).WithMessage("Maximum 50 harfden oluşmalı.");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Bu Alan Boş Geçilemez.");
            RuleFor(x => x.UserName).MinimumLength(5).WithMessage("Minumum 5 harfden oluşmalı.");
            RuleFor(x => x.UserName).MaximumLength(15).WithMessage("Maximum 15 harfden oluşmalı.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Bu Alan Boş Geçilemez.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Bu Alan Boş Geçilemez.");

            RuleFor(x => x.MailAdress).NotEmpty().WithMessage("Bu Alan Boş Geçilemez.");
            RuleFor(x => x.MailAdress).EmailAddress().WithMessage("Geçersiz Email Adresi");

        }
    }
}
