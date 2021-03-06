using System.ComponentModel.DataAnnotations;
using static Trails.Common.ValidationConstants;

namespace Trails.Data.DomainModels
{
    public class Beacon
    {
        public Beacon()
        {
            this.Id = Guid.NewGuid().ToString();
            this.BeaconData = new List<BeaconData>();
        }

        [Key]
        [MaxLength(EntityIdMaxLength)]
        public string Id { get; set; }

        [Required]
        [MaxLength(BeaconImeiMaxLength)]
        public string Imei { get; set; }

        [Required]
        [MaxLength(SimCardNumberLength)]
        public string SimCardNumber { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [MaxLength(BeaconKeyHashMaxLength)]
        public string KeyHash { get; set; }

        public ICollection<BeaconData> BeaconData { get; set; }
    }
}
