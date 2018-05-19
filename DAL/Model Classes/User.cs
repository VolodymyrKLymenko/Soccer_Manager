using Microsoft.AspNetCore.Identity;

namespace DAL.Model_Classes
{
    public enum UType { Team = 0, Organizer = 1};

    public class User: IdentityUser
    {
        public int UserId { get; set; }
    }
}
