using System.ComponentModel.DataAnnotations;
using Trails.Models.Event;
using static Trails.Common.ErrorMessages;

namespace Trails.Models.ValidationAttributes
{
    public class ValidateEventDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (IEventModel) validationContext.ObjectInstance;
            var startDate = Convert.ToDateTime(value);
            var endDate = Convert.ToDateTime(model.EndDate);

            if (endDate < startDate)
            {
                return new ValidationResult(InvalidStartEndDate);
            }
            else if (DateTime.UtcNow > startDate.AddDays(-3))
            {
                return new ValidationResult(EventThreeDaysBeforeStartError);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
