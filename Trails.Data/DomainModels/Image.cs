using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Trails.Common.ValidationConstants;

namespace Trails.Data.DomainModels
{
    public class Image
    {
        public Image() 
            => this.Id = Guid.NewGuid().ToString();

        [Key]
        [MaxLength(EntityIdMaxLength)]
        public string Id { get; set; }

        [MaxLength(ImageTitleMaxLength)]
        public string Title { get; set; }

        public byte[] DataBytes { get; set; }

        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(Creator))]
        public string CreatorId { get; set; }
        public User Creator { get; set; }

    
    }
}
