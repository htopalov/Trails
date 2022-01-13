using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Trails.Web.Data.Enums;

using static Trails.Web.Data.DataConstants.User;

namespace Trails.Web.Data.DomainModels
{
    public class User : IdentityUser
    {
        public User()
        {
            this.UsersEvents = new List<UserEvent>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(CountryNameMaxLength)]
        public string CountryName { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        //if user is volunteering to event he can't be participant...so need to check for that when applying for event!!!
        //no matter volunteer or participant he should be added to UserEvents collection
        public bool IsVolunteering { get; set; }

        [ForeignKey(nameof(Team))]
        public string TeamId { get; set; }
        public Team Team { get; set; }

        [ForeignKey(nameof(Beacon))]
        public string BeaconId { get; set; }
        public Beacon Beacon { get; set; }

        public ICollection<UserEvent> UsersEvents { get; set; }
    }
}
