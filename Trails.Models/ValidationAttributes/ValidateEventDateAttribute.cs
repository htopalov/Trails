using System.ComponentModel.DataAnnotations;
using Trails.Common;
using Trails.Models.Event;

namespace Trails.Models.ValidationAttributes
{
    public class ValidateEventDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var model = (IEventModel) validationContext.ObjectInstance;
            var startDate = Convert.ToDateTime(value);
            var endDate = Convert.ToDateTime(model.EndDate);

            if (endDate < startDate)
            {
                return new ValidationResult(ErrorMessages.InvalidStartEndDate);
            }
            else if (DateTime.UtcNow > startDate.AddDays(-3))
            {
                return new ValidationResult(ErrorMessages.EventThreeDaysBeforeStartError);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
