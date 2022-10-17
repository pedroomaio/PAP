using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PAP.DataBase
{
    public  class ContentPublishEvent
    {       
        [Key]
        public int ContentPublishEventId { get; set; }

        public int PublishEventId { get; set; }

        public string TextContent { get; set; }

        [StringLength(500)]
        public string  GithubFile { get; set; }
      
        public PublishEvent PublishEvent { get; set; }

       
        [InverseProperty(nameof(PhotoContentPublishEvent.ContentPublishEvent))]
        public virtual ICollection<PhotoContentPublishEvent> PhotoContentPublishEvents { get; set; }
        [InverseProperty(nameof(VideoContentPublishEvent.ContentPublishEvent))]
        public virtual ICollection<VideoContentPublishEvent> VideoContentPublishEvents { get; set; }
    }
}
