using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTodo.Models
{
    public class StartDateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Models.TodoItem)validationContext.ObjectInstance;
            if (model.DueAt == null || value == null)  // Abort if no End Date
                return ValidationResult.Success;

            var EndDate = model.DueAt.GetValueOrDefault();
            var StartDate = (DateTimeOffset) value;  // value = StartDate

            if (StartDate > EndDate)
                return new ValidationResult("The start date must be before the end date");
            else
                return ValidationResult.Success;
        }
    }
}
