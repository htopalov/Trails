using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

using static Trails.Web.Data.DataConstants.Common;

namespace Trails.Web.Data.DomainModels
{
    public class Team
    {
        public Team()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Users = new List<IdentityUser>();
        }
        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public int UsersCount { get; set; }

        [ForeignKey(nameof(Event))]
        public string EventId { get; set; }

        public Event Event { get; set; }

        public IEnumerable<IdentityUser> Users { get; set; }
    }
}
