using Microsoft.AspNetCore.Http;
using PAP.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PAP.Business.ViewModels
{
    public class FeedPostViewModel
    {
        [Required]
        public string TextOnPublish { get; set; }
        public string Path { get; set; }
        public byte[] PhotoBytes { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }

        public Guid UserId { get; set; }
    }
}
