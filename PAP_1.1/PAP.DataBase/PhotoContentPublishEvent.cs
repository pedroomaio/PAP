namespace PAP.DataBase
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class PhotoContentPublishEvent
    {
        [Key]
        public int PhotoContentPublishEventId { get; set; }

        public int ContentPublishEventId { get; set; }

        [StringLength(500)]
        public string PhotoURl { get; set; }

        [ForeignKey(nameof(ContentPublishEventId))]
        public virtual ContentPublishEvent ContentPublishEvent { get; set; }
    }
}
