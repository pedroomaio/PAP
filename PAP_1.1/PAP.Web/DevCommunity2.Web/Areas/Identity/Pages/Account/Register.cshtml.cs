using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PAP.Business.Managers;
using PAP.Business.Persistence.Repositories;
using PAP.Business.Repositories;
using PAP.DataBase.Auth;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Linq;

namespace DevCommunity2.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly AccountRepository _AccountRepository;
        private readonly BaseManager _baseManager;
        private readonly HostingEnvironment _hostingEnvironment;


        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IAccountRepository accountRepos,
            BaseManager baseManager,
            IHostingEnvironment hostingEnvironment)
        {
            _AccountRepository = (AccountRepository)accountRepos;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _baseManager = baseManager;
            _hostingEnvironment = (HostingEnvironment)hostingEnvironment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Display(Name = "Nick Name")]
            public string NickName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Chosse you profile Photo")]
            public IFormFile Photo { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Feed/Index");
            if (ModelState.IsValid)
            {
                var photourl = UploadPhoto(Input.Photo);

                var user = new User { UserName = Input.Email, Email = Input.Email, PhotoUrl = photourl, NickName = Input.NickName };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public string UploadPhoto(IFormFile File)
        {
            try
            {
                var uploadFolder = Path.Combine(
               _hostingEnvironment.WebRootPath, "Images", "UserPhotos");
                var uniqueFileName = Guid.NewGuid() + File.FileName;

                var path = Path.Combine(uploadFolder, uniqueFileName);


                using (var stream = new FileStream(path, FileMode.Create))
                {
                    File.CopyToAsync(stream);
                }
                return uniqueFileName;
            }
            catch (Exception)
            {
                return "DefaultUserPhoto";
                throw;
            }
           
        }
    }
}
