
using DtoLayer.GuideDtos;
using FluentValidation;

namespace BussinessLayer.ValidationRules.GuideValidator
{
    public class CreateGuideValidator : AbstractValidator<CreateGuideDto>
    {
        public CreateGuideValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad Alanı Boş Geçilmez.");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Görsel Alanı Boş Geçilmez.");
            RuleFor(x => x.TwitterUrl).NotEmpty().WithMessage("Twitter Alanı Boş Geçilmez.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description Alanı Boş Geçilmez.");
            RuleFor(x => x.Description2).NotEmpty().WithMessage("Description2 Alanı Boş Geçilmez.");
            RuleFor(x => x.InstagramUrl).NotEmpty().WithMessage("InstagramUrl Alanı Boş Geçilmez.");
  
            
        }
    }
}
