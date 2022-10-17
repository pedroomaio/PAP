using PAP.Business.ViewModels;
using PAP.Business.ViewModels.Account;
using PAP.DataBase.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace PAP.Business.Repositories
{
    public interface IAccountRepository
    {
        void UpdateData(AccountDataViewModel user);

        AccountInfoViewModel GetUserInfo(Guid UserId);
    }
}
