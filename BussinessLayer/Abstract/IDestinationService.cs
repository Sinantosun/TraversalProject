using EntityLayer.Concrete;


namespace BussinessLayer.AbstractValidator
{
    public interface IDestinationService : IGenericService<Destination>
    {
        int TGetDestinationService();
        Destination TgetDestinationByCityName(string name);
        void TdeleteDestinationByCityName(string name);
    }
}
