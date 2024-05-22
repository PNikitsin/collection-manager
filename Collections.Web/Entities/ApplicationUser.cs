using Microsoft.AspNetCore.Identity;

namespace Collections.Web.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsBlocked { get; set; }
    }
}