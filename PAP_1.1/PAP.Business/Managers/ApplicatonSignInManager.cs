using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PAP.DataBase.Auth;

namespace PAP.Business.Managers
{
    public class ApplicatonSignInManager : SignInManager<User>
    {
        public ApplicatonSignInManager(UserManager<User> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
        }

        public override Task<bool> CanSignInAsync(User user)
        {
            return base.CanSignInAsync(user);
        }
    }
}
