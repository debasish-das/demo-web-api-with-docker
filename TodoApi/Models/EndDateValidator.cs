using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class EndDateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Models.TodoItem)validationContext.ObjectInstance;
            if (model.StartDate == null || value == null)  // Abort if no End Date
                return ValidationResult.Success;

            var EndDate = (DateTimeOffset) value; // value = EndDate
            var StartDate = model.StartDate.GetValueOrDefault();

            if (StartDate > EndDate)
                return new ValidationResult("The start date must be before the end date");
            else
                return ValidationResult.Success;
        }
    }
}
