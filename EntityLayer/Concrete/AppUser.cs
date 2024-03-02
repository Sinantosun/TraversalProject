using Microsoft.AspNetCore.Identity;

namespace EntityLayer.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public List<Reservation> Reservations { get; set; }

        public bool ChangePasswordEveryThreeMonthsIsActive { get; set; }

        public DateTime? LastChangePasswordDate { get; set; }
        public DateTime? ThreeMonthsLaterPasswordDate { get; set; }
    }
}

