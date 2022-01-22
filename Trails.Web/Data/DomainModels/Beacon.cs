using System.ComponentModel.DataAnnotations;
using Trails.Web.Common;

namespace Trails.Web.Data.DomainModels
{
    public class Beacon
    {
        public Beacon()
        {
            this.Id = Guid.NewGuid().ToString();
            this.BeaconData = new List<BeaconData>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Imei { get; set; }

        [Required]
        [MaxLength(ValidationConstants.SimCardNumberLength)]
        public string SimCardNumber { get; set; }

        [Required]
        [MaxLength(ValidationConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        public string KeyHash { get; set; }

        public ICollection<BeaconData> BeaconData { get; set; }
    }
}
