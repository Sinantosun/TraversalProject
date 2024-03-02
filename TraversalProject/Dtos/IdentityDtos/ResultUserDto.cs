using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;

namespace TraversalProject.Dtos.IdentityDtos
{
    public class ResultUserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
