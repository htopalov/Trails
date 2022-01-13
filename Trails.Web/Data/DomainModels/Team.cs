using System.ComponentModel.DataAnnotations;

using static Trails.Web.Data.DataConstants.Common;

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

        public int UsersCount { get; set; }

        public string EventId { get; set; }
        public Event Event { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
