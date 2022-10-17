using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAP.Business.Persistence.Repositories;
using PAP.Business.Repositories;

namespace DevCommunity2.Web.Controllers
{

    [Authorize]
    public class UserController : Controller
    {

        private readonly AccountRepository _accountRepository;

        public UserController(IAccountRepository accountRepo)
        {
            _accountRepository = (AccountRepository)accountRepo;
        }

        [HttpPost]
        public JsonResult GetUserPhoto()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Json("ERRO");
            }

            var user = _accountRepository.GetUserInfo(new Guid(userId));

            if (user == null)
            {
                return Json("ERRO");
            }
            string userPhoto = "/Images/UserPhotos/" + user.PhotoUrl;
            return Json(userPhoto);
        }

        [HttpPost]
        public JsonResult GetNickName()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Json("Bad Request");
            }

            var user = _accountRepository.GetUserInfo(new Guid(userId));

            if (user == null)
            {
                return Json("Bad Request");
            }
            return Json(user.Nickname);
        }
    }
}