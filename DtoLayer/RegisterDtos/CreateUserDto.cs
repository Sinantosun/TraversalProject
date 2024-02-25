

namespace DtoLayer.RegisterDtos
{
    public class CreateUserDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? UserName { get; set; }
        public string? MailAdress { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
