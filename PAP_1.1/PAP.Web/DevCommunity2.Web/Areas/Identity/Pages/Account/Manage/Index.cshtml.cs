using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PAP.Business.Managers;
using PAP.Business.Persistence.Repositories;
using PAP.Business.Repositories;
using PAP.Business.ViewModels.Account;
using PAP.DataBase.Auth;

namespace DevCommunity2.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IAccountRepository _AccountRepository;
        private readonly HostingEnvironment _hostingEnvironment;
        private readonly BaseManager _BaseManager;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            IAccountRepository AccountRepo,
             IHostingEnvironment hostingEnvironment,
              BaseManager baseManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _AccountRepository = (AccountRepository)AccountRepo;
            _hostingEnvironment = (HostingEnvironment)hostingEnvironment;
            _BaseManager = baseManager;
        }


        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {

            [EmailAddress]
            public string Email { get; set; }
  
            [Display(Name = "Photo")]
            public IFormFile PhotoUrl { get; set; }


        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var email = await _userManager.GetEmailAsync(user);



            Input = new InputModel
            {
                Email = email
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var uploadFolder = Path.Combine(
                    _hostingEnvironment.WebRootPath, "Images", "UserPhotos");
            var uniqueFileName = Guid.NewGuid() + Input.PhotoUrl.FileName;

            var path = Path.Combine(uploadFolder, uniqueFileName);


            using (var stream = new FileStream(path, FileMode.Create))
            {
                await Input.PhotoUrl.CopyToAsync(stream);
            }

            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId2);

            var account = new AccountDataViewModel()
            {             
                PhotoUniqueName = uniqueFileName,
                UserId = userId2
            };

            _AccountRepository.UpdateData(account);
            _BaseManager.SaveChanges();
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}
