using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static Trails.Web.Data.DataValidationConstants.Common;

namespace Trails.Web.Data.DomainModels
{
    public class Team
    {
        public Team()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new List<User>();
        }
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public int UsersMaxCount { get; set; }

        [ForeignKey(nameof(Event))]
        public string EventId { get; set; }
        public Event Event { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
