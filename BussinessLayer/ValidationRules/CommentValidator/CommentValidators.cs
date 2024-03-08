

using DtoLayer.CommentDtos;
using FluentValidation;

namespace BussinessLayer.ValidationRules.CommentValidator
{
    public class CommentValidators : AbstractValidator<CreateCommandDto>
    {
        public CommentValidators()
        {
            RuleFor(x => x.CommentContent).NotEmpty().WithMessage("Lütfen Önce Yorumunuzu Yazınız.");
            RuleFor(x => x.CommentContent).MaximumLength(200).WithMessage("Maximum 200 karakterlik yorum yazınız.");
            RuleFor(x => x.CommentUser).MaximumLength(50).WithMessage("Ad Soyad Alanı Maximum 50 karakter olmalı.");
            RuleFor(x => x.CommentUser).NotEmpty().WithMessage("Ad Soyad Alanı Boş Geçilemez.");


        }
    }
}
