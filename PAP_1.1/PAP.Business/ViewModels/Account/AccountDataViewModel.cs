using System;
using System.Collections.Generic;
using System.Text;

namespace PAP.Business.ViewModels.Account
{
    public class AccountDataViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string PhotoUniqueName { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
