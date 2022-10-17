using PAP.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PAP.Business.ViewModels
{
    public class FeedIndexViewModel
    {
        public int AccountPublishId { get; set; }
        [DataType(DataType.MultilineText)]
        public string TextOnPublish { get; set; }
        public string PhotoPath { get; set; }


        public string UserNick { get; set; }
        public string  UserPublishPhoto { get; set; }

        public int ContentPublishId { get; set; }
        //create feedback
        public string FeedBackText { get; set; }
        //feedback index
        public IEnumerable<FeedIndexFeedBackViewModel> feedIndexFeedBacks { get; set; }

    }
}
