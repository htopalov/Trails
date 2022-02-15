using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Trails.Common;
using Trails.Data.Enums;

namespace Trails.Data.DomainModels
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(ValidationConstants.FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.LastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.CountryNameMaxLength)]
        public string CountryName { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }
    }
}
