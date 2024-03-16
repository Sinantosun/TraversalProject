using DtoLayer.DestinationDtos;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace BussinessLayer.ValidationRules.DestinationValidatior
{
    public class DestinationValidators : AbstractValidator<CreateDestinationDto>
    {
        public DestinationValidators()
        {
            RuleFor(x => x.Capacity).NotEmpty().WithMessage("Kapasite Alanı Boş Geçilemez.");
            RuleFor(x => x.CoverImage).NotEmpty().WithMessage("Kapak Fotoğraf Alanı Boş Geçilemez.");
            RuleFor(x => x.City).NotEmpty().WithMessage("Şehir Alanı Boş Geçilemez.");
            RuleFor(x => x.DayNight).NotEmpty().WithMessage("Gün Gece Alanı Boş Geçilemez.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıkalma Boş Geçilemez.");
            RuleFor(x => x.Description).MaximumLength(100).WithMessage("Lütfen açıklamayı 100 karakden kısa tutun.");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Görsel Alanı Boş Geçilemez.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat Alanı Boş Geçilemez.");


        }

    }
}
