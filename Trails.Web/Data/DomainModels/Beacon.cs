using System.ComponentModel.DataAnnotations;

using static Trails.Web.Data.DataValidationConstants.Beacon;

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
        [MaxLength(ImeiLength)]
        public string Imei { get; set; }

        [Required]
        [MaxLength(SimCardNumberLength)]
        public string SimCardNumber { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public string KeyHash { get; set; }

        public ICollection<BeaconData> BeaconData { get; set; }
    }
}
