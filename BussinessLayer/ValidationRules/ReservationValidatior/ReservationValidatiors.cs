using DtoLayer.ReservationDtos;
using FluentValidation;

namespace BussinessLayer.ValidationRules.ReservationValidatior
{
    public class ReservationValidatiors : AbstractValidator<CreateResarvationDto>
    {
        public ReservationValidatiors()
        {
            RuleFor(x => x.PersonCount).NotEmpty().WithMessage("Lütfen Bu Alanı Doldurun");
            RuleFor(x => x.ReservastionDate).NotEmpty().WithMessage("Lütfen Tarih Seçiniz.");
            RuleFor(x => x.DestinationID).NotEmpty().WithMessage("Lütfen Lokasyon Seçiniz.");
        }
    }
}
