
using DtoLayer.ContactUSDtos;
using FluentValidation;

namespace BussinessLayer.ValidationRules.ContactUSValidators
{
    public class SendContactUsValidator : AbstractValidator<CreateContactUSDto>
    {
        public SendContactUsValidator()
        {
            RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail Alanı Boş Geçilemez");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Mail Alanı Boş Geçilemez");
            RuleFor(x => x.Subject).MaximumLength(20).WithMessage("Konu başlığını lütfen 20 karakterden kısa tutun.");
            RuleFor(x => x.Subject).MinimumLength(5).WithMessage("Konu başlığını lütfen 5 karakterden uzun tutun.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Mail Alanı Boş Geçilemez");
            RuleFor(x => x.MessageBody).NotEmpty().WithMessage("Mail Alanı Boş Geçilemez");
            RuleFor(x => x.MessageBody).MaximumLength(500).WithMessage("500 karakter sınırını aştınız lütfen 500 karakterden kısa mesaj yazınız.");
            RuleFor(x => x.MessageBody).MinimumLength(5).WithMessage("Mesaj alanını lütfen en az 5 karakter içerecek şekilde girin.");

        }

    }
}
