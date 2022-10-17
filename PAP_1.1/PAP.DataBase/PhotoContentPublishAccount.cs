using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PAP.DataBase
{   
    public class PhotoContentPublishAccount
    {
        [Key]
        public int PhotoContentPublishAccountId { get; set; }

        public int ContentPublishAccountId { get; set; }

        [StringLength(500)]
        public string PhotoURl { get; set; }

        [ForeignKey(nameof(ContentPublishAccountId))]
        public virtual ContentPublishAccount ContentPublishAccount { get; set; }
    }
}
