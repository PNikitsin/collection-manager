using Microsoft.AspNetCore.Identity;

namespace Collections.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsBlocked { get; set; }
    }
}