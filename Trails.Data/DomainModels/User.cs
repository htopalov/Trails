using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Trails.Data.Enums;
using  static Trails.Common.ValidationConstants;

namespace Trails.Data.DomainModels
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(CountryNameMaxLength)]
        public string CountryName { get; set; }

        public DateTime BirthDate { get; set; }

        public Gender Gender { get; set; }
    }
}
