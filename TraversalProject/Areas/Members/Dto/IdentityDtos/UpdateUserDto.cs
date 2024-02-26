using Microsoft.AspNetCore.Http;

namespace TraversalProject.Areas.Members.Dto.IdentityDtos
{
    public class UpdateUserDto
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string imageurl { get; set; }

        public IFormFile Image { get; set; }
    }
}
