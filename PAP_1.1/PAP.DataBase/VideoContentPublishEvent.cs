using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PAP.DataBase
{
    public class VideoContentPublishEvent
    {
        [Key]
        public int VideoContentPublishEventId { get; set; }

        public int ContentPublishEventId { get; set; }

        [StringLength(500)]
        public string VideoUrl { get; set; }
        [ForeignKey(nameof(ContentPublishEventId))]
        public virtual ContentPublishEvent ContentPublishEvent { get; set; }
    }
}
