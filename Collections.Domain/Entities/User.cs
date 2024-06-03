using Microsoft.AspNetCore.Identity;

namespace Collections.Domain.Entities
{
    public class User : IdentityUser
    {
        public bool IsBlocked { get; set; }

        public List<Collection> Collections { get; set; }
    }
}