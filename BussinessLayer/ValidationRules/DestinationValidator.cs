

using DtoLayer.DestinationDtos;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BussinessLayer.ValidationRules
{
    public class DestinationValidator : AbstractValidator<CreateDestinationDto>
    {
        public DestinationValidator()
        {
            RuleFor(x => x.Capacity).NotEmpty().WithMessage("Boş Geçilemez.");
            RuleFor(x => x.City).NotEmpty().WithMessage("Boş Geçilemez.");
            RuleFor(x => x.DayNight).NotEmpty().WithMessage("Boş Geçilemez.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Boş Geçilemez.");
            RuleFor(x => x.Description).MaximumLength(100).WithMessage("Lütfen açıklamayı 100 karakden kısa tutun.");
            RuleFor(x => x.Details1).NotEmpty().WithMessage("Boş Geçilemez.");
            RuleFor(x => x.Details2).NotEmpty().WithMessage("Boş Geçilemez.");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Boş Geçilemez.");
            RuleFor(x => x.Image2).NotEmpty().WithMessage("Boş Geçilemez.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Boş Geçilemez.");
            RuleFor(x => x.Price).Must(price => double.TryParse(price.ToString(), out _))  .WithMessage("Price must be a valid double number.");


        }

    }
}
