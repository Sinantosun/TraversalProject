using EntityLayer.Concrete;


namespace BussinessLayer.AbstractValidator
{
    public interface IReservationService : IGenericService<Reservation>
    {

        List<Reservation> TGetListWithReservationByWaitApproval(int id);
        List<Reservation> TGetListWithReservationByAccepted(int id);
        List<Reservation> TGetListWithReservationByPrevious(int id);
        List<Reservation> TGetListReservationWithUserAndDestnation();
    }
}
