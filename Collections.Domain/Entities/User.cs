using Microsoft.AspNetCore.Identity;

namespace Collections.Domain.Entities
{
    public class User : IdentityUser
    {
        public bool IsBlocked { get; set; }
    }
}