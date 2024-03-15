namespace DtoLayer.AccountDtos
{
    public class UpdateAccountDto
    {
        public int SenderID { get; set; }
        public int ReciverID { get; set; }
        public decimal Amount { get; set; }
    }
}
