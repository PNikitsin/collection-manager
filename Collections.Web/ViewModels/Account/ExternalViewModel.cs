using Microsoft.AspNetCore.Authentication;

namespace Collections.Web.ViewModels.Account
{
    public class ExternalViewModel
    {
        public IEnumerable<AuthenticationScheme> Schemes { get; set; }
    }
}